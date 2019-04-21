using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ShapeEditorLibrary.Shapes
{
    public class CreateImage 
    {
        private Graphics g;
        private Pen pb;
        private Bitmap b;
        private Font f, f1;
        private PointF p;
        private Brush br;


        public Bitmap Draw_Graph(int wid, int hgt, Canvas c)
        {
            Bitmap bm = new Bitmap(wid, hgt);
            Pen p = new Pen(Color.FromArgb(255, 0, 255, 0), 4);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                gr.Clear(Color.White);

                
                MessageBox.Show(c.Shapes[1].Name.ToString());
                for (int i = 0; i < c.Shapes.Count; i++)
                {
                    if (c.Shapes[i].GetShapeTypeName() == "Pipeline")
                    {
                         //g.DrawLine(p, c.Shapes[i].Location.X, c.Shapes[i].Location.Y, c.Shapes[i].Location.X + c.Shapes[i].Size.Width, c.Shapes[i].Location.Y + c.Shapes[i].Size.Height);
                    }
                }
                   

                return bm;
            }
        }
        public void clear_pic()
        {
            g.Clear(Color.White);
        }
        public Bitmap GetBitmap()
        {
            return b;
        }
    }
}
