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
        public Main()
        {
            InitializeComponent();
            board.NewGame(true);
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            board.NewGame(true);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
