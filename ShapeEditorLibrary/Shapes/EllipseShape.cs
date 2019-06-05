using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ShapeEditorLibrary.Shapes
{
    public class EllipseShape : Shape 
    {
        public EllipseShape(Point location)
            : base(location)
        {
        }

        public override string GetShapeTypeName()
        {
            // Example: you can override this method to return a custom shape name, different from the class name.
            return "TK";
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            using (var b = new SolidBrush(Color.White))
            {
                g.FillEllipse(b, this.Bounds);
                g.DrawEllipse(Pens.Black, this.Bounds);
            }
        }

        public override void DrawText(System.Drawing.Graphics g)
        {
            using (var b = new SolidBrush(this.BackColor))
            {
                Pen p = new Pen(Color.FromArgb(255, 0, 255, 0), 4);

                String drawString = this.Name;

                // Create font and brush.
                Font drawFont = this.FontField;
                SolidBrush drawBrush = new SolidBrush(Color.Black);


                // Draw text to screen
                g.DrawString(drawString, drawFont, drawBrush, this.Bounds.X+50, this.Bounds.Y-20);

            }
        }
    }
}
