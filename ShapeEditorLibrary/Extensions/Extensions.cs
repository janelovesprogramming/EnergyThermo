using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ShapeEditorLibrary.Extensions
{
    internal static class Extensions
    {
        #region Point
        
        public static Point Add(this Point pt1, Point pt2)
        {
            return new Point(pt1.X + pt2.X, pt1.Y + pt2.Y);
        }

        public static Point Subtract(this Point pt1, Point pt2)
        {
            return pt1.Add(pt2.Negate());
        }

        public static Point Negate(this Point pt1)
        {
            return new Point(-pt1.X, -pt1.Y);
        }

        public static Point Floor(this Point pt, int gridSize)
        {
            var dividedX = pt.X / gridSize;
            var dividedY = pt.Y / gridSize;
            return new Point(dividedX*gridSize, dividedY*gridSize);
        }

        #endregion

        #region Integer

        public static bool IsBetween(this int x, int a, int b)
        {
            if (a > b) return (b <= x && a > x);
            if (a < b) return (a <= x && b > x);
            return false;
        }

        #endregion
    }
}
