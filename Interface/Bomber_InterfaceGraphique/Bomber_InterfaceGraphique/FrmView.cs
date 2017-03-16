/* *
 * Project     : BombardsGame
 * Description :
 * Authors     : De Biasi Loris, Devaud Alan
 * Date        : 17.03.2017
 * Version     : 1.0
 * */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Bomber_InterfaceGraphique
{
    public partial class FrmView : Form
    {
        #region Properties
        List<Player> playerlist = new List<Player>();
        Graphics g;
        BG_PowerBar bgBar;
        Timer test;
        bool spacePressed;

        public FrmView()
        {
            this.KeyPreview = true;
            this.DoubleBuffered = true;
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

            this.spacePressed = false;
        }

        void test_Tick(object sender, EventArgs e)
        {
            this.Refresh();

            if (!this.Focused)
                this.Focus();

            if (this.spacePressed)
                this.bgBar.StartProgress();
            else if (!this.spacePressed)
                this.bgBar.StopProgress();

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

            this.bgBar.Draw(e);
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

        private void FrmView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                this.spacePressed = true;
                Console.WriteLine("Space pressed");
            }
                
        }

        private void FrmView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                this.spacePressed = false;
        }
    }
}
