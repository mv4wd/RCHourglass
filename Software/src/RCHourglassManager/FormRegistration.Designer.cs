namespace RCHourglassManager
{
    partial class FormRegistration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRegistration));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonDone = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxImportant = new System.Windows.Forms.CheckBox();
            this.textBoxTx = new System.Windows.Forms.TextBox();
            this.textBoxNick = new System.Windows.Forms.TextBox();
            this.buttonLearn = new System.Windows.Forms.Button();
            this.labelProgress = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonDone);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 252);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(444, 63);
            this.panel1.TabIndex = 10;
            // 
            // buttonDone
            // 
            this.buttonDone.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonDone.Location = new System.Drawing.Point(320, 15);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(103, 36);
            this.buttonDone.TabIndex = 1;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDoneClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 80);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ready to learn a new RC4 transponder.\r\n1 - Put just one transponder near the loop" +
    "\r\n2 - Fill in the number and nickname\r\n3 - Click learn\r\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "TX Number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "NickName";
            // 
            // checkBoxImportant
            // 
            this.checkBoxImportant.AccessibleDescription = "Checkbox to say the transponder is important";
            this.checkBoxImportant.AutoSize = true;
            this.checkBoxImportant.Location = new System.Drawing.Point(42, 184);
            this.checkBoxImportant.Name = "checkBoxImportant";
            this.checkBoxImportant.Size = new System.Drawing.Size(176, 20);
            this.checkBoxImportant.TabIndex = 2;
            this.checkBoxImportant.Text = "Make this transponder important";
            this.checkBoxImportant.UseVisualStyleBackColor = true;
            // 
            // textBoxTx
            // 
            this.textBoxTx.AccessibleDescription = "Edit for the transponder number";
            this.textBoxTx.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTx.Location = new System.Drawing.Point(86, 109);
            this.textBoxTx.MaxLength = 10;
            this.textBoxTx.Name = "textBoxTx";
            this.textBoxTx.Size = new System.Drawing.Size(132, 26);
            this.textBoxTx.TabIndex = 0;
            // 
            // textBoxNick
            // 
            this.textBoxNick.AccessibleDescription = "Edit for the nickname of the transponder";
            this.textBoxNick.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxNick.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNick.Location = new System.Drawing.Point(86, 137);
            this.textBoxNick.MaxLength = 8;
            this.textBoxNick.Name = "textBoxNick";
            this.textBoxNick.Size = new System.Drawing.Size(132, 26);
            this.textBoxNick.TabIndex = 1;
            // 
            // buttonLearn
            // 
            this.buttonLearn.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLearn.Location = new System.Drawing.Point(251, 109);
            this.buttonLearn.Name = "buttonLearn";
            this.buttonLearn.Size = new System.Drawing.Size(156, 95);
            this.buttonLearn.TabIndex = 3;
            this.buttonLearn.Text = "Learn";
            this.buttonLearn.UseVisualStyleBackColor = true;
            this.buttonLearn.Click += new System.EventHandler(this.buttonLearn_Click);
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(45, 221);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(0, 16);
            this.labelProgress.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(126, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "A-Z 0-9 .-_";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::RCHourglassManager.Properties.Resources.RCHourglassLogoNew64;
            this.pictureBox1.Location = new System.Drawing.Point(340, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(83, 64);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // FormRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 315);
            this.ControlBox = false;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.buttonLearn);
            this.Controls.Add(this.textBoxNick);
            this.Controls.Add(this.textBoxTx);
            this.Controls.Add(this.checkBoxImportant);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormRegistration";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New transponder registration";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxImportant;
        private System.Windows.Forms.TextBox textBoxTx;
        private System.Windows.Forms.TextBox textBoxNick;
        private System.Windows.Forms.Button buttonLearn;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
    }
}