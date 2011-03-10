using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Caro
{
    public partial class Options : Form
    {
        private DataTable optionsTable;
        public Options()
        {
            InitializeComponent();
            optionsTable = new DataTable("Options");
            optionsTable.Columns.Add("C", typeof(string));
            optionsTable.Columns.Add("V", typeof(string));
        }
        private void LoadOptions(Control ctr)
        {
            string name = ctr.Name;
            if (name.StartsWith("op_"))
            {
                DataRow[] R = optionsTable.Select("[C]='" + name + "'");
                if (R.Length > 0)
                {

                    DataRow r = R[0];
                    if (name.Contains("check"))
                    {
                        CheckBox chk = (CheckBox)ctr;
                        if ((string)r[1] == "False")
                            chk.Checked = false;
                        else chk.Checked = true;
                    }
                    if (name.Contains("combo"))
                    {
                        ComboBox cb = (ComboBox)ctr;
                        cb.Text = r[1].ToString();
                    }
                    if (name.Contains("track"))
                    {
                        TrackBar tr = (TrackBar)ctr;
                        tr.Value = Convert.ToInt32(r[1]);
                    }
                    if (name.Contains("numeric"))
                    {
                        NumericUpDown ud = (NumericUpDown)ctr;
                        ud.Value = Convert.ToInt32(r[1]);
                    }
                }
            }
            if (ctr.HasChildren)
                foreach (Control child in ctr.Controls)
                    LoadOptions(child);
        }
        private void SaveOptions(Control ctr)
        {
            if (ctr.Name.StartsWith("op_"))
            {
                DataRow r = optionsTable.NewRow();
                r[0] = ctr.Name;
                if (ctr.Name.Contains("check"))
                {
                    CheckBox chk = (CheckBox)ctr;
                    r[1] = chk.Checked.ToString();
                }
                if (ctr.Name.Contains("combo"))
                {
                    ComboBox cb = (ComboBox)ctr;
                    r[1] = cb.Text;
                }
                if (ctr.Name.Contains("track"))
                {
                    TrackBar tr = (TrackBar)ctr;
                    r[1] = tr.Value;
                }
                if (ctr.Name.Contains("numeric"))
                {
                    NumericUpDown ud = (NumericUpDown)ctr;
                    r[1] = ud.Value;
                }
                optionsTable.Rows.Add(r);
            }
            if (ctr.HasChildren)
                foreach (Control child in ctr.Controls)
                {
                    SaveOptions(child);
                }
        }
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            labelTrackbar.Text = op_trackComputerLevel.Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void comboBox_keypress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            optionsTable.Rows.Clear();
            SaveOptions(this);
            optionsTable.WriteXml("Options.xml");
            Close();
        }

        private void Options_Load(object sender, EventArgs e)
        {

        }

        private void Options_Shown(object sender, EventArgs e)
        {
            try
            {
                optionsTable.ReadXml("Options.xml");
                LoadOptions(this);
            }
            catch
            {
            }
        }
    }
}
