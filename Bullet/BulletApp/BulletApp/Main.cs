using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BulletApp
{
    public partial class Form1 : Form
    {
        List<BG_Bullet> bullets = new List<BG_Bullet>();

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (BG_Bullet bullet in bullets)
            {
                if (bullet.Visible)
                {
                    bullet.Draw(e);
                }              
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            bullets.Add(new BG_Bullet(250, 300, (int)nudAngle.Value, (int)nudVelocity.Value));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();         
        }
    }
}
