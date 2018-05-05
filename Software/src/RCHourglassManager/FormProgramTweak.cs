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
    public partial class FormProgramTweak : Form
    {
        DataTable dt = new DataTable("telegrams");
        public FormProgramTweak()
        {
            InitializeComponent();
            labelExtra.Text = String.Empty;
            this.tabPageNumber.Enabled = false;
            this.tabPageAdvanced.Enabled = false;
          
            this.dataGridPackets.AutoGenerateColumns = false;


            dt.Columns.Add(new DataColumn("index", typeof(int)));
            dt.Columns.Add(new DataColumn("packet", typeof(string)));
            dt.Columns.Add(new DataColumn("telegram", typeof(string)));
            dt.Columns[2].MaxLength = 24;
        }

        public String FileName { set { this.labelFilename.Text = value; } }

        public String ExtraInfo { set { this.labelExtra.Text = value; }  }

        public uint tweakableNumber {  set { this.tabPageNumber.Enabled = true; textBoxNumber.Text = Convert.ToString(value); } }

     
        public MemoryContent Program { get; set;  }

        public List<byte[]> tewakablePackets
        {
            set
            {
                if (value == null ||value.Count==0 ) this.tabPageAdvanced.Enabled = false;
                else
                {
                    this.tabPageAdvanced.Enabled = true;
                    dt.Rows.Clear();
                    for (int i=0;i< value.Count;i++)
                    {
                        DataRow r = dt.NewRow();
                        r[0] = i;
                        r[1] = i == 0 ? "Number" : $"Status {i}";
                        StringBuilder s = new StringBuilder(50);
                        foreach (byte b in value[i]) s.Append(Convert.ToString(b, 16).PadLeft(2, '0').ToUpperInvariant());
                        r[2] = s.ToString();
                        
                        dt.Rows.Add(r);
                        r.AcceptChanges();
 
                    }
                    if (dataGridPackets.DataSource != bindingSource1)
                    {
                        bindingSource1.DataSource = dt;
                        dataGridPackets.DataSource = bindingSource1;
                    }
                }

            }
        }

        private void buttonProgram_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedTab == tabPageOriginal) { this.DialogResult = DialogResult.OK; return; }
                if (tabControl1.SelectedTab == tabPageNumber)
                {
                    // Write 8 equal telegrams to the tx encoded without status
                    int txNumber = -1;
                    if (!int.TryParse(textBoxNumber.Text.Trim(), out txNumber)) throw new Exception("Invalid transponder number");
                    if (txNumber < 2097152 || txNumber > 9999999)
                        throw new ArgumentException("Transponder number must be between 2097152 and 9999999");

                    HexMemoryBlock h = new HexMemoryBlock();

                    h.DataBlock = new ushort[12];

                    byte[] b = TransponderEncoding.encodeTransponderNoStatus((uint)txNumber);
                    for (int i = 0; i < b.Length; i++)
                    {
                        h.DataBlock[i] = (ushort)(0x03400 | b[i]);
                    }

                    uint baseAddress = 0x703;

                    for (int i = 0; i < 8; i++, baseAddress += 12)
                    {
                        h.BaseAddress = baseAddress;
                        Program.WriteMem(h);
                    }


                    this.DialogResult = DialogResult.OK;
                    return;
                }
                if (tabControl1.SelectedTab == tabPageAdvanced)
                {
                    HexMemoryBlock h = new HexMemoryBlock();

                    h.DataBlock = new ushort[12];



                    uint baseAddress = 0x703;

                    for (int i = 0; i < 8; i++, baseAddress += 12)
                    {
                        h.BaseAddress = baseAddress;
                        String val = dt.Rows[i][2].ToString().Trim().ToUpperInvariant();
                        if (val.Length != 24) throw new Exception($"Wrong value {dt.Rows[i][1]}");
                        try
                        {
                            for (int j = 0; j < 12; j++)
                            {
                                h.DataBlock[j] = (ushort)(0x03400 | Convert.ToInt16(val.Substring(2 * j, 2), 16));
                            }
                        }
                        catch
                        {
                            throw new Exception($"Wrong value {dt.Rows[i][1]}");
                        }


                        Program.WriteMem(h);
                    }


                    this.DialogResult = DialogResult.OK;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            buttonProgram.Enabled = tabControl1.SelectedTab.Enabled;
        }
    }
}
