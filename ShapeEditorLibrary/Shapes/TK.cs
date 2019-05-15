using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ShapeEditorLibrary.Shapes
{
    public class TK : Shape
    {
        public TK(Point location)
            : base(location)
        {
        }

        public override string GetShapeTypeName()
        {
            return "Compensator";
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            using (var b = new SolidBrush(this.BackColor))
            {
                Image newImage = Image.FromFile("C:\\Users\\Игорь\\Desktop\\EnergyThermo\\ShapeEditorLibrary\\Resources\\Image1.bmp");

                g.DrawImageUnscaled(newImage, this.Bounds.X, this.Bounds.Y);
            }
        }
        public override void DrawText(System.Drawing.Graphics g)
        {
            using (var b = new SolidBrush(Color.White))
            {
                g.FillEllipse(b, this.Bounds);
                g.DrawEllipse(Pens.Black, this.Bounds);
            }
        }
    }
}
