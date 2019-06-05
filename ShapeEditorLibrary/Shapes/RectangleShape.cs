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
        public override string GetShapeTypeName()
        {
            return "Object";
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            using (var b = new SolidBrush(this.BackColor))
            {
                g.FillRectangle(b, this.Bounds);
                g.DrawRectangle(Pens.Black, this.Bounds);
            }
        }
        public override void DrawText(System.Drawing.Graphics g)
        {
            using (var b = new SolidBrush(this.BackColor))
            {
                Pen p = new Pen(Color.FromArgb(255, 0, 255, 0), 4);

                String drawString = this.TextField;

                // Create font and brush.
                Font drawFont = new Font("Arial",10);
                SolidBrush drawBrush = new SolidBrush(Color.Black);


                // Draw text to screen
                g.DrawString(drawString, drawFont, drawBrush, this.Location.X, this.Location.Y + this.Size.Height/3);

            }
        }
    }
}
