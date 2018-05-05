using System;
using System.Collections.Generic;
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


/// <summary>
/// This file contains utility functions to encode/decode the data packets for the transponder.
/// </summary>
namespace RCHourglassManager
{
    class TransponderEncoding
    {
        const int TRANSPONDER_PACKET_LENGTH = 12;
        const UInt64 POLY = 0X0000776107;

        public static byte[] encodeTransponderNoStatus(UInt32 transponderNumber)
        {
            return encodeTransponderFull(transponderNumber, 510, 0);

        }

        public static byte[] encodeTransponderWithStatus(UInt32 transponderNumber)
        {
            return encodeTransponderFull(transponderNumber, 0, 0);
        }


        /// <summary>
        /// Returns the telegram to send for a given transponder number
        /// </summary>
        /// <param name="transponderNumber">Transponder number as read by the lap timing software</param>
        /// <param name="statusBits">Status bits that are inserted every 3 bit of the transponder number. LSB is the last unknown bit transmitted</param>
        /// <param name="lastByte">The last byte encoded in the telegram. It's usually 0</param>
        /// <returns>The telegram bytes</returns>
        public static byte []  encodeTransponderFull(UInt32 transponderNumber, UInt32 statusBits, Byte lastByte)
        {
            byte[] retValue  = new byte[ TRANSPONDER_PACKET_LENGTH];

            transponderNumber |= (UInt32) ((UInt64)lastByte << 24);        //add last byte to number input	

            // Punctuate transponder number with the status bits
            UInt64 fflop = 0, temp = 0, temp1;
            int i, j = 0;
            for (i = 0; i < 40; i++)
            {
                temp <<= 1;
                if (j < 3)
                {                                   //this is not status bit
                    temp |= ((transponderNumber & 0x20000000) >> 29);           //add bit from number to stream
                    transponderNumber <<= 1;
                }
                else
                {                                           //this is ghost bit 
                    temp |= (statusBits & 0x0200) >> 9;     //add status bit to stream
                    statusBits <<= 1;
                }
                if (j < 3) j++; else j = 0;

            }

            j = 0;

            int k = 2;
            fflop |= (temp & 0x01);                                     //add in stream to ff 
            temp >>= 1;                                                 //shift in stream	
            for (i = 0; i < 40; i++)
            {
                UInt64 bit_in =  (temp & 0x01);

                temp1 = fflop & POLY;                                       //mask ff with polynomial
                temp1 ^= temp1 >> 1;                                        //calculate parity (number of 1's in stream)
                temp1 ^= temp1 >> 2;
                temp1 = (temp1 & 0x1111111111111111UL) * 0x1111111111111111UL;
                temp1 = (temp1 >> 60) & 0x01;


                if (k >= TRANSPONDER_PACKET_LENGTH)
                {
                    // This is an error!
                    break;
                }
                retValue[k] <<= 1;                                                          //shift output
                retValue[k] |= (byte) (temp1 ^ bit_in);                                              //add to output
                retValue[k] <<= 1;                                                          //shift output		
                retValue[k] |= (byte)(temp1 ^ bit_in ^ (fflop & 0x01 ));              //add to output 

                if (j < 3) j++;
                else
                {
                    // Every 4 bits a new byte
                    j = 0;
                    k++;
                }


                temp >>= 1;                                         //shift in stream	
                fflop <<= 1;                                        //shift flipflops
                fflop |= bit_in;                                    //add in stream to ff 


            }
            retValue[0] = 0xF9;
            retValue[1] = 0x16;
            return retValue;

        }

        /// <summary>
        /// Decods a RC3 transponder packet (filled with pramlbe
        /// </summary>
        /// <param name="telegram"></param>
        /// <returns></returns>
        public static UInt32 decodeRC3Telegram(byte[] telegram)
        {
            if (telegram == null || telegram.Length != 12) throw new ArgumentException("Wrong telegram size");



            UInt32 decoded = 0;
            byte k, b;
            for (k = TRANSPONDER_PACKET_LENGTH - 1; k >= 2; k--) // all bytes opposite except preamble
            {
                byte digit = telegram[k];
                for (b = 0; b < 3; b++)
                {
                    decoded <<= 1;
                    if ((digit & 0x03) == 0x01 ||
                        (digit & 0x03) == 0x02)
                    {
                        decoded |= 1;
                    }
                    digit >>= 2;
                }
            }
            return decoded;

        }

        /// <summary>
        /// Pretty simple test method against some well known numbers
        /// </summary>
        public static void doTest()
        {
            byte[] result = encodeTransponderWithStatus(4961721);
            byte[] expected = { 0XF9, 0X16, 0XEF, 0XDA, 0X35, 0X3B, 0X28, 0X29, 0X0B, 0X0B, 0XCF, 0X3C };

            for (int i=0;i< result.Length;i++)
            { 
                if (result[i] != expected[i]) throw new Exception("Test failed 4961721");
            }

            if (decodeRC3Telegram(result) != 4961721) throw new Exception("Test decode 5843805");

            result = encodeTransponderNoStatus(5843805);
            expected = new byte[] { 0xf9, 0x16, 0xe2, 0x9b, 0xad, 0x5d, 0x7b, 0x44, 0x79, 0x87, 0x7f, 0x03 };

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] != expected[i]) throw new Exception("Test failed 5843805");
            }

            if (decodeRC3Telegram(result)!= 5843805) throw new Exception("Test decode 5843805");

            // Example status packet... uses the same encoding
            result = encodeTransponderFull(7632095, 76, 0);
            expected = new byte[] { 0XF9, 0X16, 0xDA, 0xE7, 0x94, 0x77, 0xE9, 0x3C,  0x91, 0xD7, 0xC3, 0xCC };

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] != expected[i]) throw new Exception("Test failed 7632095");
            }
        }
    }
}
