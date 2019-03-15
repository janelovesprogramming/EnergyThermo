using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ShapeEditorLibrary.Shapes
{
    public class Text : Shape
    {
        public Text(Point location)
            : base(location)
        {
        }

        public override string GetShapeTypeName()
        {
            // Example: you can override this method to return a custom shape name, different from the class name.
            return "Text";
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            using (var b = new SolidBrush(this.BackColor))
            {
                Pen p = new Pen(Color.FromArgb(255, 0, 255, 0), 4);
                //g.FillEllipse(b, this.Bounds);
                String drawString = "Sample Text";
                
                // Create font and brush.
                Font drawFont = new Font("Arial", 16);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
             

                // Set format of string.
                StringFormat drawFormat = new StringFormat();
                drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;

                // Draw string to screen.
                g.DrawString(drawString, drawFont, drawBrush, this.Bounds.X, this.Bounds.Y, drawFormat);
            }
        }
    }
}
