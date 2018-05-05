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
/// This file contains commands to read and write the decoder configuration.
/// </summary>

namespace RCHourglassManager
{
    public class BeeperConfigGet : IBasicCommand
    {

        protected String errorCause = String.Empty;

        protected bool finished = false;

        protected int duration = 150, divider = 10;

        public int BeepDuration { get { return duration; } }

        /// <summary>
        /// Returns the divider for the beeper frequency. Freq = 20kHz / divider
        /// </summary>
        public int BeepDivider { get { return divider; } }

        public string Command
        {
            get
            {
                return "GET BEEP";
            }
        }

        public string Description
        {
            get
            {
                return "Read the beep configuration from decoder";
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
            if (resp.StartsWith("SUCCESS BEEP "))
            {
                finished = true;
                try
                {
                    string[] s = resp.Substring("SUCCESS BEEP ".Length).Trim().Split(' ');
                    duration = Convert.ToInt32(s[0]);
                    divider = Convert.ToInt32(s[1]);

                }
                catch (Exception e)
                {
                    errorCause = "Error parsing response for beep configuration. Cause: " + e.Message;

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
        public int TimeoutMs { get { return 1000; } } 
    }

    public class BeeperConfigSet : IBasicCommand
    {

        protected String errorCause = String.Empty;

        protected bool finished = false;

        protected int duration = 0, divider = 0;

        public BeeperConfigSet(int duration, int divider)
        {
            if (duration < 0 || duration > 255) throw new ArgumentException("Beep duration must be between 0 and 255");
            if ((divider < 6 || divider > 30) && divider!=0) throw new ArgumentException("Beep divider must be between 6 and 30");
            this.duration = duration;
            this.divider = divider;
        }

        public string Command
        {
            get
            {
                return $"SET BEEP {duration} {divider}";
            }
        }

        public string Description
        {
            get
            {
                return "Set the beep configuration";
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
            if (resp.StartsWith("SUCCESS BEEP "))
            {
                finished = true;
                try
                {
                    string[] s = resp.Substring("SUCCESS BEEP ".Length).Trim().Split(' ');
                    duration = Convert.ToInt32(s[0]);
                    divider = Convert.ToInt32(s[1]);

                }
                catch (Exception e)
                {
                    errorCause = "Error parsing response for beep configuration. Cause: " + e.Message;

                }


            }
        }
        /// <summary>
        /// Returns the timeout in ms before the command is considered expired without response
        /// </summary>
        public int TimeoutMs { get { return 1000; } }  

    }

    /// <summary>
    /// Command to issue a beep and led feedback
    /// </summary>
    public class Beep : IBasicCommand
    {

        protected String errorCause = String.Empty;

        protected bool finished = false;
 

        public string Command
        {
            get
            {
                return $"BEEP";
            }
        }

        public string Description
        {
            get
            {
                return "Makes the decoder beep and led";
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
            if (resp.StartsWith("SUCCESS BEEP"))
            {
                finished = true;
            }
        }

        /// <summary>
        /// Returns the timeout in ms before the command is considered expired without response
        /// </summary>
        public int TimeoutMs { get { return 1000; } }  
    }



    public class LedConfigGet : IBasicCommand
    {

        protected String errorCause = String.Empty;

        protected bool finished = false;

        protected int duration = 150;


        public int LedDuration { get { return duration; } }

        public string Command
        {
            get
            {
                return "GET LED";
            }
        }

        public string Description
        {
            get
            {
                return "Read the led configuration from decoder";
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
            if (resp.StartsWith("SUCCESS LED "))
            {
                finished = true;
                try
                {
                    duration = Convert.ToInt32(resp.Substring("SUCCESS LED ".Length).Trim());


                }
                catch (Exception e)
                {
                    errorCause = "Error parsing response for led configuration. Cause: " + e.Message;

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
        public int TimeoutMs { get { return 1000; } }
    }

    public class LedConfigSet : IBasicCommand
    {

        protected String errorCause = String.Empty;

        protected bool finished = false;

        protected int duration = 0;

        public LedConfigSet(int duration)
        {
            if (duration < 0 || duration > 255) throw new ArgumentException("Beep duration must be between 0 and 255");
            this.duration = duration;

        }

        public string Command
        {
            get
            {
                return $"SET LED {duration}";
            }
        }

        public string Description
        {
            get
            {
                return "Set the led configuration";
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
            if (resp.StartsWith("SUCCESS LED "))
            {
                finished = true;
                try
                {
                    string[] s = resp.Substring("SUCCESS LED ".Length).Trim().Split(' ');
                    duration = Convert.ToInt32(s[0]);


                }
                catch (Exception e)
                {
                    errorCause = "Error parsing response for led configuration. Cause: " + e.Message;

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
        public int TimeoutMs { get { return 1000; } }
    }


    public class CanoModeSet : IBasicCommand
    {

        protected String errorCause = String.Empty;

        protected bool finished = false;

        public bool decoding ;

        public CanoModeSet(bool withDecoding = true )
        {
            decoding = withDecoding;

        }

        public string Command
        {
            get
            {
                if (decoding) return "CANO MODE";
                else return "CANO CLASSIC MODE";
            }
        }

        public string Description
        {
            get
            {
                return "Set the decoder in Cano decoder emulation";
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
            if (resp.StartsWith("CANO MODE ON") ||
                resp.StartsWith("CANO CLASSIC MODE ON"))
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
        public int TimeoutMs { get { return 1000; } }
    }


    public class RCHourglassModeSet : IBasicCommand
    {

        protected String errorCause = String.Empty;

        protected bool finished = false;

        

        public RCHourglassModeSet()
        {  

        }

        public string Command
        {
            get
            {
                return "RCHOURGLASS MODE";
            }
        }

        public string Description
        {
            get
            {
                return "Set the decoder in RCHourglass mode";
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
            if (resp.StartsWith("RCHOURGLASS MODE ON"))
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
        public int TimeoutMs { get { return 1000; } }
    }

    public class TranxModeSet : IBasicCommand
    {

        protected String errorCause = String.Empty;

        protected bool finished = false;



        public TranxModeSet()
        {

        }

        public string Command
        {
            get
            {
                return "TRANX MODE";
            }
        }

        public string Description
        {
            get
            {
                return "Set the decoder in TRANX mode";
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
            if (resp.StartsWith("TRANX MODE ON"))
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
        public int TimeoutMs { get { return 1000; } }
    }


    public class AmbRcModeSet : IBasicCommand
    {

        protected String errorCause = String.Empty;

        protected bool finished = false;

 

        public AmbRcModeSet()
        {
            

        }

        public string Command
        {
            get
            {
                return "AMBRC MODE";
            }
        }

        public string Description
        {
            get
            {
                return "Set the decoder in AmbRc decoder emulation";
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
            if (resp.StartsWith("AMBRC MODE ON"))  
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
        public int TimeoutMs { get { return 1000; } }
    }

    public class LoopbackModeSet : IBasicCommand
    {

        protected String errorCause = String.Empty;

        protected bool finished = false;



        public LoopbackModeSet()
        {


        }

        public string Command
        {
            get
            {
                return "LOOPBACK MODE";
            }
        }

        public string Description
        {
            get
            {
                return "Set the current port in loopback mode of the other port";
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
            if (resp.StartsWith("LOOPBACK MODE ON"))
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
        public int TimeoutMs { get { return 1000; } }
    }


    // Decoder ID setup

    public class DecoderIdConfigGet : IBasicCommand
    {

        protected String errorCause = String.Empty;

        protected bool finished = false;

        protected int decoderId = 0;


        public int DecoderId { get { return decoderId; } }

        public string Command
        {
            get
            {
                return "GET ID";
            }
        }

        public string Description
        {
            get
            {
                return "Read the decoder id configuration from decoder";
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
            if (resp.StartsWith("SUCCESS GET ID "))
            {
                finished = true;
                try
                {
                    decoderId = Convert.ToInt32(resp.Substring("SUCCESS GET ID ".Length).Trim());
                }
                catch (Exception e)
                {
                    errorCause = "Error parsing response for decoder ID configuration. Cause: " + e.Message;

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
        public int TimeoutMs { get { return 1000; } }
    }

    public class DecoderIdConfigSet : IBasicCommand
    {

        protected String errorCause = String.Empty;

        protected bool finished = false;

        protected int decoderId = 0;

        public DecoderIdConfigSet(int decoderId)
        {
            if (decoderId < 0 || decoderId > 255) throw new ArgumentException("Decoder ID must be between 0 and 255");
            this.decoderId = decoderId;

        }

        public string Command
        {
            get
            {
                return $"SET ID {decoderId}";
            }
        }

        public string Description
        {
            get
            {
                return "Set the decoder ID configuration";
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
            if (resp.StartsWith("SUCCESS ID "))
            {
                finished = true;
                try
                {
                    string[] s = resp.Substring("SUCCESS ID ".Length).Trim().Split(' ');
                    decoderId = Convert.ToInt32(s[0]);


                }
                catch (Exception e)
                {
                    errorCause = "Error parsing response for decoder ID configuration. Cause: " + e.Message;

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
        public int TimeoutMs { get { return 1000; } }
    }



    // Voltage threshold setup

    public class VMinConfigGet : IBasicCommand
    {

        protected String errorCause = String.Empty;

        protected bool finished = false;

        protected int voltage = 0;


        public Decimal Voltage { get { return voltage/10; } }

        public string Command
        {
            get
            {
                return "GET VMIN";
            }
        }

        public string Description
        {
            get
            {
                return "Read the min voltage threshold configuration from decoder";
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
            if (resp.StartsWith("SUCCESS GET VMIN "))
            {
                finished = true;
                try
                {
                    voltage = Convert.ToInt32(resp.Substring("SUCCESS GET VMIN ".Length).Trim());
                }
                catch (Exception e)
                {
                    errorCause = "Error parsing response for voltege threshold configuration. Cause: " + e.Message;

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
        public int TimeoutMs { get { return 1000; } }
    }

    public class VMinConfigSet : IBasicCommand
    {

        protected String errorCause = String.Empty;

        protected bool finished = false;

        protected int voltage = 0;

        public VMinConfigSet(Decimal v)
        {
            if (v < 0 || v > new Decimal(25.5)) throw new ArgumentException("Voltage must be between 0 and 25.5");
            this.voltage = (int)(v*10);

        }

        public string Command
        {
            get
            {
                return $"SET VMIN {voltage}";
            }
        }

        public string Description
        {
            get
            {
                return "Set the min voltage threshold configuration";
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
            if (resp.StartsWith("SUCCESS VMIN "))
            {
                finished = true;
                try
                {
                    string[] s = resp.Substring("SUCCESS VMIN ".Length).Trim().Split(' ');
                    voltage = Convert.ToInt32(s[0]);


                }
                catch (Exception e)
                {
                    errorCause = "Error parsing response for voltage threshold configuration. Cause: " + e.Message;

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
        public int TimeoutMs { get { return 1000; } }
    }




    /// <summary>
    /// Command to set startup mode of the decoder on the two ports
    /// </summary>
    public class SetStartupModeCommand : IBasicCommand
    {

        protected String errorCause = String.Empty;

        protected bool finished = false, serial = false;

        protected String mode = String.Empty;

        public static List<String> modes = new List<String> () { "CANO", "CANO CLASSIC" , "RCHOURGLASS",  "TRANX", "AMBRC" ,"OFF" };

        /// <summary>
        /// Create the setup command
        /// </summary>
        /// <param name="mode">one of  CANO | CANO CLASSIC | RCHOURGLASS | TRANX | AMBRC | OFF</param>
        /// <param name="serial">if true is for serial port, otherwise usb</param>
        public SetStartupModeCommand(String mode, bool serial)
        {
            if (modes.IndexOf(mode) == -1) throw new Exception("Wrong mode " + mode);
            this.mode = mode;
            this.serial = serial;
        }

        public string Command
        {
            get
            {
                return serial? $"SET SERIAL {mode}" : $"SET USB {mode}";
            }
        }

        public string Description
        {
            get
            {
                return "Set the startup mode for a port";
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
            if (serial)
            {
                if (resp.StartsWith("SUCCESS SET SERIAL"))
                {
                     
                    finished = true;
                    return;
                     
                }
            }
            else

            {
                if (resp.StartsWith("SUCCESS SET USB"))
                {
                    
                    finished = true;
                    return;
                     
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
        public int TimeoutMs { get { return 1000; } }
    }


    public class USBStartConfigGet : IBasicCommand
    {

        protected String errorCause = String.Empty;

        protected bool finished = false;

        protected string mode = String.Empty;

        public string Mode { get { return mode; } }

   
        public string Command
        {
            get
            {
                return "GET USB";
            }
        }

        public string Description
        {
            get
            {
                return "Read the USB startup mode from decoder";
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
            if (resp.StartsWith("SUCCESS GET USB "))
            {
                finished = true;
                mode = resp.Substring("SUCCESS GET USB ".Length).Trim();
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
        public int TimeoutMs { get { return 1000; } }
    }


    public class SerialStartConfigGet : IBasicCommand
    {

        protected String errorCause = String.Empty;

        protected bool finished = false;

        protected string mode = String.Empty;

        public string Mode { get { return mode; } }


        public string Command
        {
            get
            {
                return "GET SERIAL";
            }
        }

        public string Description
        {
            get
            {
                return "Read the SERIAL startup mode from decoder";
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
            if (resp.StartsWith("SUCCESS GET SERIAL "))
            {
                finished = true;
                mode = resp.Substring("SUCCESS GET SERIAL ".Length).Trim();
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
        public int TimeoutMs { get { return 1000; } }
    }
}
