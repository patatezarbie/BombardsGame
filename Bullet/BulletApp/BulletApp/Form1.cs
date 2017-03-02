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
    public partial class Form1 : Form
    {
        BG_Bullet bullet = new BG_Bullet(409, 305, 5, 20);

        public Form1()
        {
            InitializeComponent();
            
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            bullet.Draw(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
