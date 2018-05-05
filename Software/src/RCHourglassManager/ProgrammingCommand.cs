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
/// This file contains PIC programming commands
/// </summary>

namespace RCHourglassManager
{

    /// <summary>
    /// Detect which PIC is in the socket
    /// </summary>
    public class PicDetectCommand : IBasicCommand
    {
        protected String errorCause = String.Empty;

        protected bool finished = false;

        protected string picModelID = String.Empty;

         
        /// <summary>
        /// Return HEX ID for the model. This is in Microchip datasheets
        /// </summary>
        public String PicModelId { get { return picModelID;  } }

        public String PicPartNumber
        {
            get
            {
                switch (picModelID)
                {
                    case "": return String.Empty;
                    case "3066": return "PIC16F18313";
                    case "3068": return "PIC16LF18313";
                    case "3067": return "PIC16F18323";
                    case "3069": return "PIC16LF18323";
                        // add here more models
                    default: return "Model " + picModelID;
                }
            }
        }

        public string Command
        {
            get
            {
                return $"DETECT";
            }
        }

        public string Description
        {
            get
            {
                return "Read the model of the PIC in the programmer";
            }
        }

        public string ErrorCause
        {
            get
            {
                return errorCause;
            }
        }

        public bool HasFinished
        {
            get
            {
                return finished;
            }
        }

        public bool WasSuccessfull
        {
            get
            {
                return finished && String.IsNullOrEmpty(errorCause);
            }
        }

        public void HandleStringResponse(string resp)
        {
            if (String.IsNullOrEmpty(resp)) return;
            if (resp.StartsWith("SUCCESS PIC MODEL "))
            {
                finished = true;
                try
                {
                    picModelID = resp.Substring("SUCCESS PIC MODEL ".Length).Trim();
                     


                }
                catch (Exception e)
                {
                    errorCause = "Error parsing response for PIC detection. Cause: " + e.Message;

                }


            }
            if (resp.StartsWith("ERROR"))
            {
                this.finished = true;
                errorCause = resp.Substring(6).Trim();
                if (String.IsNullOrEmpty(errorCause)) errorCause = "Generic error";
                return;
            }
        }

        /// <summary>
        /// Returns the timeout in ms before the command is considered expired without response
        /// </summary>
        public int TimeoutMs { get { return 30000; } }
    }



    /// <summary>
    /// Begin PIC programming
    /// </summary>
    public class PicStartProgramCommand : IBasicCommand
    {
        protected String errorCause = String.Empty;

        protected bool finished = false;

 
        public string Command
        {
            get
            {
                return $"BEGIN PROGRAMMING";
            }
        }

        public string Description
        {
            get
            {
                return "Begin PIC programming";
            }
        }

        public string ErrorCause
        {
            get
            {
                return errorCause;
            }
        }

        public bool HasFinished
        {
            get
            {
                return finished;
            }
        }

        public bool WasSuccessfull
        {
            get
            {
                return finished && String.IsNullOrEmpty(errorCause);
            }
        }

        public void HandleStringResponse(string resp)
        {
            if (String.IsNullOrEmpty(resp)) return;
            if (resp.StartsWith("SUCCESS BEGIN PROGRAMMING"))
            {
                finished = true;
            }
            if (resp.StartsWith("ERROR"))
            {
                this.finished = true;
                errorCause = resp.Substring(6).Trim();
                if (String.IsNullOrEmpty(errorCause)) errorCause = "Generic error";
                return;
            }
        }

        /// <summary>
        /// Returns the timeout in ms before the command is considered expired without response
        /// </summary>
        public int TimeoutMs { get { return 5000; } }
    }

    /// <summary>
    /// End PIC programming
    /// </summary>
    public class PicEndProgramCommand : IBasicCommand
    {
        protected String errorCause = String.Empty;

        protected bool finished = false;

        public string Command
        {
            get
            {
                return $"END PROGRAMMING";
            }
        }

        public string Description
        {
            get
            {
                return "End PIC programming";
            }
        }

        public string ErrorCause
        {
            get
            {
                return errorCause;
            }
        }

        public bool HasFinished
        {
            get
            {
                return finished;
            }
        }

        public bool WasSuccessfull
        {
            get
            {
                return finished && String.IsNullOrEmpty(errorCause);
            }
        }

        public void HandleStringResponse(string resp)
        {
            if (String.IsNullOrEmpty(resp)) return;
            if (resp.StartsWith("SUCCESS END PROGRAMMING"))
            {
                finished = true;
            }
            if (resp.StartsWith("ERROR"))
            {
                this.finished = true;
                errorCause = resp.Substring(6).Trim();
                if (String.IsNullOrEmpty(errorCause)) errorCause = "Generic error";
                return;
            }
        }

        /// <summary>
        /// Returns the timeout in ms before the command is considered expired without response
        /// </summary>
        public int TimeoutMs { get { return 5000; } }
    }


    /// <summary>
    /// Erase PIC memory
    /// </summary>
    public class PicEraseCommand : IBasicCommand
    {
        protected String errorCause = String.Empty;

        protected bool finished = false;

        public string Command
        {
            get
            {
                return $"ERASE DEVICE";
            }
        }

        public string Description
        {
            get
            {
                return "Erase PIC memory";
            }
        }

        public string ErrorCause
        {
            get
            {
                return errorCause;
            }
        }

        public bool HasFinished
        {
            get
            {
                return finished;
            }
        }

        public bool WasSuccessfull
        {
            get
            {
                return finished && String.IsNullOrEmpty(errorCause);
            }
        }

        public void HandleStringResponse(string resp)
        {
            if (String.IsNullOrEmpty(resp)) return;
            if (resp.StartsWith("SUCCESS ERASE DEVICE"))
            {
                finished = true;
            }
            if (resp.StartsWith("ERROR"))
            {
                this.finished = true;
                errorCause = resp.Substring(6).Trim();
                if (String.IsNullOrEmpty(errorCause)) errorCause = "Generic error";
                return;
            }
        }

        /// <summary>
        /// Returns the timeout in ms before the command is considered expired without response
        /// </summary>
        public int TimeoutMs { get { return 5000; } }
    }



    /// <summary>
    /// Write PIC memory
    /// </summary>
    public class PicWriteMemoryCommand : IBasicCommand
    {
        protected String errorCause = String.Empty;

        protected bool finished = false;
        bool verify;
        uint address;
        UInt16[] data;

        public PicWriteMemoryCommand(uint address, UInt16 []data, bool verify)
        {
            if (address < 0 || data == null || data.Length > 10 || data.Length==0) throw new ArgumentException("Memory address ora data size wrong");
            this.address = address;
            this.verify = verify;
            this.data = new UInt16[data.Length];
            data.CopyTo(this.data, 0);
        }

        public string Command
        {
            get
            {
                StringBuilder s = new StringBuilder(100);

                s.Append("WRITEMEM ");
                if (verify) s.Append("V ");
                s.Append(Convert.ToString(address, 16).PadLeft(4, '0'));
                s.Append(" ");
                for (int i=0;i<data.Length;i++)
                {
                    s.Append(Convert.ToString(data[i], 16).PadLeft(4, '0'));
                }
                return s.ToString().ToUpperInvariant();

            }
        }

        public string Description
        {
            get
            {
                return "Write PIC memory";
            }
        }

        public string ErrorCause
        {
            get
            {
                return errorCause;
            }
        }

        public bool HasFinished
        {
            get
            {
                return finished;
            }
        }

        public bool WasSuccessfull
        {
            get
            {
                return finished && String.IsNullOrEmpty(errorCause);
            }
        }

        public void HandleStringResponse(string resp)
        {
            if (String.IsNullOrEmpty(resp)) return;
            if (resp.StartsWith("SUCCESS WRITEMEM ADDRESS "))
            {
                finished = true;
                String writtenAddress = resp.Substring("SUCCESS WRITEMEM ADDRESS ".Length).Trim().Split(' ')[0];
                try
                {

                    int ad = Convert.ToInt32(writtenAddress, 16);
                    if (ad!=address) errorCause = $"Address write mismatch. Got {ad} expected {address}";
                }
                catch
                {
                    errorCause = "Address write mismatch";
                }
                return;

            }
            if (resp.StartsWith("ERROR"))
            {
                this.finished = true;
                errorCause = resp.Substring(6).Trim();
                if (String.IsNullOrEmpty(errorCause)) errorCause = "Generic error";
                return;
            }
        }
        /// <summary>
        /// Returns the timeout in ms before the command is considered expired without response
        /// </summary>
        public int TimeoutMs { get { return 5000; } }
    }
}
