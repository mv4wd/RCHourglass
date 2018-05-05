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
/// This file contains transponder management commands
/// </summary>

namespace RCHourglassManager
{
    /// <summary>
    /// Command to list all the transponder in the memory of the decoder
    /// </summary>
    public class ListTransponderCommand : IBasicCommand
    {
 



        protected String errorCause = String.Empty;
        protected bool finished = false;
        protected List<TransponderInfo> entries = new List<TransponderInfo>();



        public ListTransponderCommand() {  }

        /// <summary>
        /// Returns the list of entries returned by the command
        /// </summary>
        public IEnumerable<TransponderInfo> Transponders { get { return entries.AsEnumerable<TransponderInfo>();  } }

        public String Command { get { return "LIST"; } }

        public string Description
        {
           get { return "List the transponder registered in the memory of the decoder"; }
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
            if (resp.StartsWith("INFO "))
            {
                string[] s = resp.Substring(5).Split(' ');
                
                Int32 txCandidate = 0;
                if (Int32.TryParse(s[0].Trim(), out txCandidate))
                {
                    bool important = false; 
                    String nick=String.Empty;                    
                    
                    if (s.Length > 1) nick = s[1].Trim();
                    if (s.Length > 2) important = s[2].Trim()=="*";

                    TransponderInfo newEntry = new TransponderInfo(txCandidate, important);
                    newEntry.NickName = nick;
                    this.entries.Add(newEntry);
                }


            }
            if (resp.StartsWith("SUCCESS List complete")) this.finished = true;
            if (resp.StartsWith("ERROR"))
            {
                this.finished = true;
                errorCause = resp.Substring(6).Trim();
                if (String.IsNullOrEmpty(errorCause)) errorCause = "Generic error";
            }
        }

        /// <summary>
        /// Returns the timeout in ms before the command is considered expired without response
        /// </summary>
        public int TimeoutMs { get { return 5000; } }
    }

    /// <summary>
    /// Manually add a transponder to decoder memory
    /// </summary>
    public class TransponderAddCommand : IBasicCommand
    {
        protected String errorCause = String.Empty;
        protected bool finished = false;
        TransponderInfo info = null;

        List<String> warnings = new List<string>();



        public TransponderAddCommand(TransponderInfo info)
        {
            if (info==null || info.TxNumber==0 || info.ImportantPackets?.Count<12)
            {
                throw new ArgumentException("Missing transponder registration info");
            }
            this.info = info;
        }

        /// <summary>
        /// Returns warning messages from decoder
        /// </summary>
        public List<String> WarningInfos {
            get { return warnings;  } }

        public String Command
        {
            get
            {
                StringBuilder s = new StringBuilder();
                s.Append("ADD ");
                if (info.Important) s.Append("I ");
                s.Append(info.TxNumber);
                if (!String.IsNullOrEmpty(info.NickName)) s.Append(" "+info.NickName);
                return s.ToString();
            }
        }

        public string Description
        {
            get { return "Add a transponder to the decoder memory"; }
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

        /// <summary>
        /// Notice that this method loops to write the 12 important pakets to the decoder memory
        /// </summary>
        /// <param name="resp"></param>
        public void HandleStringResponse(string resp)
        {
            if (this.finished) return;
            if (String.IsNullOrEmpty(resp)) return;

            if (resp.StartsWith ("INFO Write packet "))
            {
                // the decoder is waiting for the next important packet
                int packet = -1;
                String sIndex = resp.Substring("INFO Write packet ".Length, 2).Replace(".", "") ;
                
                if (int.TryParse(sIndex, out packet))
                {
                    // Send the next packet
                    if (packet > 12 || packet <= 0)
                    {
                        this.finished = true;
                        errorCause = "Wrong packet index";
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        for(int i = 0; i<10;i++)
                        {
                            sb.Append(Convert.ToString(info.ImportantPackets[packet - 1][i], 16).PadLeft(2, '0'));
                        }
                        
                        ComPortCommandExecutor.Singleton.SendString(sb.ToString());
                    }
                }

            }
            if (resp.StartsWith("WARNING"))
            {
                if (!warnings.Contains(resp.Substring(8))) warnings.Add(resp.Substring(8));
                
            }

            if (resp.StartsWith("SUCCESS ADD")) this.finished = true;
            if (resp.StartsWith("ERROR"))
            {
                this.finished = true;
                errorCause = resp.Substring(6).Trim();
                if (String.IsNullOrEmpty(errorCause)) errorCause = "Generic error";
            }
        }

        /// <summary>
        /// Returns the timeout in ms before the command is considered expired without response
        /// </summary>
        public int TimeoutMs { get { return 5000; } }
    }

    /// <summary>
    /// Delete a transponder form decoder memory
    /// </summary>
    public class TransponderDeleteCommand : IBasicCommand
    {
        protected String errorCause = String.Empty;
        protected bool finished = false;
        int txNumber = 0;
        public TransponderDeleteCommand(int txNumber)
        {
            this.txNumber = txNumber;
        }


        public String Command { get { return "DELETE " + txNumber; } }

        public string Description
        {
            get { return "Delete a transponder from the decoder memory"; }
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
            if (this.finished) return;
            if (String.IsNullOrEmpty(resp)) return;
            
            if (resp.StartsWith("SUCCESS TX deleted")) this.finished = true;
            if (resp.StartsWith("ERROR"))
            {
                this.finished = true;
                errorCause = resp.Substring(6).Trim();
                if (String.IsNullOrEmpty(errorCause)) errorCause = "Generic error";
            }
        }

        /// <summary>
        /// Returns the timeout in ms before the command is considered expired without response
        /// </summary>
        public int TimeoutMs { get { return 1000; } }
    }
    /// <summary>
    /// Get all infos about a registered transponder entry
    /// </summary>
    public class TransponderDetailCommand : IBasicCommand
    {
        protected String errorCause = String.Empty;
        protected bool finished = false;

        TransponderInfo info = new TransponderInfo();


        public TransponderDetailCommand(int txNumber)
        {
            info.TxNumber = txNumber;            
        }

        /// <summary>
        /// Returns the list of entries returned by the command
        /// </summary>
        public TransponderInfo Transponder { get { if (!finished || !String.IsNullOrEmpty(errorCause)) return null; return info; } }

        public String Command { get { return "INFO "+ info.TxNumber; } }

        public string Description
        {
            get { return "Get the details of a transponder registered in the decoder"; }
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
            if (this.finished) return;
            if (String.IsNullOrEmpty(resp)) return;
            
            if (resp.StartsWith("INFO P"))
            {
                string[] args = resp.Substring(6).Split(' ');
                if (args.Length == 2)
                {
                    try
                    {
                        int packetIndex = Convert.ToInt16(args[0]);
                        if (packetIndex != info.ImportantPackets.Count+1) throw new Exception("Telegram index mismatch");
                        if (args[1].Trim().Length!=20) throw new Exception("Telegram data corrupted");
                        byte[] a = new byte[10];
                        for (int i = 0; i<10;i++)
                        {
                            string digit = args[1].Trim().Substring(2 * i, 2);
                            a[i] = (byte)(Convert.ToUInt16(digit, 16) & 0x0FF);
                        }
                        info.ImportantPackets.Add(a);
                    }
                    catch (Exception ex)
                    {
                        this.finished = true;
                        errorCause = "Error decoding transponder telegrams: " + ex.Message;
                    }
                }

            }
            else if (resp.StartsWith("INFO "))
            {
                string[] s = resp.Substring(5).Split(' ');

                Int32 txCandidate = 0;
                if (Int32.TryParse(s[0].Trim(), out txCandidate))
                {
                    bool important = false;
                    String nick = String.Empty;

                    if (s.Length > 1) nick = s[1].Trim();
                    if (s.Length > 2) important = s[2].Trim() == "*";

                    if (txCandidate != info.TxNumber)
                    {
                        this.finished = true;
                        errorCause = "Transponder number mismatch in response";
                        return;
                    }
                    info.NickName = nick;
                    info.Important = important;
                }
            }

            else if (resp.StartsWith("SUCCESS TX info")) this.finished = true;
            else if (resp.StartsWith("ERROR"))
            {
                this.finished = true;
                errorCause = resp.Substring(6).Trim();
                if (String.IsNullOrEmpty(errorCause)) errorCause = "Generic error";
            }
        }

        /// <summary>
        /// Returns the timeout in ms before the command is considered expired without response
        /// </summary>
        public int TimeoutMs { get { return 5000; } }
    }

    /// <summary>
    /// Start the registration procedure.
    /// Response is very chatty
    /// </summary>
    public class TransponderRegisterCommand : IBasicCommand
    {
        protected String errorCause = String.Empty;
        protected bool finished = false;

        TransponderInfo info = new TransponderInfo();
        public List<int> hits = new List<int>();


        public TransponderRegisterCommand(int txNumber, String nickname, bool important)
        {
            if (txNumber < 2097152 || txNumber > 9999999)
                throw new ArgumentException("Transponder number must be between 2097152 and 9999999");
            if (nickname.Length > 8) nickname = nickname.Substring(0, 8);
            foreach (char c in nickname)
            {
                if (!( (c>='A' && c<='Z') || (c >= '0' && c <= '9') || c=='-' || c=='.' || c=='_'))
                {
                    throw new ArgumentException("Invalid nickname character. Valid characters are A-Z 0-9 .-_");
                }
            }
            info.TxNumber = txNumber;
            info.NickName = nickname;
            info.Important = important;
        }

        /// <summary>
        /// Returns the list of entries returned by the command
        /// </summary>
        public TransponderInfo Transponder { get { if (!finished || !String.IsNullOrEmpty(errorCause)) return null; return info; } }

        public String Command
        {
            get
            {
                string imp = info.Important ? " I " : String.Empty;
                return $"REGISTER {imp}{info.TxNumber} {info.NickName}".Trim();
            }
        }

        public string Description
        {
            get { return "Ask the decoder to register a transponder"; }
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
            if (this.finished) return;
            if (String.IsNullOrEmpty(resp)) return;
            if (resp.StartsWith("INFO REGISTER "))
            {
                string[] s = resp.Substring(14).Split(' ');

                Int32 txCandidate = 0;
                if (Int32.TryParse(s[0].Trim(), out txCandidate))
                {
                    if (txCandidate != info.TxNumber)
                    {
                        this.finished = true;
                        errorCause = "Transponder number mismatch in response";
                        return;
                    }                     
                }
            }
            if (resp.StartsWith("INFO P"))
            {
                string[] args = resp.Substring(6).Split(new char[] { ' ','\t'});
                if (args.Length == 4)
                {
                    try
                    {
                        int packetIndex = Convert.ToInt16(args[0]);
                        if (packetIndex != info.ImportantPackets.Count + 1) throw new Exception("Telegram index mismatch");
                        if (args[1].Trim().Length != 20) throw new Exception("Telegram data corrupted");
                        byte[] a = new byte[10];
                        for (int i = 0; i < 10; i++)
                        {
                            string digit = args[1].Trim().Substring(2 * i, 2);
                            a[i] = (byte)(Convert.ToUInt16(digit, 16) & 0x0FF);
                        }
                        info.ImportantPackets.Add(a);
                        this.hits.Add(Convert.ToInt32(args[3].Trim()));
                        
                    }
                    catch (Exception ex)
                    {
                        this.finished = true;
                        errorCause = "Error decoding transponder telegrams: " + ex.Message;
                    }
                }

            }

            if (resp.StartsWith("SUCCESS REGISTER"))
            {
                this.finished = true;
            }
            if (resp.StartsWith("ERROR"))
            {
                this.finished = true;
                errorCause = resp.Substring(6).Trim();
                if (String.IsNullOrEmpty(errorCause)) errorCause = "Generic error";
            }
        }

        /// <summary>
        /// Returns the timeout in ms before the command is considered expired without response
        /// </summary>
        public int TimeoutMs { get { return 30000; } }
    }
}
