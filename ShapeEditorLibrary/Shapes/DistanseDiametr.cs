using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ShapeEditorLibrary.Shapes
{

    public class DistanseDiametr:Shape
    {
        public DistanseDiametr(Point location, Shape s)
            : base(location)
        {
            this.TextField = "sss";
            this.ShapeName = s;
        }

        private Shape _ShapeName;

        public Shape ShapeName
        {
            get { return _ShapeName; }
            set
            {
                _ShapeName = value;
            }

        }

        public override string GetShapeTypeName()
        {
            return "DistanseDiametr";
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            //MessageBox.Show(this.LogicField.ToString());
            if (this.ShapeName.LogicField)
            {
                using (var b = new SolidBrush(this.BackColor))
                {
                    Pen pen1 = new Pen(Color.Black, 2);

                    g.DrawLine(pen1, this.ShapeName.Bounds.X + this.ShapeName.Bounds.Width / 2, this.ShapeName.Bounds.Y + this.ShapeName.Bounds.Height / 2, this.Bounds.X, this.Bounds.Y + this.Bounds.Height);
                    g.DrawLine(pen1, this.Bounds.X, this.Bounds.Y + this.Bounds.Height, this.Bounds.X + this.Bounds.Width, this.Bounds.Y + this.Bounds.Height);
                    Pen p = new Pen(Color.FromArgb(255, 0, 255, 0), 4);

                    String drawString = "text\ntext";

                    // Create font and brush.
                    Font drawFont = this.FontField;
                    SolidBrush drawBrush = new SolidBrush(Color.Black);


                    // Draw text to screen
                    g.DrawString(drawString, drawFont, drawBrush, this.Bounds.X, this.Bounds.Y);


                }
            }

        }
        public override void DrawText(System.Drawing.Graphics g)
        {
           
        }
    }
}

