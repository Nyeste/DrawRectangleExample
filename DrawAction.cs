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
    class DrawAction
    {
        public char type { get; set; }
        public Rectangle rect { get; set; }
        public Color color { get; set; }
        //.....

        public DrawAction(char type_, Rectangle rect_, Color color_)
        { type = type_; rect = rect_; color = color_; }
    }
}
