using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

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
            return "Text";
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            using (var b = new SolidBrush(this.BackColor))
            {
                Pen p = new Pen(Color.FromArgb(255, 0, 255, 0), 4);
                
                String drawString = this.TextField;
                
                // Create font and brush.
                Font drawFont = this.FontField;
                SolidBrush drawBrush = new SolidBrush(Color.Black);
             

                // Draw text to screen
               g.DrawString(drawString, drawFont, drawBrush, this.Bounds.X, this.Bounds.Y);
                
            }
        }
        public override void DrawText(System.Drawing.Graphics g)
        {
            using (var b = new SolidBrush(Color.Black))
            {
                g.FillEllipse(b, this.Bounds);
                g.DrawEllipse(Pens.Black, this.Bounds);
            }
        }
    }
}
