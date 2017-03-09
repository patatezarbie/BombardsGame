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
        BG_Canon c1;
        BG_Canon c2;
        bool allowShot;

        public Form1()
        {
            InitializeComponent();
            c1 = new BG_Canon(Color.OrangeRed, new Point(0,0));
            c2 = new BG_Canon(Color.Blue, new Point(100,100));
            allowShot = false;
            Invalidate();

            
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            c1.draw(e);
            c2.draw(e);  
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    c1.ajustAngle(2.5f);
                    break;

                case Keys.A:
                    c1.ajustAngle(-2.5f);
                    break;
                case Keys.Space:
                    c1.shoot();
                    break;
                case Keys.M:
                    c1.Move(new Point(300, 300));
                    break;

            }

            Invalidate();
        }


    }
}
