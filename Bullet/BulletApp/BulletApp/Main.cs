/* Author : Kevin Amado & Lucien Camuglia
 * Class  : T.IS-E2A
 * Date   : 16.03.17
 * Version : 2.0
 * Description : Application for testing BG_Bullet.cs .
 */
using System;
using System.Collections.Generic;
using System.Drawing;
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
            Pen p = new Pen(Color.Green);
            e.Graphics.DrawRectangle(p, 300, 300, 10, 10);
             List<BG_Bullet> bulletsToRemove = new List<BG_Bullet>();
            foreach (BG_Bullet bullet in bullets)
            {
                //calcul if the bullet is out of the application
                  if(bullet.Position.X < this.Width && bullet.Position.X > -1 && bullet.Position.Y < this.Height )
                    bullet.Draw(e);
                else
                {
                    bulletsToRemove.Add(bullet);
                   
                }
            }
            foreach (BG_Bullet bullet in bulletsToRemove)
            {
                bullets.Remove(bullet);
            }              
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            bullets.Add(new BG_Bullet(300, 300, (int)nudAngle.Value, (int)nudVelocity.Value));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();         
        }
    }
}
