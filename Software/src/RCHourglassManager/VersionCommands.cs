using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




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
/// This file contains commands to query the decoder firmware version and license
/// </summary>

namespace RCHourglassManager
{

    public class VersionMinMaj
    {
        public int Minor = 0, Major = 0;
        public bool Beta=false;
    }

    /// <summary>
    /// Get the version from the decoder firmware
    /// </summary>
    public class VersionCommand : IBasicCommand
    {
        protected String errorCause = String.Empty, versionInfo  = String.Empty;
        protected bool finished = false;

        public VersionMinMaj VersionData = new VersionMinMaj();

        /// <summary>
        /// Returns the full version string
        /// </summary>
        public string Version {  get { return versionInfo;  } }

        public string Command
        {
            get
            {
                return "VERSION";
            }
        }

        public string Description
        {
            get
            {
                return "Get the RCHourglass firmware version";
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
            if (resp.StartsWith("RC Hourglass v"))
            {
                versionInfo = resp;
                try
                {
                    string s = resp.Substring("RC Hourglass v".Length);
                    string[] split = s.Split(' ');
                    string[] split2 = split[0].Split('.');
                    VersionData.Major = Convert.ToInt16(split2[0]);
                    VersionData.Minor = Convert.ToInt16(split2[1]);
                }
                catch
                { }
                VersionData.Beta = resp.ToUpperInvariant().Contains("BETA");
                this.finished = true;
            }
            if (resp.StartsWith("ERROR"))
            {
                this.finished = true;
                errorCause = resp.Substring(6).Trim();
                if (String.IsNullOrEmpty(errorCause)) errorCause = "Generic error";
            }
        }
    }

    public class LicenseCommand : IBasicCommand
    {
        protected String errorCause = String.Empty;
        protected StringBuilder licenseLines = new StringBuilder();
        protected bool finished = false;


        public string LicenseTerms { get { return licenseLines.ToString(); } }

        public string Command
        {
            get
            {
                return "LICENSE";
            }
        }

        public string Description
        {
            get
            {
                return "Get the RCHourglass license info";
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
            if (resp.StartsWith("ERROR"))
            {
                this.finished = true;
                errorCause = resp.Substring(6).Trim();
                if (String.IsNullOrEmpty(errorCause)) errorCause = "Generic error";
                return;
            }
            licenseLines.AppendLine(resp);
           
            if (resp.ToLowerInvariant(). Contains("venturini"))
            {
                 
                this.finished = true;
            }

        }
    }
}
