using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ShapeEditorLibrary.Shapes
{
    public class RectangleShape : Shape
    {
        public RectangleShape(Point location) 
            : base(location)
        {
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            using (var b = new SolidBrush(this.BackColor))
            {
                g.FillRectangle(b, this.Bounds);
                g.DrawRectangle(Pens.Black, this.Bounds);
            }
        }
    }
}
