using System;
using System.Management;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;




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
/// This file contains serial port utility methods
/// </summary>
namespace RCHourglassManager
{

    /// <summary>
    /// WMI utilities to read available COM ports
    /// </summary>
    internal class COMPortUtils
    {
        public static ConnectionOptions ProcessConnectionOptions()
        {

            ConnectionOptions options = new ConnectionOptions();
            options.Impersonation = ImpersonationLevel.Impersonate;
            options.Authentication = AuthenticationLevel.Default;
            options.EnablePrivileges = true;
            return options;
        }



        public static ManagementScope ConnectionScope(string machineName, ConnectionOptions options, string path)
        {
            ManagementScope connectScope = new ManagementScope();
            connectScope.Path = new ManagementPath(@"\\" + machineName + path);
            connectScope.Options = options;
            connectScope.Connect();
            return connectScope;
        }

    }


    /// <summary>
    /// Serial port utilities
    /// </summary>
    public class COMPortInfo

    {

        public string Name { get; set; }

        public string Description { get; set; }

        public override string ToString() { return string.Format("{0} – {1}", Name, Description); }



        public COMPortInfo() { }



        public static List<COMPortInfo> GetCOMPortsInfo()

        {

            List<COMPortInfo> comPortInfoList = new List<COMPortInfo>();



            ConnectionOptions options = COMPortUtils.ProcessConnectionOptions();

            ManagementScope connectionScope = COMPortUtils.ConnectionScope(Environment.MachineName, options, @"\root\CIMV2");



            ObjectQuery objectQuery = new ObjectQuery("SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0");

            ManagementObjectSearcher comPortSearcher = new ManagementObjectSearcher(connectionScope, objectQuery);



            using (comPortSearcher)

            {

                string caption = null;

                foreach (ManagementObject obj in comPortSearcher.Get())

                {

                    if (obj != null)

                    {

                        object captionObj = obj["Caption"];

                        if (captionObj != null)

                        {

                            caption = captionObj.ToString();

                            if (caption.Contains("(COM"))

                            {

                                COMPortInfo comPortInfo = new COMPortInfo();

                                comPortInfo.Name = caption.Substring(caption.LastIndexOf("(COM")).Replace("(", string.Empty).Replace(")",

                                                                     string.Empty);

                                comPortInfo.Description = caption;

                                comPortInfoList.Add(comPortInfo);

                            }

                        }

                    }

                }

            }

            return comPortInfoList;

        }
    }



    /// <summary>
    /// Serial Port command executor
    /// </summary>
    public class ComPortCommandExecutor : IDisposable
    {

        String currentName = String.Empty;
        SerialPort thePort = null;
        byte[] buffer = new byte[4096];

        Action kickoffRead = null;
        List<IBasicCommand> commandQueue = new List<IBasicCommand>();
        List<RCHourglassCommandListener> listeners = new List<RCHourglassCommandListener>();


        static ComPortCommandExecutor theSingleton = null;

        public static ComPortCommandExecutor Singleton { get { return theSingleton; } }

        public bool isPortConnected {  get { return thePort != null; } }

        public ComPortCommandExecutor()
        {
            if (theSingleton == null) theSingleton = this;
        }

        public void addListener (RCHourglassCommandListener l)
        {
            if (!listeners.Contains(l)) listeners.Add(l);

        }

        public void removeListener(RCHourglassCommandListener l)
        {
            while (listeners.Contains(l)) listeners.Remove(l);

        }


        public void SetPortName(String comName)
        {
            if (!String.Equals(currentName, comName) || thePort == null)
            {
                if (thePort != null) try { thePort.Close(); } catch { }
                this.currentName = comName;

                try
                {
                    thePort = new SerialPort(currentName);
                    thePort.Parity = Parity.None;
                    thePort.DataBits = 8;
                    thePort.StopBits = StopBits.One;
                    thePort.BaudRate = 115200;
                    thePort.Open();
                    kickoffRead = delegate
                    {
                        thePort.BaseStream.BeginRead(buffer, 0, buffer.Length, delegate (IAsyncResult ar)
                        {
                            try
                            {
                                int actualLength = thePort.BaseStream.EndRead(ar);
                                byte[] received = new byte[actualLength];
                                Buffer.BlockCopy(buffer, 0, received, 0, actualLength);
                                raiseAppSerialDataEvent(received);
                            }
                            catch (IOException exc)
                            {
                                handleAppSerialError(exc);
                            }
                            kickoffRead();
                        }, null);
                    };
                    kickoffRead();

                    LineReceived += ComPortCommandExecutor_LineReceived;
                }
                catch
                {
                    try { thePort.Close(); } catch { }
                    thePort = null;
                    throw;

                }
            }
        }

        private void notifyCommandFinished(IBasicCommand finishedCommand)
        {
            if (finishedCommand.WasSuccessfull)
            {

                foreach (RCHourglassCommandListener l in listeners)
                {
                    try
                    {

                        l.CommandFinished(finishedCommand);
                    }
                    catch
                    { }

                }
            }
            else
            {
                foreach (RCHourglassCommandListener l in listeners)
                {
                    try { l.CommandError(finishedCommand); } catch { }

                }

            }
        }

        private void ComPortCommandExecutor_LineReceived(byte[] arr)
        {
            String s = Encoding.ASCII.GetString(arr);
            s = s.Replace("\r", "").Replace("\n",""); // Remove endline

            if (commandQueue.Count>0)
            {
                commandQueue[0].HandleStringResponse(s);
                if (commandQueue[0].HasFinished)
                {
                    // Correct command execution
                    IBasicCommand finishedCommand = commandQueue[0];


                    commandQueue.RemoveAt(0);

                    notifyCommandFinished(finishedCommand);

                    if (commandQueue.Count > 0)
                    {
                        byte[] msg = Encoding.ASCII.GetBytes(commandQueue[0].Command + "\r\n");
                        thePort.BaseStream.BeginWrite(msg, 0, msg.Length, null, commandQueue[0]);
                        if (commandQueue[0].HasFinished)
                        {
                            finishedCommand = commandQueue[0];
                            commandQueue.RemoveAt(0);
                            notifyCommandFinished(finishedCommand);
                        }
                    }

                }
            }

        }

        public delegate void LineRecevedDelegate(byte[] a);

        public event LineRecevedDelegate LineReceived;
        public byte Delimiter = (byte)'\n';
        byte[] leftover;




        /// <summary>
        /// An execution errorn the serial line happened!
        /// </summary>
        /// <param name="ex"></param>
        void handleAppSerialError(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        static byte[] ConcatArray(byte[] head, byte[] tail, int tailOffset, int tailCount)
        {
            byte[] result;
            if (head == null)
            {
                result = new byte[tailCount];
                Array.Copy(tail, tailOffset, result, 0, tailCount);
            }
            else
            {
                result = new byte[head.Length + tailCount];
                head.CopyTo(result, 0);
                Array.Copy(tail, tailOffset, result, head.Length, tailCount);
            }

            return result;
        }

        void raiseAppSerialDataEvent(byte[] buffer)
        {
            int offset = 0;
            while (true)
            {
                int newlineIndex = Array.IndexOf(buffer, Delimiter, offset);
                if (newlineIndex < offset)
                {
                    leftover = ConcatArray(leftover, buffer, offset, buffer.Length - offset);
                    return;
                }
                ++newlineIndex;


                byte[] full_line = ConcatArray(leftover, buffer, offset, newlineIndex - offset);
                leftover = null;
                offset = newlineIndex;
                LineReceived?.Invoke(full_line); // raise an event for further processing
            }


        }

        public void SendCommand(IBasicCommand cmd)
        {
            if (cmd == null) return;
            if (thePort == null) throw new Exception("COM Port not connected!");
            commandQueue.Add(cmd);
            // No message in queue.... send direct
            if (commandQueue.Count == 1)
            {
                byte[] msg = Encoding.ASCII.GetBytes(cmd.Command + "\r\n");
                thePort.BaseStream.BeginWrite(msg, 0, msg.Length, null, cmd);
                if (commandQueue[0].HasFinished)
                {
                    IBasicCommand finishedCommand = commandQueue[0];
                    commandQueue.RemoveAt(0);
                    notifyCommandFinished(finishedCommand);
                }
            }
        }

        /// <summary>
        /// Method to send directly a string
        /// </summary>
        /// <param name="s"></param>
        public void SendString(string s)
        {
            if (thePort == null) throw new Exception("COM Port not connected!");
            byte[] msg = Encoding.ASCII.GetBytes(s + "\r\n");
            thePort.BaseStream.BeginWrite(msg, 0, msg.Length, null, null);
        }


        public void Dispose()
        {
            if (theSingleton == this) theSingleton = null;
        }

    }


}
