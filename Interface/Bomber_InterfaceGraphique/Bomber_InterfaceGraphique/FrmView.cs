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
        List<Player> playerlist = new List<Player>();
        Graphics g;
        BG_PowerBar bgBar;
        Timer test;

        public FrmView()
        {
            InitializeComponent();
            playerlist.Add(new Player("paul"));
            playerlist.Add(new Player("wdwdw"));
            playerlist.Add(new Player("ghhhhh"));

            rtb_score.WriteScore(playerlist);
            bgBar = new BG_PowerBar(200, this.Height - 100);
            
            this.test = new Timer();
            this.test.Interval = 1;
            this.test.Tick += test_Tick;
            this.test.Start();
        }

        void test_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        // Show the debug form
        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug debug = new Debug();
            debug.ShowDialog(this);
        }

        // Quit the application
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

        private void FrmView_Paint(object sender, PaintEventArgs e)
        {
            // Get the graphics
            g = e.Graphics;

            // Draw the status
            DrawConnectionStatus(g, true);
        }

        /// <summary>
        /// Draw the connection status
        ///     - Red => disconnected
        ///     - Green => connected
        /// </summary>
        /// <param name="g"></param>
        /// <param name="isConnected"></param>
        private void DrawConnectionStatus(Graphics g, bool isConnected)
        {
            Rectangle rect = new Rectangle(10, 30, 20, 20);

            if (isConnected == true)
            {
                g.FillRectangle(Brushes.Green, rect);
            }
            else
            {
                g.FillRectangle(Brushes.Red, rect);
            }
        }

        // Disable or enable the menu option
        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (true) // Need to check if player already in game or not
            {
                rejoindrePartieToolStripMenuItem.Enabled = true;
                quitterPartieToolStripMenuItem.Enabled = false;
            }
            else
            {
                rejoindrePartieToolStripMenuItem.Enabled = false;
                quitterPartieToolStripMenuItem.Enabled = true;
            }
        }

        private void FrmView_Paint(object sender, PaintEventArgs e)
        {
            this.bgBar.Draw(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.bgBar.StartStopProgress();
        }
    }
}
