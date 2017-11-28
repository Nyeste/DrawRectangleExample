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
        private BuildingBlock nextBuildingBlock = null;
        private static Size rectSize = new Size(100, 50);
        private Rectangle rec;
        public Size BlockSize
        {
            get { return rectSize; }
        }
        public ProcessBlock(Point location)
        {
            this.rec = new Rectangle(new Point (location.X - rectSize.Width/2, location.Y - rectSize.Height/2), rectSize);
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
            DrawConnections(e);
        }

        public override Point GetNode(NodeDirection dir)
        {
            switch (dir)
            {
                case NodeDirection.Top:
                    return new Point(this.rec.Location.X + this.rec.Width / 2, this.rec.Location.Y);
                case NodeDirection.Bottom:
                    return new Point(this.rec.Location.X + this.rec.Width / 2, this.rec.Location.Y + this.rec.Height);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void DrawConnections(PaintEventArgs e)
        {
            if (nextBuildingBlock != null)
            {
                e.Graphics.DrawLine(Pens.Black, this.GetNode(NodeDirection.Bottom), nextBuildingBlock.GetNode(NodeDirection.Top));
            }
        }

        public override void ConnectNodeToBlock(NodeDirection dir, BuildingBlock bb)
        {
            if (dir == NodeDirection.Bottom) {
                nextBuildingBlock = bb;
            } else {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
