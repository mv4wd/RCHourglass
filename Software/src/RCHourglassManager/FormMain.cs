using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

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
    /// Main form for the application
    /// </summary>
    public partial class FormManager : Form, RCHourglassCommandListener
    {

        ComPortCommandExecutor executor = new ComPortCommandExecutor();
        List<TransponderInfo> decoderTransponders = new List<TransponderInfo>();
        List<TransponderInfo> transponderDatabase = new List<TransponderInfo>();

        /// <summary>
        /// This is to avoid a warning overlap
        /// </summary>
        DateTime lastWarnDialog = new DateTime(0);

        public FormManager()
        {
            InitializeComponent();            
            refreshCOMPorts();
            executor.addListener(this);
            if (FillDatabase()) refreshTranspondersDatabase();
            this.Text = $"RCHourglass Manager version {Application.ProductVersion} - {Application.CompanyName}";

            for (int i = 6; i<=30; i++)
            {
                float kHz = 20.0f / i;
                comboBoxFreq.Items.Add(kHz.ToString("0.##"));

            }
        }

        
        /// <summary>
        /// The path that contains the transponder archive
        /// </summary>
        public String DBPath { get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RCHourglass Manager");  } }

        /// <summary>
        /// The transponder archive file. Plain XML
        /// </summary>
        public String DBFile
        {
            get { return Path.Combine(DBPath, "TransponderArchive.xml"); }
        }

        /// <summary>
        /// Load or create an empty archive
        /// </summary>
        /// <returns></returns>
        bool FillDatabase()
        {

            if (!Directory.Exists(DBPath))
            {
                try
                {
                    Directory.CreateDirectory(DBPath);
                }
                catch (Exception ex)
                {
                    this.logBox.AppendText("Cannot create directory for transponder database\r\n");
                    this.logBox.AppendText("Directory: "+DBPath+"\r\n");
                    this.logBox.AppendText("Cause: " + ex.Message + "\r\n");
                    return false;
                }
            }
            if (!File.Exists(DBFile))
            {
                try
                {
                    XmlSerializer s = new XmlSerializer(typeof(List<TransponderInfo>));
                    using (FileStream fs = new FileStream(DBFile, FileMode.CreateNew))
                    {
                        List<TransponderInfo> l = new List<TransponderInfo>();
                        s.Serialize(fs, l);
                        fs.Flush();

                    }
                    transponderDatabase.Clear();
                    return true;
                }
                catch (Exception ex)
                {
                    this.logBox.AppendText("Cannot initialize transponder database\r\n");
                    this.logBox.AppendText("File : " + DBFile + "\r\n");
                    this.logBox.AppendText("Cause: " + ex.Message + "\r\n");
                    return false;
                }
            }
            else
            {
                try
                {
                    XmlSerializer s = new XmlSerializer(typeof(List<TransponderInfo>));
                    using (FileStream fs = new FileStream(DBFile, FileMode.Open))
                    {
                        List<TransponderInfo> l = null;
                        l = s.Deserialize(fs) as List<TransponderInfo>;
                        transponderDatabase.Clear();
                        if (l!=null)
                        {                            
                            transponderDatabase.AddRange(l);
                            logBox.AppendText(string.Format("Read {0} transponders from archive\r\n", l.Count));
                        }
                    }
                    
                    return true;
                }
                catch (Exception ex)
                {
                    this.logBox.AppendText("Cannot load transponder database\r\n");
                    this.logBox.AppendText("File : " + DBFile + "\r\n");
                    this.logBox.AppendText("Cause: " + ex.Message + "\r\n");
                    return false;
                }
            }
        }


        /// <summary>
        /// Save the archive
        /// </summary>
        /// <returns></returns>
        bool SaveDatabase()
        {
            try
            {
                XmlSerializer s = new XmlSerializer(typeof(List<TransponderInfo>));
                using (FileStream fs = new FileStream(DBFile, FileMode.OpenOrCreate | FileMode.Truncate))
                {
                    s.Serialize(fs, transponderDatabase);
                    
                    logBox.AppendText("Archive updated\r\n");
                    
                }

                return true;
            }
            catch (Exception ex)
            {
                this.logBox.AppendText("Cannot save transponder database\r\n");
                this.logBox.AppendText("File : " + DBFile + "\r\n");
                this.logBox.AppendText("Cause: " + ex.Message + "\r\n");
                return false;
            }

        }

        /// <summary>
        /// Align the GUI of decoder transponders
        /// </summary>
        void refreshTranspondersList()
        {
            listViewDecoder.Items.Clear();
            foreach (TransponderInfo tx in decoderTransponders)
            {
                ListViewItem i = listViewDecoder.Items.Add(tx.TxNumber.ToString());
                i.SubItems.Add(tx.NickName);
                i.SubItems.Add(tx.Important ? "*" : String.Empty);
            }
        }

        /// <summary>
        /// Align the GUI of history transponders
        /// </summary>
        void refreshTranspondersDatabase()
        {
            listViewDB.Items.Clear();
            foreach (TransponderInfo tx in transponderDatabase)
            {
                ListViewItem i = listViewDB.Items.Add(tx.TxNumber.ToString());
                i.SubItems.Add(tx.NickName);
                i.SubItems.Add(tx.Important ? "*" : String.Empty);
            }
        }

        /// <summary>
        /// Returns the COM ported selected from the combo box
        /// </summary>
        public String SelectedPortName 
        {
            get
            {
                if (this.cbCOMPorts.SelectedIndex > 0)
                {
                    return (this.cbCOMPorts.Items[cbCOMPorts.SelectedIndex] as COMPortInfo)?.Name;
                }
                return cbCOMPorts.Text;
            }
        }

        /// <summary>
        /// Refresh the drop down list for COM ports
        /// </summary>
        void refreshCOMPorts()
        {
            try
            {
                this.cbCOMPorts.Items.Clear();
                this.cbCOMPorts.Items.Add(String.Empty);
                 

                foreach (COMPortInfo comPort in COMPortInfo.GetCOMPortsInfo())
                {
                    this.cbCOMPorts.Items.Add(comPort);
                }
                if (this.cbCOMPorts.Items.Count==1)
                {
                    this.cbCOMPorts.SelectedIndex = -1;

                }
                if (this.cbCOMPorts.Items.Count == 2)
                {
                    this.cbCOMPorts.SelectedIndex = 1;
                }
            }
            catch
            { }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            refreshCOMPorts();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { executor.Dispose(); } catch { }
        }

        /// <summary>
        /// Ask the transponder to report back the list of registered transponders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2RefreshTx(object sender, EventArgs e)
        {
            try
            {
                ListTransponderCommand cmdList = new ListTransponderCommand();
                executor.SendCommand(cmdList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error reading transponders\r\nCause:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }


        /// <summary>
        /// First connecton. Ask version , license, registered transponders and setup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.labelVersion.Text = String.Empty;
            if (String.IsNullOrEmpty(SelectedPortName))
            {
                MessageBox.Show("No COM Port selected");
                return;
            }

            try
            {
                executor.SetPortName(SelectedPortName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error opening port " + SelectedPortName + "\r\nCause:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                VersionCommand v = new VersionCommand();
                executor.SendCommand(v);


                LicenseCommand l = new LicenseCommand();
                executor.SendCommand(l);

                ListTransponderCommand cmdList = new ListTransponderCommand();
                executor.SendCommand(cmdList);

                executor.SendCommand(new BeeperConfigGet());
                executor.SendCommand(new LedConfigGet());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error connecting to decoder\r\nCause:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }


        /// <summary>
        /// Callback called at command completion
        /// </summary>
        /// <param name="command"></param>
        public void CommandFinished(IBasicCommand command)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate { this.CommandFinished(command); });
            }
            else
            {
                if (command is VersionCommand)
                {
                    labelVersion.Text = (command as VersionCommand).Version;
                    logBox.AppendText("Decoder detected: " + (command as VersionCommand).Version + "\r\n");
                    return;
                }

                if (command is LicenseCommand)
                {
                    using (FormAbout a = new FormAbout())
                    {
                        a.licenseTextBox.Text = (command as LicenseCommand).LicenseTerms;                         
                        a.ShowDialog(this);
                    }
                    return;
                }

                if (command is ListTransponderCommand)
                {
                    // Refresh the trasnponder list 
                    decoderTransponders.Clear();
                    decoderTransponders.AddRange((command as ListTransponderCommand).Transponders);
                    refreshTranspondersList();
                    if (decoderTransponders.Count == 0)
                    {
                        logBox.AppendText("No transponders registered\r\n");
                    }
                    else
                    {
                        logBox.AppendText(string.Format("Read {0} transponders from decoder\r\n", decoderTransponders.Count));

                        // Read details from all the transponders currently not in the Archive.
                        foreach (TransponderInfo t in (command as ListTransponderCommand).Transponders)
                        {
                            if (!transponderDatabase.Exists(x => x.TxNumber == t.TxNumber))
                            {
                                // Add a command to have transponder info details
                                executor.SendCommand(new TransponderDetailCommand(t.TxNumber));
                            }
                        }
                    }

                    return;
                }
                if (command is TransponderDetailCommand)
                {
                    TransponderInfo learn = (command as TransponderDetailCommand).Transponder;
                    if (learn == null || learn.TxNumber == 0 || learn.ImportantPackets?.Count == 0) return;
                    if (!transponderDatabase.Exists(x => x.TxNumber == learn.TxNumber))
                    {
                        // Grab the transponder from the Decoder. It is new

                        transponderDatabase.Add(learn);
                    }
                    else
                    {
                        // Update the existing transponder from the decoder
                        bool found = false;
                        for (int i = 0; i < transponderDatabase.Count; i++)
                        {
                            if (transponderDatabase[i].TxNumber == learn.TxNumber)
                            {
                                if (!found)
                                {
                                    transponderDatabase[i] = learn;
                                    found = true;
                                }
                            }
                        }
                    }
                    SaveDatabase();
                    refreshTranspondersDatabase();
                    return;
                }
                if (command is BeeperConfigGet)
                {
                    this.textBoxBeep.Text = (command as BeeperConfigGet).BeepDuration.ToString();
                    int comboIndex = (command as BeeperConfigGet).BeepDivider -6;
                    if (comboIndex>=0 && comboIndex < comboBoxFreq.Items.Count)
                    {
                        comboBoxFreq.SelectedIndex = comboIndex;
                    }
                    buttonApplyConfig.Enabled = true;
                    buttonRefreshConfig.Enabled = true;
                    return;
                }
                if (command is LedConfigGet)
                {
                    this.textBoxLED.Text = (command as LedConfigGet).LedDuration.ToString();
                    buttonApplyConfig.Enabled = true;
                    buttonRefreshConfig.Enabled = true;
                    return;
                }
                if (command is PicDetectCommand)
                {
                    MessageBox.Show(this, $"Pic detected. Model ID {(command as PicDetectCommand).PicModelId}\r\nPart name: {(command as PicDetectCommand).PicPartNumber}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (command is CanoModeSet)
                {
                    MessageBox.Show(this, "Switched to CANO mode", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (command is AmbRcModeSet)
                {
                    MessageBox.Show(this, "Switched to AmbRc mode (experimental).\r\nNOTICE: decoder will revert to CANO mode if USB is disconnected", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (command is TransponderAddCommand)
                {

                    List<String> w = (command as TransponderAddCommand).WarningInfos;
                    if (w.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (String s in w) { logBox.AppendText(s + "\r\n"); sb.AppendLine(s); }
                        if (lastWarnDialog.AddSeconds(10) < DateTime.Now)
                        {
                            MessageBox.Show(this, "Check log detail for warnings\r\n"+ sb.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            lastWarnDialog = DateTime.Now;
                        }
                    }
                    return;
                }
            }
        }

        public void CommandTimeout(IBasicCommand command)
        {
            // TO DO 
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
                logBox.AppendText("Command error:" + command.ErrorCause + "\r\n");
                if(command is PicDetectCommand || command is AmbRcModeSet || command is CanoModeSet)
                {
                    MessageBox.Show(this, command.ErrorCause, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int selCount = listViewDecoder.SelectedItems.Count;
            if (selCount==0)
            {
                MessageBox.Show("No transponder selected");
                return;
            }
            try
            {
                if (DialogResult.Yes == MessageBox.Show(this, $"Do you really want to remove {selCount} transponder from decoder memory?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
                {
                    List<IBasicCommand> cmds = new List<IBasicCommand>();
                    for (int i = 0; i < selCount; i++)
                    {
                        try
                        {
                            int tx = Convert.ToInt32(listViewDecoder.SelectedItems[i].Text);
                            cmds.Add(new TransponderDeleteCommand(tx));
                        }
                        catch { }
                    }
                    cmds.Add(new ListTransponderCommand());

                    foreach (IBasicCommand c in cmds) executor.SendCommand(c);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error deleting transponder\r\nCause:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void buttonAddTxToDecoder_Click(object sender, EventArgs e)
        {
            if (!executor.isPortConnected)
            {
                MessageBox.Show("Not connected");
                return;
            }
            int selCount = listViewDB.SelectedItems.Count;
            if (selCount == 0)
            {
                MessageBox.Show("No transponder selected");
                return;
            }
            try
            {
                lastWarnDialog = new DateTime(0);
                if (DialogResult.Yes == MessageBox.Show(this, $"Do you want to add {selCount} transponders to the decoder memory?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
                {
                    List<IBasicCommand> cmds = new List<IBasicCommand>();
                    for (int i = 0; i < selCount; i++)
                    {
                        try
                        {
                            int tx = Convert.ToInt32(listViewDB.SelectedItems[i].Text);
                            TransponderInfo info = (from TransponderInfo t in this.transponderDatabase where t.TxNumber == tx select t).FirstOrDefault();
                            if (info!=null) cmds.Add(new TransponderAddCommand(info));
                        }
                        catch { }
                    }
                    cmds.Add(new ListTransponderCommand());

                    foreach (IBasicCommand c in cmds) executor.SendCommand(c);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error adding transponders\r\nCause:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        /// <summary>
        /// Pop up the registration dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if (!executor.isPortConnected)
            {
                MessageBox.Show("Not connected");
                return;
            }
            using (FormRegistration r = new FormRegistration())
            {
                 
                r.ShowDialog(this);
                try { executor.SendCommand(new ListTransponderCommand()); } catch { }
            }
        }

        private void buttonDeleteSelected_Click(object sender, EventArgs e)
        {
            int selCount = listViewDB.SelectedItems.Count;
            if (selCount == 0)
            {
                MessageBox.Show("No transponder selected");
                return;
            }
            try
            {
                if (DialogResult.Yes == MessageBox.Show(this, $"Do you really want to remove {selCount} transponder from the history archive?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
                {
                     
                    for (int i = 0; i < selCount; i++)
                    {
                        try
                        {
                            int tx = Convert.ToInt32(listViewDB.SelectedItems[i].Text);
                            transponderDatabase.RemoveAll(x => x.TxNumber == tx);
                        }
                        catch { }
                    }
                    SaveDatabase();
                    refreshTranspondersDatabase();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error deleting transponder\r\nCause:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void buttonRefreshFromDecoder_Click(object sender, EventArgs e)
        {
            if (!executor.isPortConnected)
            {
                MessageBox.Show("Not connected");
                return;
            }

            int selCount = listViewDecoder.Items.Count;
            if (selCount == 0)
            {
                MessageBox.Show("No transponder in the decoder. Refresh");
                return;
            }

            try
            {
                if (DialogResult.Yes == MessageBox.Show(this, $"Do you want to update the archive database with the data from the decoder?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
                {

                    List<IBasicCommand> cmds = new List<IBasicCommand>();
                    for (int i = 0; i < selCount; i++)
                    {
                        try
                        {
                            int tx = Convert.ToInt32(listViewDecoder.Items[i].Text);
                             cmds.Add(new TransponderDetailCommand(tx));
                        }
                        catch { }
                    }
                    cmds.Add(new ListTransponderCommand());

                    foreach (IBasicCommand c in cmds) executor.SendCommand(c);

                

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error refreshing transponders.\r\nCause:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void tabPageSetup_Enter(object sender, EventArgs e)
        {

            try
            {
                 
                buttonApplyConfig.Enabled = executor != null ? executor.isPortConnected : false;
                buttonRefreshConfig.Enabled = executor != null ? executor.isPortConnected : false;
                if (executor != null && executor.isPortConnected)
                {
                    executor.SendCommand(new BeeperConfigGet());
                    executor.SendCommand(new LedConfigGet());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error refreshing configuration.\r\nCause:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void buttonRefreshConfig_Click(object sender, EventArgs e)
        {
            // Refresh the data
            tabPageSetup_Enter(null, null);
        }

        private void buttonApplyConfig_Click(object sender, EventArgs e)
        {
            try
            {
                int ld = -1, bd = -1;
                try { ld = Convert.ToInt32(textBoxLED.Text.Trim()); } catch { }
                try { bd = Convert.ToInt32(textBoxBeep.Text.Trim()); } catch { }
                LedConfigSet c1 = new LedConfigSet(ld);
                BeeperConfigSet c2 = new BeeperConfigSet(bd, 6+comboBoxFreq.SelectedIndex);

                executor.SendCommand(c1);
                executor.SendCommand(c2);
                executor.SendCommand(new BeeperConfigGet());
                executor.SendCommand(new LedConfigGet());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error setting configuration.\r\nCause:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void buttonDetectPIC_Click(object sender, EventArgs e)
        {
            if (!executor.isPortConnected)
            {
                MessageBox.Show("Not connected");
                return;
            }
            try
            {
                executor.SendCommand(new PicDetectCommand());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error reading PIC model.\r\nCause:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

        }

        private void buttonAddAll_Click(object sender, EventArgs e)
        {
            if (!executor.isPortConnected)
            {
                MessageBox.Show("Not connected");
                return;
            }
            int selCount = listViewDB.Items.Count;
            if (selCount == 0)
            {
                MessageBox.Show("No transponder in the archive");
                return;
            }
            try
            {
                lastWarnDialog = new DateTime(0);
                if (DialogResult.Yes == MessageBox.Show(this, $"Do you want to add {selCount} transponders to the decoder memory?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
                {
                    List<IBasicCommand> cmds = new List<IBasicCommand>();
                    for (int i = 0; i < selCount; i++)
                    {
                        try
                        {
                            int tx = Convert.ToInt32(listViewDB.Items[i].Text);
                            TransponderInfo info = (from TransponderInfo t in this.transponderDatabase where t.TxNumber == tx select t).FirstOrDefault();
                            if (info != null) cmds.Add(new TransponderAddCommand(info));
                        }
                        catch { }
                    }
                    cmds.Add(new ListTransponderCommand());

                    foreach (IBasicCommand c in cmds) executor.SendCommand(c);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error adding transponders\r\nCause:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void buttonCanoMode_Click(object sender, EventArgs e)
        {
            if (!executor.isPortConnected)
            {
                MessageBox.Show("Not connected");
                return;
            }
            try
            {
                executor.SendCommand(new CanoModeSet());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error setting mode.\r\nCause:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void buttonAmbRcMode_Click(object sender, EventArgs e)
        {
            if (!executor.isPortConnected)
            {
                MessageBox.Show("Not connected");
                return;
            }
            try
            {
                executor.SendCommand(new AmbRcModeSet());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error setting mode.\r\nCause:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void buttonOpenDB_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog d = new OpenFileDialog())
                {
                    d.CheckFileExists = true;
                    d.CheckPathExists = true;
                    d.Filter = "(*.xml)|*.xml";
                    d.Multiselect = false;
                    d.Title = "Import transponders from file";
                    if (d.ShowDialog() == DialogResult.OK)
                    {
                        XmlSerializer s = new XmlSerializer(typeof(List<TransponderInfo>));
                        using (Stream fs = d.OpenFile())
                        {
                            List<TransponderInfo> l = null;
                            try
                            {
                                l = s.Deserialize(fs) as List<TransponderInfo>;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(this, "Invalid transponder archive.\r\nCause:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }

                            if (l != null)
                            {
                                // transponderDatabase.AddRange(l);
                                logBox.AppendText(string.Format("Read {0} transponders from archive\r\n", l.Count));
                                int collision = 0;
                                bool overwrite = false;
                                foreach(TransponderInfo t in l)
                                {
                                    if (transponderDatabase.Find(x => x.TxNumber == t.TxNumber)!=null)
                                    {
                                        collision++;

                                    }
                                }
                                if (collision>0)
                                {
                                    switch (MessageBox.Show(this, $"The archive contains {l.Count} transponders.\r\n{collision} transponders already in the history.\r\nDo you want to update them?\r\nYes = Update from archive   No = Keep my registration data", "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Stop))
                                    {
                                        case DialogResult.Yes:
                                            overwrite = true;
                                            break;
                                        case DialogResult.No:
                                            break;
                                        default: return;
                                    }
                                }
                                else
                                {
                                    if (DialogResult.Yes != MessageBox.Show(this, $"The archive contains {l.Count} transponders.\r\nDo you want to add them to the history?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Stop))
                                    {
                                        return;
                                    }
                                }
                                foreach (TransponderInfo t in l)
                                {
                                    TransponderInfo existing = transponderDatabase.Find(x => x.TxNumber == t.TxNumber);
                                    if (existing != null)
                                    {
                                        if (!overwrite) continue;
                                        transponderDatabase[transponderDatabase.IndexOf(existing)] = t;
                                    }
                                    else
                                    {
                                        transponderDatabase.Add(t);
                                    }                                     
                                }
                                SaveDatabase();
                                refreshTranspondersDatabase();
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error opening archive.\r\nCause:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void buttonSaveDB_Click(object sender, EventArgs e)
        {
            try
            {
                if (transponderDatabase.Count == 0)
                {
                    MessageBox.Show("Empty history...cannot save");
                    return;
                }
                using (SaveFileDialog d = new SaveFileDialog())
                {

                    d.OverwritePrompt = true;
                    d.CheckPathExists = true;
                    d.Filter = "(*.xml)|*.xml";
                     
                    d.Title = "Save transponders to file";
                    if (d.ShowDialog() == DialogResult.OK)
                    {
                        XmlSerializer s = new XmlSerializer(typeof(List<TransponderInfo>));
                        using (Stream fs = d.OpenFile())
                        {
                            s.Serialize(fs, transponderDatabase);

                            logBox.AppendText("Archive exported\r\n");
 
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error saving archive.\r\nCause:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }


        /// <summary>
        /// Generate 40 random racers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDebug_Click(object sender, EventArgs e)
        {
            
            Random r = new Random();
            for (int i= 1; i<=40;i++)
            {

                int txNumber = r.Next(2097152, 9999999);
                TransponderInfo t = new TransponderInfo(txNumber, false);
                t.NickName = $"MR {i}";
                for (int j=0;j<12;j++)
                {
                    byte[] imp = new byte[10];
                    r.NextBytes(imp);
                    t.ImportantPackets.Add(imp);
                }
                transponderDatabase.Add(t);
            }
            refreshTranspondersDatabase();
        }
    }
}
