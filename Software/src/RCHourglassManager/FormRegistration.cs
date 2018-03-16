using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

namespace RCHourglassManager
{
    /// <summary>
    /// Pop up form for decoder registration
    /// </summary>
    public partial class FormRegistration : Form, RCHourglassCommandListener
    {
        bool addedListener = false;
        public FormRegistration()
        {
            InitializeComponent();
        }

        public void CommandError(IBasicCommand command)
        {
            if (command == null) return;
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate { this.CommandError(command); });
            }
            else
            {
                if (command is TransponderRegisterCommand)
                {
                    labelProgress.Text = "Operation error: " + command.ErrorCause;
                    labelProgress.ForeColor = Color.Red;
                    buttonLearn.Enabled = true;
                }
            }
        }

        public void CommandFinished(IBasicCommand command)
        {
            if (command == null) return;
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate { this.CommandFinished(command); });
            }
            else
            {
                if (command is TransponderRegisterCommand)
                {
                    labelProgress.Text = "Registration completed";
                    buttonLearn.Enabled = true;
                    // hits analysis and transponder registration
                    List<int> hits = (command as TransponderRegisterCommand).hits;
                    if (hits != null && hits.Count > 0)
                    {
                        // Total amount of packets lear is 3276
                        // 1/4 of them are unknown
                        // An amout of 2457 known packets is 100% read for 23 packets
                        // We keep only the 12 most frequent, around 2/3 = 1638
                        // average of 136 hits
                        // If average > 50 % is a good capture
                        // If average > 30 % is an average capute
                        // If average < 30 % is a bad captture
                        // If min < 15 is a bad caputer  

                        int totHits = 0, minHits = -1, maxHits = -1;
                        foreach (int i in hits)
                        {
                            totHits += i;
                            if (minHits == -1) minHits = i;
                            if (maxHits == -1) maxHits = i;
                            if (i < minHits) minHits = i;
                            if (i > maxHits) maxHits = i;
                        }
                        int avgHits = totHits / hits.Count;
                        String captureQuality = String.Empty;
                        if (avgHits > 60) captureQuality = "Good";
                        else if (avgHits > 40) captureQuality = "Average";
                        else captureQuality = "Bad";
                        if (minHits<15 ) captureQuality = "Bad";

                        labelProgress.Text = $"Registration completed. Quality {captureQuality} Hits {totHits} Average {avgHits} Min {minHits}";

                        using (FormRegistrationResult r = new FormRegistrationResult())
                        {
                            r.labelResult.Text = $"Registration completed.\r\nQuality {captureQuality}\r\nHits {totHits}\r\nAverage {avgHits}\r\nMin {minHits}";


                            if (captureQuality.Equals("Bad"))
                            {
                                r.buttonRetry.Visible = true;
                                r.labelResult.Text += "\r\n" + "Capture quality seems low:\r\nYou should try to re-learn\r\nHint: move the transpoder closer to one\r\nof the two wires or slightly outside the loop";
                            }
                            else
                            {
                                r.buttonRetry.Visible = false;

                            }

                            switch (r.ShowDialog(this))
                            {
                                case DialogResult.Yes:
                                    this.textBoxTx.Text = String.Empty;
                                    this.textBoxNick.Text = String.Empty;
                                    break;
                                
                                case DialogResult.Retry:
                                    this.BeginInvoke((MethodInvoker)delegate { this.buttonLearn_Click(null, null); });
                                    break;
                                
                                case DialogResult.Abort:
                                default:
                                    this.BeginInvoke((MethodInvoker)delegate { this.buttonDoneClick(null, null); this.DialogResult = DialogResult.Cancel; });
                                    break;

                            }
                        }
                            
                    }
                    
                }
            }
        }

        public void CommandTimeout(IBasicCommand command)
        {
            if (command == null) return;
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate { this.CommandTimeout(command); });
            }
            else
            {
                if (command is TransponderRegisterCommand)
                {
                    labelProgress.Text = "Command has timed out";
                    labelProgress.ForeColor = Color.Red;
                    buttonLearn.Enabled = true;

                }
            }
        }

       

        private void buttonLearn_Click(object sender, EventArgs e)
        {
            try
            {
                labelProgress.ForeColor = SystemColors.ControlText;
                labelProgress.Text = String.Empty;

                int txNumber = -1;
                if (!int.TryParse(textBoxTx.Text, out txNumber)) throw new Exception("Invalid transponder number");
                TransponderRegisterCommand cmd = new TransponderRegisterCommand(txNumber, textBoxNick.Text.Trim(), checkBoxImportant.Checked);

                if (!addedListener)
                {
                    addedListener = true;
                    ComPortCommandExecutor.Singleton.addListener(this);
                }
                labelProgress.Text = "Learning transponder....";
                ComPortCommandExecutor.Singleton.SendCommand(cmd);
                buttonLearn.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "An error has occurred:\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }


        private void buttonDoneClick(object sender, EventArgs e)
        {
            try
            {
                ComPortCommandExecutor.Singleton.removeListener(this);
                ComPortCommandExecutor.Singleton.SendCommand(new AbortCommand());

            }
            catch
            { }
        }
    }
}
