using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ShapeEditorLibrary.Shapes
{
    public class Pipeline : Shape 
    {
        public Pipeline(Point location)
            : base(location)
        {
        }

        public override string GetShapeTypeName()
        {            
            return "Pipeline";
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            using (var b = new SolidBrush(this.BackColor))
            {
                Pen p = new Pen(Color.Red,4);                
                g.DrawLine(p, this.Bounds.X, this.Bounds.Y, this.Bounds.X + this.Bounds.Width, this.Bounds.Y + this.Bounds.Height);
            }
        }
        public override void DrawText(System.Drawing.Graphics g)
        {
            using (var b = new SolidBrush(Color.Black))
            {
                Pen p = new Pen(Color.Black);
                g.DrawLine(p, this.Bounds.X + this.Bounds.Width/2, this.Bounds.Y + this.Bounds.Height/2, this.Bounds.X + this.Bounds.Width / 2,this.Bounds.Y);
                g.DrawLine(p, this.Bounds.X + this.Bounds.Width / 2, this.Bounds.Y, this.Bounds.Width, this.Bounds.Height);
            }
        }
    }
}
