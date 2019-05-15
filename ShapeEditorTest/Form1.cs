using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ShapeEditorLibrary.Shapes;
using ShapeEditorLibrary;

namespace ShapeEditorTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void AddShape(Shape s)
        {
            canvas1.Shapes.Add(s);
            canvas1.Invalidate();
            
        }

        private void addRectangleButton_Click(object sender, EventArgs e)
        {
            this.AddShape(new RectangleShape(Point.Empty));
        }

        private void addEllipseButton_Click(object sender, EventArgs e)
        {
            this.AddShape(new EllipseShape(Point.Empty));
        }

        private void addTriangleButton_Click(object sender, EventArgs e)
        {
            this.AddShape(new TriangleShape(Point.Empty));
        }

        private void canvas1_ShapesCollectionChanged(object sender, EventArgs e)
        {
            shapesComboBox1.Items.Clear();
            shapesComboBox1.Items.AddRange(canvas1.Shapes.ToArray());
        }

        private void canvas1_SelectedShapeChanged(object sender, EventArgs e)
        {
            Shape s = canvas1.SelectedShape;
            shapesComboBox1.SelectedItem = s;
            propertyGrid1.SelectedObject = s;
            
        }

        private void shapesComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Shape s = shapesComboBox1.SelectedItem as Shape;
            if (s != null)
            {
                canvas1.SetSelection(s);
            }
            else
            {
                canvas1.SetSelection(null);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //this.AddShape(new Pipeline(Point.Empty));
            this.AddShape(new Pipeline(Point.Empty));
            
        }

       

        private void canvas1_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Shape s = canvas1.SelectedShape;
            if(s!=null)
                canvas1.RemoveShape(s);
        }

        private void наПереднийПланToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Shape s = canvas1.SelectedShape;
            if (s != null)
                canvas1.BringToFront(s);
        }

        private void наЗаднийПланToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Shape s = canvas1.SelectedShape;
            if (s != null)
                canvas1.SendToBack(s);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.AddShape(new Text(Point.Empty));
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.AddShape(new TK(Point.Empty));
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы хотите выйти из программы?", "EnergyThermo", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
            if (res == DialogResult.OK) Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы хотите выйти из программы?", "Учет сотрудников на предприятии", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
            if (res == DialogResult.Cancel) e.Cancel = true;
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            CreateImage ci = new CreateImage();

            pic.Image = ci.Draw_Graph(canvas1.Width, canvas1.Height, canvas1);
            /*
            if (pic.Image != null)
            {
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Сохранить картинку как...";
                savedialog.OverwritePrompt = true;
                savedialog.CheckPathExists = true;
                savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pic.Image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }*/

        }

        
        private void сеткаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics g = canvas1.CreateGraphics();
            canvas1.PaintPoint(g);
        }

       

        private void canvas1_MouseClick(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            List<Shape> s = canvas1.GetShapesAtPoint(p);
            MessageBox.Show(s[1].ToString());
        }
    }
    }
