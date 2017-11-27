using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2.BuildingBlocks
{
    class ProcessBlock : BuildingBlock
    {
        private Rectangle rec;
        public ProcessBlock(Rectangle rec_)
        {
            this.rec = rec_;
        }

        public override bool Contains(MouseEventArgs e)
        {
            return this.rec.Contains(e.Location);
        }

        public override void Draw(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.White, this.rec);
            e.Graphics.DrawRectangle(Pens.Black, this.rec);
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            e.Graphics.DrawString("Process", SystemFonts.DefaultFont, Brushes.Black, this.rec, sf);
            if (NextBuildingBlock != null)
            {
                e.Graphics.DrawLine(Pens.Black, this.GetOutputNode(), NextBuildingBlock.GetInputNode());
            }
        }

        public override Point GetInputNode()
        {
            return new Point(this.rec.Location.X + this.rec.Width / 2, this.rec.Location.Y);
        }

        public override Point GetOutputNode()
        {
            return new Point(this.rec.Location.X + this.rec.Width / 2, this.rec.Location.Y + this.rec.Height);
        }
    }
}
