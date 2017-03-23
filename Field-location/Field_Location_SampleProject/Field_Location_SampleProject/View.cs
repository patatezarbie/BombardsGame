/* 
 * Robin Plojoux, DEylan Wacker - CFPT-I  
 * 16.03.2017
 * POO 
 * POO tech project - Exaple project
 * 
 * Description : Example project for the BG_Field class drawing
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Field_Location_SampleProject
{
    public partial class View : Form
    {
        BG_Field field;
        Graphics g;
        Pen pen;
        SolidBrush b;
        SolidBrush b2;
        Random rnd;
        float fieldSize;

        public View()
        {
            InitializeComponent();
        }

        private void View_Load(object sender, EventArgs e)
        {
            g = panelDraw.CreateGraphics();
            b = new SolidBrush(Color.Green);
            b2 = new SolidBrush(Color.DarkGreen);
            rnd = new Random();
            fieldSize = 2.2f;

            DrawField();
        }

        public void DrawField()
        {
            int seed = rnd.Next(0,9999);
            lblSeed.Text = seed.ToString();
            field = new BG_Field(panelDraw.Width, panelDraw.Height, seed);
            //field = new BG_Field(panelDraw.Width, panelDraw.Height, 1); 
        }

        private void panelDraw_Paint(object sender, PaintEventArgs e)
        {
            // Bullet hit test
            int x = rnd.Next(0, panelDraw.Width);
            int y = rnd.Next(0, panelDraw.Height);

            //Draw field points
            field.Draw(e);

            g.FillEllipse(new SolidBrush(Color.Black), x - 3f, y - 3f, 6f, 6f);

            bool hit = field.IsFieldTouched(new BG_Location(x, y));
            lblBulletHit.Text = hit.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawField();
            panelDraw.Invalidate();
        }
    }
}
