using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RCHourglassManager
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
            labelVersion.Text = "RC Hourglass Manager version: " + Application.ProductVersion;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Thank you", "", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void linkLabelGit_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://github.com/mv4wd/RCHourglass");
            }
            catch { }
        }
    }
}
