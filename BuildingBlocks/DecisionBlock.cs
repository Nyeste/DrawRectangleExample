using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2.BuildingBlocks
{
    class DecisionBlock : BuildingBlock
    {
        private BuildingBlock nextTrueBlock = null;
        private BuildingBlock nextFalseBlock = null;
        private static Size rectSize = new Size(100, 50);
        private Point[] points = new Point[4];
        public DecisionBlock(Point location)
        {
            points[0] = new Point(location.X, location.Y + rectSize.Height/2);
            points[1] = new Point(location.X + rectSize.Width / 2, location.Y);
            points[2] = new Point(location.X, location.Y - rectSize.Height / 2);
            points[3] = new Point(location.X - rectSize.Width / 2, location.Y);
        }
        public override bool Contains(MouseEventArgs e)
        {
            var coef = points.Skip(1).Select((p, i) =>
                                            (e.Y - points[i].Y) * (p.X - points[i].X)
                                          - (e.X - points[i].X) * (p.Y - points[i].Y))
                                    .ToList();

            if (coef.Any(p => p == 0))
                return true;

            for (int i = 1; i < coef.Count(); i++)
            {
                if (coef[i] * coef[i - 1] < 0)
                    return false;
            }
            return true;
        }

        public NodeDirection GetNodeFromLocation (Point p)
        {
            if (p.X < points[0].X)
                return NodeDirection.Left;
            else
                return NodeDirection.Right;
        }

        public override void Draw(PaintEventArgs e)
        {
            e.Graphics.FillPolygon(Brushes.White, this.points);
            e.Graphics.DrawPolygon(Pens.Black, this.points);
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            e.Graphics.DrawString("Decision", SystemFonts.DefaultFont, Brushes.Black, GetOrigo(), sf);
            DrawConnections (e);
        }

        public override Point GetNode(NodeDirection dir)
        {
            switch (dir)
            {
                case NodeDirection.Top:
                    return points[2];
                case NodeDirection.Bottom:
                    return points[0];
                case NodeDirection.Left:
                    return points[1];
                case NodeDirection.Right:
                    return points[3];
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void DrawConnections(PaintEventArgs e)
        {
            if (nextFalseBlock != null)
            {
                e.Graphics.DrawLine(Pens.Black, this.GetNode(NodeDirection.Left), nextFalseBlock.GetNode(NodeDirection.Top));
            }
            if (nextTrueBlock != null)
            {
                e.Graphics.DrawLine(Pens.Black, this.GetNode(NodeDirection.Right), nextTrueBlock.GetNode(NodeDirection.Top));

            }
        }

        public override void ConnectNodeToBlock(NodeDirection dir, BuildingBlock bb)
        {
            switch (dir)
            {
                case NodeDirection.Left:
                    nextTrueBlock = bb;
                    break;
                case NodeDirection.Right:
                    nextFalseBlock = bb;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private Point GetOrigo()
        {
            return new Point(points[0].X, points[1].Y);
        }
    }
}
