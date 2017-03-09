using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BulletApp
{
    public partial class Main : Form
    {
        List<BG_Bullet> bullets = new List<BG_Bullet>();

        public Main()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (BG_Bullet bullet in bullets)
            {
                bullet.Draw(e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            bullets.Add(new BG_Bullet(150, 300, 45, 50));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();         
        }
    }
}
