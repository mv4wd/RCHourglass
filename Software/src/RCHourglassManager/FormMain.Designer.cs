namespace RCHourglassManager
{
    partial class FormManager
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormManager));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.cbCOMPorts = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageTransponders = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.listViewDecoder = new System.Windows.Forms.ListView();
            this.columnTx = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnNick = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnImportant = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel9 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonRefreshTransponders = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.listViewDB = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel7 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonAddTxToDecoder = new System.Windows.Forms.Button();
            this.buttonAddAll = new System.Windows.Forms.Button();
            this.buttonDeleteSelected = new System.Windows.Forms.Button();
            this.buttonRefreshFromDecoder = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.buttonSaveDB = new System.Windows.Forms.Button();
            this.buttonOpenDB = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPageSetup = new System.Windows.Forms.TabPage();
            this.buttonAmbRcMode = new System.Windows.Forms.Button();
            this.buttonCanoMode = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxFreq = new System.Windows.Forms.ComboBox();
            this.buttonRefreshConfig = new System.Windows.Forms.Button();
            this.buttonApplyConfig = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxBeep = new System.Windows.Forms.TextBox();
            this.textBoxLED = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPagePIC = new System.Windows.Forms.TabPage();
            this.buttonDetectPIC = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.buttonDebug = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageTransponders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel9.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel7.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tabPageSetup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPagePIC.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.labelVersion);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(753, 48);
            this.panel1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(130, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Decoder firmware:";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.Location = new System.Drawing.Point(130, 21);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(0, 16);
            this.labelVersion.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(107, 40);
            this.button3.TabIndex = 2;
            this.button3.Text = "Connect";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.cbCOMPorts);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(314, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(439, 48);
            this.panel2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.AccessibleDescription = "Refresh COM Port List";
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(353, 5);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 34);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // cbCOMPorts
            // 
            this.cbCOMPorts.FormattingEnabled = true;
            this.cbCOMPorts.Location = new System.Drawing.Point(85, 13);
            this.cbCOMPorts.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbCOMPorts.Name = "cbCOMPorts";
            this.cbCOMPorts.Size = new System.Drawing.Size(260, 24);
            this.cbCOMPorts.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Serial Port";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageTransponders);
            this.tabControl1.Controls.Add(this.tabPageSetup);
            this.tabControl1.Controls.Add(this.tabPagePIC);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(753, 256);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPageTransponders
            // 
            this.tabPageTransponders.Controls.Add(this.splitContainer1);
            this.tabPageTransponders.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageTransponders.Location = new System.Drawing.Point(4, 29);
            this.tabPageTransponders.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageTransponders.Name = "tabPageTransponders";
            this.tabPageTransponders.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageTransponders.Size = new System.Drawing.Size(745, 223);
            this.tabPageTransponders.TabIndex = 0;
            this.tabPageTransponders.Text = "Transponders";
            this.tabPageTransponders.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(4, 5);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel8);
            this.splitContainer1.Panel1.Controls.Add(this.panel3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel11);
            this.splitContainer1.Panel2.Controls.Add(this.panel7);
            this.splitContainer1.Panel2.Controls.Add(this.panel6);
            this.splitContainer1.Size = new System.Drawing.Size(737, 213);
            this.splitContainer1.SplitterDistance = 299;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.panel10);
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 50);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(299, 163);
            this.panel8.TabIndex = 4;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.listViewDecoder);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(299, 126);
            this.panel10.TabIndex = 4;
            // 
            // listViewDecoder
            // 
            this.listViewDecoder.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnTx,
            this.columnNick,
            this.columnImportant});
            this.listViewDecoder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewDecoder.FullRowSelect = true;
            this.listViewDecoder.HideSelection = false;
            this.listViewDecoder.Location = new System.Drawing.Point(0, 0);
            this.listViewDecoder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listViewDecoder.Name = "listViewDecoder";
            this.listViewDecoder.ShowGroups = false;
            this.listViewDecoder.Size = new System.Drawing.Size(299, 126);
            this.listViewDecoder.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewDecoder.TabIndex = 2;
            this.listViewDecoder.UseCompatibleStateImageBehavior = false;
            this.listViewDecoder.View = System.Windows.Forms.View.Details;
            // 
            // columnTx
            // 
            this.columnTx.Tag = "";
            this.columnTx.Text = "Transponder";
            this.columnTx.Width = 101;
            // 
            // columnNick
            // 
            this.columnNick.Tag = "";
            this.columnNick.Text = "Nickname";
            this.columnNick.Width = 96;
            // 
            // columnImportant
            // 
            this.columnImportant.Tag = "";
            this.columnImportant.Text = "Important";
            this.columnImportant.Width = 67;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.flowLayoutPanel2);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel9.Location = new System.Drawing.Point(0, 126);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(299, 37);
            this.panel9.TabIndex = 3;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.buttonRegister);
            this.flowLayoutPanel2.Controls.Add(this.button8);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(299, 37);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // buttonRegister
            // 
            this.buttonRegister.AccessibleDescription = "Register a new transponder";
            this.buttonRegister.Location = new System.Drawing.Point(3, 3);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(168, 31);
            this.buttonRegister.TabIndex = 0;
            this.buttonRegister.Text = "Register New";
            this.buttonRegister.UseVisualStyleBackColor = true;
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
            // 
            // button8
            // 
            this.button8.AccessibleDescription = "Remove the selected transponders from decoder memory";
            this.button8.Location = new System.Drawing.Point(177, 3);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(101, 31);
            this.button8.TabIndex = 2;
            this.button8.Text = "Delete Selected";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.buttonRefreshTransponders);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(299, 50);
            this.panel3.TabIndex = 3;
            // 
            // buttonRefreshTransponders
            // 
            this.buttonRefreshTransponders.AccessibleDescription = "Refresh list of registered transponders in the decoder";
            this.buttonRefreshTransponders.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefreshTransponders.Image")));
            this.buttonRefreshTransponders.Location = new System.Drawing.Point(172, 7);
            this.buttonRefreshTransponders.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonRefreshTransponders.Name = "buttonRefreshTransponders";
            this.buttonRefreshTransponders.Size = new System.Drawing.Size(34, 34);
            this.buttonRefreshTransponders.TabIndex = 4;
            this.buttonRefreshTransponders.UseVisualStyleBackColor = true;
            this.buttonRefreshTransponders.Click += new System.EventHandler(this.button2RefreshTx);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Transponders in decoder";
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.listViewDB);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(0, 50);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(298, 163);
            this.panel11.TabIndex = 5;
            // 
            // listViewDB
            // 
            this.listViewDB.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewDB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewDB.FullRowSelect = true;
            this.listViewDB.HideSelection = false;
            this.listViewDB.Location = new System.Drawing.Point(0, 0);
            this.listViewDB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listViewDB.Name = "listViewDB";
            this.listViewDB.ShowGroups = false;
            this.listViewDB.Size = new System.Drawing.Size(298, 163);
            this.listViewDB.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewDB.TabIndex = 3;
            this.listViewDB.UseCompatibleStateImageBehavior = false;
            this.listViewDB.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Tag = "";
            this.columnHeader1.Text = "Transponder";
            this.columnHeader1.Width = 101;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Tag = "";
            this.columnHeader2.Text = "Nickname";
            this.columnHeader2.Width = 96;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Tag = "";
            this.columnHeader3.Text = "Important";
            this.columnHeader3.Width = 67;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.flowLayoutPanel1);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel7.Location = new System.Drawing.Point(298, 50);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(135, 163);
            this.panel7.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.buttonAddTxToDecoder);
            this.flowLayoutPanel1.Controls.Add(this.buttonAddAll);
            this.flowLayoutPanel1.Controls.Add(this.buttonDeleteSelected);
            this.flowLayoutPanel1.Controls.Add(this.buttonRefreshFromDecoder);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(135, 163);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // buttonAddTxToDecoder
            // 
            this.buttonAddTxToDecoder.Location = new System.Drawing.Point(3, 3);
            this.buttonAddTxToDecoder.Name = "buttonAddTxToDecoder";
            this.buttonAddTxToDecoder.Size = new System.Drawing.Size(128, 52);
            this.buttonAddTxToDecoder.TabIndex = 3;
            this.buttonAddTxToDecoder.Text = "Add selected to decoder";
            this.buttonAddTxToDecoder.UseVisualStyleBackColor = true;
            this.buttonAddTxToDecoder.Click += new System.EventHandler(this.buttonAddTxToDecoder_Click);
            // 
            // buttonAddAll
            // 
            this.buttonAddAll.Location = new System.Drawing.Point(3, 61);
            this.buttonAddAll.Name = "buttonAddAll";
            this.buttonAddAll.Size = new System.Drawing.Size(128, 23);
            this.buttonAddAll.TabIndex = 0;
            this.buttonAddAll.Text = "Add all to decoder";
            this.buttonAddAll.UseVisualStyleBackColor = true;
            this.buttonAddAll.Click += new System.EventHandler(this.buttonAddAll_Click);
            // 
            // buttonDeleteSelected
            // 
            this.buttonDeleteSelected.Location = new System.Drawing.Point(3, 90);
            this.buttonDeleteSelected.Name = "buttonDeleteSelected";
            this.buttonDeleteSelected.Size = new System.Drawing.Size(128, 23);
            this.buttonDeleteSelected.TabIndex = 1;
            this.buttonDeleteSelected.Text = "Delete selected";
            this.buttonDeleteSelected.UseVisualStyleBackColor = true;
            this.buttonDeleteSelected.Click += new System.EventHandler(this.buttonDeleteSelected_Click);
            // 
            // buttonRefreshFromDecoder
            // 
            this.buttonRefreshFromDecoder.AccessibleDescription = "Read back all transponder data from the decoder";
            this.buttonRefreshFromDecoder.Location = new System.Drawing.Point(3, 119);
            this.buttonRefreshFromDecoder.Name = "buttonRefreshFromDecoder";
            this.buttonRefreshFromDecoder.Size = new System.Drawing.Size(128, 23);
            this.buttonRefreshFromDecoder.TabIndex = 2;
            this.buttonRefreshFromDecoder.Text = "Refresh from decoder";
            this.buttonRefreshFromDecoder.UseVisualStyleBackColor = true;
            this.buttonRefreshFromDecoder.Click += new System.EventHandler(this.buttonRefreshFromDecoder_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.buttonDebug);
            this.panel6.Controls.Add(this.buttonSaveDB);
            this.panel6.Controls.Add(this.buttonOpenDB);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(433, 50);
            this.panel6.TabIndex = 4;
            // 
            // buttonSaveDB
            // 
            this.buttonSaveDB.AccessibleDescription = "Export the History to a file";
            this.buttonSaveDB.Image = global::RCHourglassManager.Properties.Resources.disk_blue;
            this.buttonSaveDB.Location = new System.Drawing.Point(231, 7);
            this.buttonSaveDB.Name = "buttonSaveDB";
            this.buttonSaveDB.Size = new System.Drawing.Size(50, 39);
            this.buttonSaveDB.TabIndex = 4;
            this.buttonSaveDB.UseVisualStyleBackColor = true;
            this.buttonSaveDB.Click += new System.EventHandler(this.buttonSaveDB_Click);
            // 
            // buttonOpenDB
            // 
            this.buttonOpenDB.AccessibleDescription = "Read the history form a file";
            this.buttonOpenDB.Image = global::RCHourglassManager.Properties.Resources.folder_add;
            this.buttonOpenDB.Location = new System.Drawing.Point(175, 7);
            this.buttonOpenDB.Name = "buttonOpenDB";
            this.buttonOpenDB.Size = new System.Drawing.Size(50, 39);
            this.buttonOpenDB.TabIndex = 3;
            this.buttonOpenDB.UseVisualStyleBackColor = true;
            this.buttonOpenDB.Click += new System.EventHandler(this.buttonOpenDB_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 14);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Transponder History";
            // 
            // tabPageSetup
            // 
            this.tabPageSetup.Controls.Add(this.buttonAmbRcMode);
            this.tabPageSetup.Controls.Add(this.buttonCanoMode);
            this.tabPageSetup.Controls.Add(this.groupBox1);
            this.tabPageSetup.Location = new System.Drawing.Point(4, 29);
            this.tabPageSetup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageSetup.Name = "tabPageSetup";
            this.tabPageSetup.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageSetup.Size = new System.Drawing.Size(745, 223);
            this.tabPageSetup.TabIndex = 1;
            this.tabPageSetup.Text = "Setup";
            this.tabPageSetup.UseVisualStyleBackColor = true;
            this.tabPageSetup.Enter += new System.EventHandler(this.tabPageSetup_Enter);
            // 
            // buttonAmbRcMode
            // 
            this.buttonAmbRcMode.Location = new System.Drawing.Point(506, 26);
            this.buttonAmbRcMode.Name = "buttonAmbRcMode";
            this.buttonAmbRcMode.Size = new System.Drawing.Size(105, 69);
            this.buttonAmbRcMode.TabIndex = 2;
            this.buttonAmbRcMode.Text = "Set AmbRc mode (experimental)";
            this.buttonAmbRcMode.UseVisualStyleBackColor = true;
            this.buttonAmbRcMode.Click += new System.EventHandler(this.buttonAmbRcMode_Click);
            // 
            // buttonCanoMode
            // 
            this.buttonCanoMode.Location = new System.Drawing.Point(372, 26);
            this.buttonCanoMode.Name = "buttonCanoMode";
            this.buttonCanoMode.Size = new System.Drawing.Size(105, 69);
            this.buttonCanoMode.TabIndex = 1;
            this.buttonCanoMode.Text = "Set CANO mode";
            this.buttonCanoMode.UseVisualStyleBackColor = true;
            this.buttonCanoMode.Click += new System.EventHandler(this.buttonCanoMode_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxFreq);
            this.groupBox1.Controls.Add(this.buttonRefreshConfig);
            this.groupBox1.Controls.Add(this.buttonApplyConfig);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBoxBeep);
            this.groupBox1.Controls.Add(this.textBoxLED);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 188);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Notification Setup";
            // 
            // comboBoxFreq
            // 
            this.comboBoxFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFreq.FormattingEnabled = true;
            this.comboBoxFreq.Location = new System.Drawing.Point(193, 96);
            this.comboBoxFreq.Name = "comboBoxFreq";
            this.comboBoxFreq.Size = new System.Drawing.Size(75, 28);
            this.comboBoxFreq.TabIndex = 7;
            // 
            // buttonRefreshConfig
            // 
            this.buttonRefreshConfig.Location = new System.Drawing.Point(97, 149);
            this.buttonRefreshConfig.Name = "buttonRefreshConfig";
            this.buttonRefreshConfig.Size = new System.Drawing.Size(75, 33);
            this.buttonRefreshConfig.TabIndex = 6;
            this.buttonRefreshConfig.Text = "Refresh";
            this.buttonRefreshConfig.UseVisualStyleBackColor = true;
            this.buttonRefreshConfig.Click += new System.EventHandler(this.buttonRefreshConfig_Click);
            // 
            // buttonApplyConfig
            // 
            this.buttonApplyConfig.Location = new System.Drawing.Point(193, 149);
            this.buttonApplyConfig.Name = "buttonApplyConfig";
            this.buttonApplyConfig.Size = new System.Drawing.Size(75, 33);
            this.buttonApplyConfig.TabIndex = 5;
            this.buttonApplyConfig.Text = "Apply";
            this.buttonApplyConfig.UseVisualStyleBackColor = true;
            this.buttonApplyConfig.Click += new System.EventHandler(this.buttonApplyConfig_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(152, 20);
            this.label7.TabIndex = 4;
            this.label7.Text = "Beeper frequency (kHz)";
            // 
            // textBoxBeep
            // 
            this.textBoxBeep.Location = new System.Drawing.Point(193, 63);
            this.textBoxBeep.Name = "textBoxBeep";
            this.textBoxBeep.Size = new System.Drawing.Size(75, 26);
            this.textBoxBeep.TabIndex = 3;
            // 
            // textBoxLED
            // 
            this.textBoxLED.Location = new System.Drawing.Point(193, 31);
            this.textBoxLED.Name = "textBoxLED";
            this.textBoxLED.Size = new System.Drawing.Size(75, 26);
            this.textBoxLED.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(171, 20);
            this.label6.TabIndex = 1;
            this.label6.Text = "Beeper duration (0-255 ms)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "LED blink time (0-255 ms)";
            // 
            // tabPagePIC
            // 
            this.tabPagePIC.AccessibleDescription = "PIC programming functions";
            this.tabPagePIC.Controls.Add(this.buttonDetectPIC);
            this.tabPagePIC.Controls.Add(this.label8);
            this.tabPagePIC.Location = new System.Drawing.Point(4, 29);
            this.tabPagePIC.Name = "tabPagePIC";
            this.tabPagePIC.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePIC.Size = new System.Drawing.Size(745, 223);
            this.tabPagePIC.TabIndex = 2;
            this.tabPagePIC.Text = "PIC Programming";
            this.tabPagePIC.UseVisualStyleBackColor = true;
            // 
            // buttonDetectPIC
            // 
            this.buttonDetectPIC.Location = new System.Drawing.Point(58, 87);
            this.buttonDetectPIC.Name = "buttonDetectPIC";
            this.buttonDetectPIC.Size = new System.Drawing.Size(128, 44);
            this.buttonDetectPIC.TabIndex = 1;
            this.buttonDetectPIC.Text = "Detect PIC";
            this.buttonDetectPIC.UseVisualStyleBackColor = true;
            this.buttonDetectPIC.Click += new System.EventHandler(this.buttonDetectPIC_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(283, 60);
            this.label8.TabIndex = 0;
            this.label8.Text = "This feature is under development.\r\nYou can detect a PIC connected to the socket\r" +
    "\nto test the programming circuit\r\n";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.logBox);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 304);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(753, 110);
            this.panel4.TabIndex = 4;
            // 
            // logBox
            // 
            this.logBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logBox.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logBox.Location = new System.Drawing.Point(0, 0);
            this.logBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(753, 110);
            this.logBox.TabIndex = 0;
            this.logBox.Text = "";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.tabControl1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 48);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(753, 256);
            this.panel5.TabIndex = 0;
            // 
            // buttonDebug
            // 
            this.buttonDebug.Enabled = false;
            this.buttonDebug.Location = new System.Drawing.Point(314, 14);
            this.buttonDebug.Name = "buttonDebug";
            this.buttonDebug.Size = new System.Drawing.Size(75, 23);
            this.buttonDebug.TabIndex = 5;
            this.buttonDebug.Text = "Debug";
            this.buttonDebug.UseVisualStyleBackColor = true;
            this.buttonDebug.Visible = false;
            this.buttonDebug.Click += new System.EventHandler(this.buttonDebug_Click);
            // 
            // FormManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 414);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RCHourglass Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageTransponders.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.tabPageSetup.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPagePIC.ResumeLayout(false);
            this.tabPagePIC.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageTransponders;
        private System.Windows.Forms.TabPage tabPageSetup;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListView listViewDecoder;
        private System.Windows.Forms.ColumnHeader columnTx;
        private System.Windows.Forms.ColumnHeader columnNick;
        private System.Windows.Forms.ColumnHeader columnImportant;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RichTextBox logBox;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbCOMPorts;
        private System.Windows.Forms.Button buttonRefreshTransponders;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonAddTxToDecoder;
        private System.Windows.Forms.Button buttonAddAll;
        private System.Windows.Forms.Button buttonDeleteSelected;
        private System.Windows.Forms.Button buttonRefreshFromDecoder;
        private System.Windows.Forms.ListView listViewDB;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabPagePIC;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxFreq;
        private System.Windows.Forms.Button buttonRefreshConfig;
        private System.Windows.Forms.Button buttonApplyConfig;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxBeep;
        private System.Windows.Forms.TextBox textBoxLED;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonDetectPIC;
        private System.Windows.Forms.Button buttonAmbRcMode;
        private System.Windows.Forms.Button buttonCanoMode;
        private System.Windows.Forms.Button buttonOpenDB;
        private System.Windows.Forms.Button buttonSaveDB;
        private System.Windows.Forms.Button buttonDebug;
    }
}

