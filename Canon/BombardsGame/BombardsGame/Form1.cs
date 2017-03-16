using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BombardsGame
{
    public partial class Form1 : Form
    {
        BG_Cannon c1;
        bool allowShot;

        public Form1()
        {
            InitializeComponent();
            c1 = new BG_Cannon(Color.OrangeRed, new BG_Location(50, 50));
            allowShot = false;
            this.DoubleBuffered = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            c1.Draw(e); 
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    c1.AdjustAngle(-1f);
                    break;

                case Keys.S:
                    c1.AdjustAngle(1f);
                    break;
                case Keys.Space:
                    c1.Shoot();
                    break;
                case Keys.M:
                    c1.Move(new BG_Location(300, 300));
                    break;
            }
        }

        private void animator_Tick(object sender, EventArgs e)
        {
            Invalidate();
            /*label1.Text = "Location cannon Red: X : " + c1.Location.PosX + " Y : " + c1.Location.PosY;
            if (c1.Bullet != null)
            {
                label2.Text = "Location bullet X : " + c1.Bullet + " Y : " + c1.Bullet._y;
            }
            */
        }
    }
}
