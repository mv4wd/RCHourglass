namespace RCHourglassManager
{
    partial class FormProgramTweak
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProgramTweak));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonProgram = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageOriginal = new System.Windows.Forms.TabPage();
            this.tabPageNumber = new System.Windows.Forms.TabPage();
            this.tabPageAdvanced = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.labelFilename = new System.Windows.Forms.Label();
            this.labelExtra = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridPackets = new System.Windows.Forms.DataGridView();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.Packet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Telegram = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageOriginal.SuspendLayout();
            this.tabPageNumber.SuspendLayout();
            this.tabPageAdvanced.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPackets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 306);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(451, 55);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonProgram);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(275, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(176, 55);
            this.panel2.TabIndex = 0;
            // 
            // buttonProgram
            // 
            this.buttonProgram.Location = new System.Drawing.Point(24, 4);
            this.buttonProgram.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonProgram.Name = "buttonProgram";
            this.buttonProgram.Size = new System.Drawing.Size(117, 48);
            this.buttonProgram.TabIndex = 0;
            this.buttonProgram.Text = "Program";
            this.buttonProgram.UseVisualStyleBackColor = true;
            this.buttonProgram.Click += new System.EventHandler(this.buttonProgram_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPageOriginal);
            this.tabControl1.Controls.Add(this.tabPageNumber);
            this.tabControl1.Controls.Add(this.tabPageAdvanced);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(451, 306);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPageOriginal
            // 
            this.tabPageOriginal.Controls.Add(this.labelExtra);
            this.tabPageOriginal.Controls.Add(this.labelFilename);
            this.tabPageOriginal.Controls.Add(this.label1);
            this.tabPageOriginal.Location = new System.Drawing.Point(4, 4);
            this.tabPageOriginal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageOriginal.Name = "tabPageOriginal";
            this.tabPageOriginal.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageOriginal.Size = new System.Drawing.Size(443, 277);
            this.tabPageOriginal.TabIndex = 0;
            this.tabPageOriginal.Text = "Original";
            this.tabPageOriginal.UseVisualStyleBackColor = true;
            // 
            // tabPageNumber
            // 
            this.tabPageNumber.Controls.Add(this.label4);
            this.tabPageNumber.Controls.Add(this.textBoxNumber);
            this.tabPageNumber.Controls.Add(this.label3);
            this.tabPageNumber.Controls.Add(this.label2);
            this.tabPageNumber.Location = new System.Drawing.Point(4, 4);
            this.tabPageNumber.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageNumber.Name = "tabPageNumber";
            this.tabPageNumber.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageNumber.Size = new System.Drawing.Size(433, 237);
            this.tabPageNumber.TabIndex = 1;
            this.tabPageNumber.Text = "Change Number";
            this.tabPageNumber.UseVisualStyleBackColor = true;
            // 
            // tabPageAdvanced
            // 
            this.tabPageAdvanced.Controls.Add(this.panel4);
            this.tabPageAdvanced.Controls.Add(this.panel3);
            this.tabPageAdvanced.Location = new System.Drawing.Point(4, 4);
            this.tabPageAdvanced.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageAdvanced.Name = "tabPageAdvanced";
            this.tabPageAdvanced.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageAdvanced.Size = new System.Drawing.Size(443, 277);
            this.tabPageAdvanced.TabIndex = 2;
            this.tabPageAdvanced.Text = "Advanced";
            this.tabPageAdvanced.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Program the firmware as read from file:";
            // 
            // labelFilename
            // 
            this.labelFilename.Location = new System.Drawing.Point(21, 73);
            this.labelFilename.Name = "labelFilename";
            this.labelFilename.Size = new System.Drawing.Size(391, 57);
            this.labelFilename.TabIndex = 1;
            this.labelFilename.Text = "labelFilename";
            // 
            // labelExtra
            // 
            this.labelExtra.AutoSize = true;
            this.labelExtra.Location = new System.Drawing.Point(21, 130);
            this.labelExtra.Name = "labelExtra";
            this.labelExtra.Size = new System.Drawing.Size(68, 16);
            this.labelExtra.TabIndex = 2;
            this.labelExtra.Text = "labelExtra";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(230, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Program a new Transponder number.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Number";
            // 
            // textBoxNumber
            // 
            this.textBoxNumber.Location = new System.Drawing.Point(94, 56);
            this.textBoxNumber.Name = "textBoxNumber";
            this.textBoxNumber.Size = new System.Drawing.Size(136, 22);
            this.textBoxNumber.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(19, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(383, 101);
            this.label4.TabIndex = 4;
            this.label4.Text = "Warning: the transponder will transmit only the number packets and might not be c" +
    "ompatible with commercial decoders";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(227, 16);
            this.label5.TabIndex = 2;
            this.label5.Text = "Modify the single packets transmitted";
            // 
            // dataGridPackets
            // 
            this.dataGridPackets.AllowUserToAddRows = false;
            this.dataGridPackets.AllowUserToDeleteRows = false;
            this.dataGridPackets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPackets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Packet,
            this.Telegram});
            this.dataGridPackets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridPackets.Location = new System.Drawing.Point(0, 0);
            this.dataGridPackets.Name = "dataGridPackets";
            this.dataGridPackets.Size = new System.Drawing.Size(435, 238);
            this.dataGridPackets.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(435, 31);
            this.panel3.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dataGridPackets);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Font = new System.Drawing.Font("Monospac821 BT", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(4, 35);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(435, 238);
            this.panel4.TabIndex = 5;
            // 
            // Packet
            // 
            this.Packet.DataPropertyName = "packet";
            this.Packet.HeaderText = "Packet";
            this.Packet.Name = "Packet";
            this.Packet.ReadOnly = true;
            this.Packet.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Telegram
            // 
            this.Telegram.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Telegram.DataPropertyName = "telegram";
            this.Telegram.HeaderText = "Telegram";
            this.Telegram.Name = "Telegram";
            this.Telegram.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Telegram.ToolTipText = "Hex packet with preamble";
            this.Telegram.Width = 78;
            // 
            // FormProgramTweak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 361);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormProgramTweak";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Firmware Tweak";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageOriginal.ResumeLayout(false);
            this.tabPageOriginal.PerformLayout();
            this.tabPageNumber.ResumeLayout(false);
            this.tabPageNumber.PerformLayout();
            this.tabPageAdvanced.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPackets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonProgram;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageOriginal;
        private System.Windows.Forms.Label labelExtra;
        private System.Windows.Forms.Label labelFilename;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPageNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPageAdvanced;
        private System.Windows.Forms.DataGridView dataGridPackets;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Packet;
        private System.Windows.Forms.DataGridViewTextBoxColumn Telegram;
    }
}