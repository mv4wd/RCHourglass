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


namespace RCHourglassManager
{
    /// <summary>
    /// This command is just a placeholder to abort a sequence of commands in case of errors
    /// for the COM port writer queue
    /// </summary>
    class BeginTransactionCommand : IBasicCommand
    {
        public string Command
        {
            get
            {
                return "BEGIN TRAN"; // :-) SQL Style
            }
        }

        public string Description
        {
            get
            {
                return "Begin transaction";
            }
        }

        public string ErrorCause
        {
            get
            {
                return "";
            }
        }

        public bool HasFinished
        {
            get
            {
                return true; // This command doesn't do anything in the com port. No feedback required
            }
        }

        public bool WasSuccessfull
        {
            get
            {
                return true;
            }
        }

        public void HandleStringResponse(string resp)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Returns the timeout in ms before the command is considered expired without response
        /// </summary>
        public int TimeoutMs { get { return 1000; } }
    }


    class EndTransactionCommand : IBasicCommand
    {
        public string Command
        {
            get
            {
                return "COMMIT"; // :-) SQL Style
            }
        }

        public string Description
        {
            get
            {
                return "End transaction";
            }
        }

        public string ErrorCause
        {
            get
            {
                return "";
            }
        }

        public bool HasFinished
        {
            get
            {
                return true; // This command doesn't do anything in the com port. No feedback required
            }
        }

        public bool WasSuccessfull
        {
            get
            {
                return true;
            }
        }

        public void HandleStringResponse(string resp)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the timeout in ms before the command is considered expired without response
        /// </summary>
        public int TimeoutMs { get { return 1000; } }
    }
}
