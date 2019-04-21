using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ShapeEditorLibrary.Shapes;
using System.Drawing;
using System.ComponentModel;
using ShapeEditorLibrary.Extensions;
using System.Drawing.Drawing2D;


namespace ShapeEditorLibrary
{
    class CreateImage
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

                ShapeCollection sc = new ShapeCollection(c);
                MessageBox.Show(sc[0].ToString());


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
