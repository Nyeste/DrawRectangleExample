using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{


    public partial class Form1 : Form
    {
        int gridSize = 20;
        bool started = false;
        List<DrawAction> actions = new List<DrawAction>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            started = true;
            this.Cursor = Cursors.Cross;
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {
            foreach (DrawAction da in actions)
            {
                if (da.type == 'R') e.Graphics.DrawRectangle(Pens.Red, da.rect);
                else if (da.type == 'E') e.Graphics.DrawEllipse(Pens.Red, da.rect);
                //..
            }
        }

        private void mainPanel_PaintGrid (object sender, PaintEventArgs e)
        {
            for (int i=0; i < panel1.Size.Width; i=i+gridSize)
            {
                e.Graphics.DrawLine(Pens.LightGray, new Point(i,0), new Point(i, panel1.Height));
            }
            for (int j = 0; j < panel1.Size.Height; j = j + gridSize)
            {
                e.Graphics.DrawLine(Pens.LightGray, new Point(0, j), new Point(panel1.Width, j));
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            actions.Clear();
            panel1.Invalidate();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            Point target = GetClosestGrid(e);
            if (started)
            {
                Pen blackPen = new Pen(Color.Black, 3);
                actions.Add(new DrawAction('R', new Rectangle(target.X, target.Y, 100, 50), Color.DarkGoldenrod));
                started = false;
                this.Cursor = Cursors.Default;
                panel1.Invalidate();
            }
        }

        private Point GetClosestGrid(MouseEventArgs e)
        {
            int x = e.X - e.X%gridSize;
            int y = e.Y - e.Y % gridSize;
            return new Point(x, y);
        }

    }
}
