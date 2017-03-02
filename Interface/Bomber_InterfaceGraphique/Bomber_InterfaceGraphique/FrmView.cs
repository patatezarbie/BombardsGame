using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bomber_InterfaceGraphique
{
    public partial class FrmView : Form
    {
        public FrmView()
        {
            InitializeComponent();
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug debug = new Debug();
            debug.ShowDialog(this);
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void rejoindrePartieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JoinGame join = new JoinGame();
            if (join.ShowDialog(this) == DialogResult.OK)
            {
                MessageBox.Show("En cours");
            }
        }
    }
}
