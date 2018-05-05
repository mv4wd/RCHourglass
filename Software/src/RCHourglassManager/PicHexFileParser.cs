using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

/* ========================================
 * This software is copyright by Marco Venturini , 2017
 * and it's part of RCHourglass project currently hosted on GitHub
 * 
 * Licensing terms
 *
 * Short version: if you build it for your personal use, you're only kindly requested to donate a small sum to a children charity of your choice (use 'RC Hourglass for children' as a reference).
 * Please feedback your donations to:charity dot rchourglass at gmail.com
 *
 * Please consider donating 5 euros per transponder and 30 euros for the decoder for personal use. If the transponder is used in a club/circuit with an admission fee, please consider dontaing 100 euros for the decoder.
 *
 * The original RCTech thread author/designer (Howard Cano) license applies to the decoder project:
 * "All information presented on this thread is free for use for personal, non-commercial purposes. Contact me for licensing arrangements if you wish to produceand market the decoder." -user howardcano on rctech
 *
 * Additional licensing terms for the PSOC firmware/design & transponder by mv4wd:
 * "All information shared is free for use for personal, non-commercial purposes.  Contact me for licensing arrangements if you wish to produce and market the decoder. 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * Any derivative work source code/design must be public and use this licensing terms.
 * Any device derived from this project must respond to the command 'License' with this text.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANYKIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
 * WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OROTHER LIABILITY, WHETHER IN AN
 * ACTION OF CONTRACT, TORT OROTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE." Marco Venturini - mv4wd
 *
 * ========================================
*/

namespace RCHourglassManager
{
    /// <summary>
    /// This is a minimal class to parse HEX file as produced by MASM compiler
    /// Warning : a lot of PIC-specific hex formatting info is implied
    /// </summary>
    public class PicHexFileParser
    {
        /// <summary>
        /// Fills the memory blocks from the stream
        /// </summary>
        /// <param name="s"></param>
        public void ParseHex(StreamReader s)
        {
            uint address = 0;
            byte [] buffer = new byte[255];
            Records = new MemoryContent();
            int lineNumber = 0;
            while (!s.EndOfStream)
            {
                try
                {
                    String line = s.ReadLine().Trim();
                    lineNumber++;
                    if (String.IsNullOrEmpty(line)) continue;
                    if (!line.StartsWith(":")) continue;
                    if (line.Length < 11) throw new Exception("Unexpected end of line");
                    uint byteCount = Convert.ToUInt16(line.Substring(1, 2), 16);
                    uint lineAddress   = Convert.ToUInt16(line.Substring(3, 4), 16);
                    uint recordType   = Convert.ToUInt16(line.Substring(7, 2), 16);

                    if (line.Length < 11+ byteCount) throw new Exception("Line too short");
                    for (int i = 0; i < byteCount;i++)
                    {
                        buffer[i] = Convert.ToByte(line.Substring(9 + 2*i, 2), 16);
                    }
                    switch(recordType)
                    {
                       
                        case 1: return; // Eof record
                        case 0:  // Data record
                            {
                                HexMemoryBlock b = new HexMemoryBlock();
                                b.BaseAddress = (address | lineAddress)/2; // Pic specific addressing
                                b.DataBlock = new UInt16[byteCount / 2];
                                for (int i=0; i < byteCount; i+=2 )
                                {
                                    UInt16 number = buffer[i+1];
                                    number = (UInt16)(number << 8  | buffer[i]);
                                    b.DataBlock[i / 2] = number;
                                }
                                Records.Add(b);
                                break;
                            }
                        case 4: // Extended linear address
                            {
                                if (byteCount != 2) throw new Exception("Wrong byte count in Extended Linear Address record");
                                address = buffer[0];
                                address = (address << 8 | buffer[1]) << 16;
                                break;
                            }
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception($"Parsing error at line {lineNumber}. Cause: {ex.Message}", ex);
                }
            }
            
        }

        /// <summary>
        /// Memoery blocks read from the stream
        /// </summary>
        public MemoryContent Records;
    }


    /// <summary>
    /// Structure that represents a memory block to be written on the PIC
    /// </summary>
    public class HexMemoryBlock
    {
        public uint BaseAddress { get; set; }

        public UInt16  [] DataBlock { get; set; }

        public uint LastAddress { get { return ( uint) (BaseAddress + DataBlock?.Length); } }


        public void WriteMem(HexMemoryBlock newValues)
        {
            if (newValues == null) return;
            uint from = Math.Max(this.BaseAddress, newValues.BaseAddress);
            uint to = Math.Min(this.LastAddress, newValues.LastAddress);
            for (uint i = from; i < to; i++)
            {

                DataBlock[i - BaseAddress] = newValues.DataBlock[i - newValues.BaseAddress];


            }
        }

        public void ReadMem(HexMemoryBlock newValues)
        {
            if (newValues == null || newValues.DataBlock?.Length ==0) return;
            uint from = Math.Max(this.BaseAddress, newValues.BaseAddress);
            uint to   = Math.Min(this.LastAddress, newValues.LastAddress);
            for (uint i = from; i< to; i++)
            {

                newValues.DataBlock[i - newValues.BaseAddress] = DataBlock[i - BaseAddress];
 

            }
        }
    }

    /// <summary>
    /// Helper class to access memory blocks in a linear way
    /// </summary>
    public class MemoryContent : List<HexMemoryBlock>
    {
        public void WriteMem(HexMemoryBlock newValues)
        {
            foreach (HexMemoryBlock x in this) x?.WriteMem(newValues);
        }

        public HexMemoryBlock ReadMem(uint adress , uint count)
        {
            if (count <= 0) throw new ArgumentException("Invalid cell count");
            HexMemoryBlock retVal = new HexMemoryBlock();
            retVal.BaseAddress = adress;
            retVal.DataBlock = new UInt16[count];
            foreach (HexMemoryBlock x in this) x?.ReadMem(retVal);
            return retVal;
        }

    }
}
