/* 
 * Robin Plojoux, DEylan Wacker - CFPT-I  
 * 09.03.2017
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
        Random rnd;

        public View()
        {
            InitializeComponent();
        }

        private void View_Load(object sender, EventArgs e)
        {
            g = panelDraw.CreateGraphics();
            b = new SolidBrush(Color.Green);
            pen = new Pen(Color.Green);
            pen.Width = 2;
            rnd = new Random();

            DrawField();
        }

        public void DrawField()
        {
            field = new BG_Field(panelDraw.Width, panelDraw.Height, rnd.Next(0,999));
            //field = new BG_Field(panelDraw.Width, panelDraw.Height, 1); 
        }

        private void panelDraw_Paint(object sender, PaintEventArgs e)
        {
            //Draw rectangles

            foreach (var l in field.Locations)
            {
                g.FillRectangle(b, l.PosX, l.PosY , 2.2f, 2.2f);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
        }
    }
}
