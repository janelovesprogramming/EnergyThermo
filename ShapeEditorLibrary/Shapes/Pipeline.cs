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
                Pen p = new Pen(Color.FromArgb(255, 0, 255, 0), 4);

                String drawString = this.Name;

                // Create font and brush.
                Font drawFont = new Font("Arial", 12);
                SolidBrush drawBrush = new SolidBrush(Color.Black);


                // Draw text to screen
                g.DrawString(drawString, drawFont, drawBrush, this.Bounds.X + this.Bounds.Width/3, this.Bounds.Y-30);

            }

        }
        public override void DrawText(System.Drawing.Graphics g)
        {
            using (var b = new SolidBrush(this.BackColor))
            {
                Pen p = new Pen(Color.Red, 4);
                g.DrawLine(p, this.Bounds.X, this.Bounds.Y, this.Bounds.X + this.Bounds.Width, this.Bounds.Y+this.Bounds.Height);
            }
        }
    }
}
