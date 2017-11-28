using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication2.BuildingBlocks;

namespace WindowsFormsApplication2
{
    enum BlockBuildingPhase { Nothing, ProcessBlock, ConnectionStart, ConnectionEnd, DecisionBlock};

    public partial class Form1 : Form
    {
        int gridSize = 20;
        private BlockBuildingPhase phase = BlockBuildingPhase.Nothing;
        List<BuildingBlock> buildingBlocks = new List<BuildingBlock>();
        BuildingBlock targetBlock = null;
        BuildingBlock.NodeDirection targetNode = BuildingBlock.NodeDirection.Bottom;

        public Form1()
        {
            InitializeComponent();
            panel1.Paint += this.mainPanel_PaintGrid;
            panel1.Paint += this.mainPanel_Paint;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            phase = BlockBuildingPhase.ProcessBlock;
            this.Cursor = Cursors.Cross;
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {
            foreach (BuildingBlock bb in buildingBlocks)
            {
                bb.Draw(e);
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
            buildingBlocks.Clear();
            panel1.Invalidate();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (phase == BlockBuildingPhase.ProcessBlock)
            {
                Point target = GetClosestGrid(e);
                buildingBlocks.Add(new ProcessBlock(target));
                phase = BlockBuildingPhase.Nothing;
                this.Cursor = Cursors.Default;
                panel1.Invalidate();
            } else if (phase == BlockBuildingPhase.ConnectionStart){
                targetBlock = GetBuildingBlock(e);
                if (targetBlock != null)
                {
                    phase = BlockBuildingPhase.ConnectionEnd;
                    if (targetBlock is ProcessBlock)
                        targetNode = BuildingBlock.NodeDirection.Bottom;
                    else if (targetBlock is DecisionBlock)
                    {
                        targetNode = ((DecisionBlock)targetBlock).GetNodeFromLocation(e.Location);
                    }
                }
            } else if (phase == BlockBuildingPhase.ConnectionEnd) {
                BuildingBlock nextTargetBlock = GetBuildingBlock(e);
                if (nextTargetBlock != null)
                {
                    targetBlock.ConnectNodeToBlock(targetNode, nextTargetBlock);
                    phase = BlockBuildingPhase.Nothing;
                    this.Cursor = Cursors.Default;
                    panel1.Invalidate();
                }
            } else if (phase == BlockBuildingPhase.DecisionBlock) {
                Point target = GetClosestGrid(e);
                buildingBlocks.Add(new DecisionBlock(target));
                phase = BlockBuildingPhase.Nothing;
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

        private BuildingBlock GetBuildingBlock (MouseEventArgs e)
        {
            foreach (BuildingBlock bb in buildingBlocks)
            {
                if (bb.Contains(e))
                {
                    return bb;
                }
            }
            return null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            phase = BlockBuildingPhase.ConnectionStart;
            this.Cursor = Cursors.VSplit;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            phase = BlockBuildingPhase.DecisionBlock;
            this.Cursor = Cursors.Cross;
        }
    }
}
