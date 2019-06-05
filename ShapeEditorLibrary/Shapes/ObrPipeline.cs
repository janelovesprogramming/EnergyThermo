using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ShapeEditorLibrary.Shapes
{
    public class ObrPipeline : Shape
    {
        public ObrPipeline(Point location)
            : base(location)
        {
        }

        public override string GetShapeTypeName()
        {
            return "ObrPipeline";
        }

        public override void Draw(System.Drawing.Graphics g)
        {


        }
        public override void DrawText(System.Drawing.Graphics g)
        {
            using (var b = new SolidBrush(this.BackColor))
            {

                Pen p = new Pen(Color.Blue, 4);
                g.DrawLine(p, this.Bounds.X, this.Bounds.Y, this.Bounds.X + this.Bounds.Width, this.Bounds.Y + this.Bounds.Height);
            }
        }
    }
}
