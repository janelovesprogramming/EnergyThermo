using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ShapeEditorLibrary
{
    public class SnapLine
    {
        public SnapLine(int x1, int y1, int x2, int y2, Color color)
        {
            this.X1 = x1;
            this.Y1 = y1;
            this.X2 = x2;
            this.Y2 = y2;
            this.Color = color;
        }

        #region Properties

        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public Color Color { get; set; }

	    #endregion

        #region Methods

        public void Draw(Graphics g)
        {
            using (var p = new Pen(this.Color))
            {
                g.DrawLine(p, this.X1, this.Y1, this.X2, this.Y2);
            }
        }

        #endregion
    }
}
