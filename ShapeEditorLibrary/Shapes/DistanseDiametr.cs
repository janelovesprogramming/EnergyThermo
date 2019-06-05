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

                    g.DrawLine(pen1, this.ShapeName.Bounds.X + this.ShapeName.Bounds.Width / 2, this.ShapeName.Bounds.Y + this.ShapeName.Bounds.Height / 2, this.Bounds.X, this.Bounds.Y + this.Bounds.Height - 2);
                    g.DrawLine(pen1, this.Bounds.X, this.Bounds.Y + this.Bounds.Height - 2, this.Bounds.X + this.Bounds.Width + 20, this.Bounds.Y + this.Bounds.Height - 2);
                    Pen p = new Pen(Color.FromArgb(255, 0, 255, 0), 4);
                    double dis = 0;
                    if (this.ShapeName.GetShapeTypeName() == "Pipeline" || this.ShapeName.GetShapeTypeName() == "ObrPipeline" || this.ShapeName.GetShapeTypeName() == "Vodoprovod")
                    {
                        dis = Math.Sqrt(Math.Pow((this.ShapeName.Location.X + this.ShapeName.Size.Width) - this.ShapeName.Location.X, 2) + Math.Pow((this.ShapeName.Location.Y + this.ShapeName.Size.Height) - this.ShapeName.Location.Y, 2));
                        
                    }
                    String drawString = this.ShapeName.MyList+"\n" + Math.Round(dis,2);

                    // Create font and brush.
                    Font drawFont = new Font("Arial", 11);
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

