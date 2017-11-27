using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    abstract class BuildingBlock
    {
        private BuildingBlock nextBuildingBlock = null;
        public WindowsFormsApplication2.BuildingBlock NextBuildingBlock
        {
            get { return nextBuildingBlock; }
            set { nextBuildingBlock = value; }
        }
        public void Execute()
        {

        }

        public abstract void Draw(PaintEventArgs e);
        public abstract bool Contains(MouseEventArgs e);

        public abstract Point GetInputNode();
        public abstract Point GetOutputNode();
    }
}
