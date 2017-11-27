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
                if (da.type == 'R') e.Graphics.DrawRectangle(new Pen(da.color), da.rect);
                else if (da.type == 'E') e.Graphics.DrawEllipse(new Pen(da.color), da.rect);
                //..
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            actions.Clear();
            panel1.Invalidate();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (started)
            {
                Pen blackPen = new Pen(Color.Black, 3);
                actions.Add(new DrawAction('R', new Rectangle(e.X, e.Y, 100, 50), Color.DarkGoldenrod));
                started = false;
                this.Cursor = Cursors.Default;
                panel1.Invalidate();
            }
        }
    }
}
