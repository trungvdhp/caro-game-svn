using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Caro
{
    public partial class Fake : Form
    {
        public Fake()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate(Application.StartupPath+@"\sgomoku\index.html");
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(Application.StartupPath+@"\sgomoku\index.html");
        }
    }
}
