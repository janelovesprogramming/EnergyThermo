using System.Drawing;

namespace ShapeEditorLibrary.Shapes
{
    public class TriangleShape : Shape
    {
        public TriangleShape(Point location)
            : base(location)
        {
        }
        public override string GetShapeTypeName()
        {
            return "TriangleShape";
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            float middle = this.Bounds.X + this.Bounds.Width / 2f;

            PointF left = new PointF(this.Bounds.Left, this.Bounds.Bottom);
            PointF right = new PointF(this.Bounds.Right, this.Bounds.Bottom);
            PointF top = new PointF(middle, this.Bounds.Top);

            using (var b = new SolidBrush(Color.Black))
            {
                g.FillPolygon(b, new PointF[] { left, right, top });
            }
        }
    }
}
