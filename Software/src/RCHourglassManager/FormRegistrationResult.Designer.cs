namespace RCHourglassManager
{
    partial class FormRegistrationResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRegistrationResult));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonAnother = new System.Windows.Forms.Button();
            this.buttonRetry = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelResult = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonRetry);
            this.panel1.Controls.Add(this.buttonAnother);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 216);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(370, 100);
            this.panel1.TabIndex = 0;
            // 
            // buttonAnother
            // 
            this.buttonAnother.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonAnother.Location = new System.Drawing.Point(11, 23);
            this.buttonAnother.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonAnother.Name = "buttonAnother";
            this.buttonAnother.Size = new System.Drawing.Size(112, 50);
            this.buttonAnother.TabIndex = 0;
            this.buttonAnother.Text = "Register another";
            this.buttonAnother.UseVisualStyleBackColor = true;
            // 
            // buttonRetry
            // 
            this.buttonRetry.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.buttonRetry.Location = new System.Drawing.Point(129, 23);
            this.buttonRetry.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonRetry.Name = "buttonRetry";
            this.buttonRetry.Size = new System.Drawing.Size(112, 50);
            this.buttonRetry.TabIndex = 1;
            this.buttonRetry.Text = "Retry registration";
            this.buttonRetry.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(247, 23);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(112, 50);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Done";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelResult.Location = new System.Drawing.Point(12, 11);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(0, 20);
            this.labelResult.TabIndex = 1;
            // 
            // FormRegistrationResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 316);
            this.ControlBox = false;
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormRegistrationResult";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Registration Results";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAnother;
        public System.Windows.Forms.Button buttonRetry;
        public System.Windows.Forms.Label labelResult;
    }
}