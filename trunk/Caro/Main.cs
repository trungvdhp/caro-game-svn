using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Caro
{
    public partial class Main : Form
    {
        private DataTable optionsTable;
        private bool cursorChanged=false;
        public Main()
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
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (board.processing) return;
            try
            {
                optionsTable.Rows.Clear();
                optionsTable.ReadXml("Options.xml");
                LoadOptions(this);
                bool playerFirst = op_comboFirstPlayer.Text == "Player" ? true : false;
                char playerSymbol = op_comboPlayerSymbol.Text == "X" ? 'x' : 'o';
                int level = op_trackComputerLevel.Value;
                board.NewGame(playerFirst, playerSymbol, level);
            }
            catch
            {
                board.NewGame(true, 'x', 3);
            }
            timer1.Start();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options options = new Options();
            options.ShowInTaskbar = false;
            options.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (board.processing)
            {
                if (!cursorChanged)
                {
                    cursorChanged = true;
                    Cursor = Cursors.WaitCursor;
                }
            }
            else
            {
                if(cursorChanged)
                {
                    cursorChanged = false;
                    Cursor = Cursors.Arrow;
                }
            }
            if(board.GameOver)
            {
                Cursor = Cursors.Arrow;
                timer1.Stop();
            }
        }
    }
}
