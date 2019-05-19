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
using Npgsql;

using System.Data.Common;


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
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //this.AddShape(new Pipeline(Point.Empty));
            Shape pip = new Pipeline(Point.Empty);
            Shape dis = new DistanseDiametr(Point.Empty, pip);

            this.AddShape(dis);
            this.AddShape(pip);

          



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
            DialogResult res = MessageBox.Show("Вы хотите выйти из программы?", "EnergyThermo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (res == DialogResult.OK) Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы хотите выйти из программы?", "EnergyThermo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (res == DialogResult.Cancel) e.Cancel = true;
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
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
                        int width = canvas1.Size.Width;
                        int height = canvas1.Size.Height;

                        Bitmap bm = new Bitmap(width, height);
                        canvas1.DrawToBitmap(bm, new Rectangle(0, 0, width, height));

                        bm.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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
            //MessageBox.Show(s[1].ToString());
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Shape> s = canvas1.GetShapes();
            DataSet ds = new DataSet("Scheme");
            DataTable dt = new DataTable("Pipeline");
            ds.Tables.Add(dt);

            DataRow Row;
            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "LocationX";
            column.AutoIncrement = false;
            column.ReadOnly = false;
            column.Unique = false;
      
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "LocationY";
            column.AutoIncrement = false;
            column.ReadOnly = false;
            column.Unique = false;

            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "SizeH";
            column.AutoIncrement = false;
            column.ReadOnly = false;
            column.Unique = false;


            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "SizeW";
            column.AutoIncrement = false;
            column.ReadOnly = false;
            column.Unique = false;


            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Name";
            column.AutoIncrement = false;
            column.ReadOnly = false;
            column.Unique = false;

            dt.Columns.Add(column);


            for (int i = 0; i < s.Count; i++)
            {

                if(s[i].GetShapeTypeName() == "Pipeline")
                {
                    Row = dt.NewRow();

                    Row["Name"] = s[i].Name;
                    Row["LocationX"] = s[i].Location.X;
                    Row["LocationY"] = s[i].Location.Y;
                    Row["SizeW"] = s[i].Size.Width;
                    Row["SizeH"] = s[i].Size.Height;

                    dt.Rows.Add(Row);

                }
            }
                //MessageBox.Show(s[0].TextField.ToString());
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XML-File | *.xml";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    ds.WriteXml(saveFileDialog.FileName);

            MessageBox.Show("Данные сохранены в файл");
        }

        private void загрузитьИхXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {

            List<Shape> s = new List<Shape>();
           
            List<Shape> old_s = canvas1.GetShapes();
            for(int i = 0; i < old_s.Count; i++)
                canvas1.RemoveShape(old_s[i]);

            DataSet ds = new DataSet();
            
            openFileDialog1.Filter = "XML-File | *.xml";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            // читаем файл в строку
            ds.ReadXml(filename);
            DataTable dt = ds.Tables[0];
            DataRow[] rows = dt.Select();


            // Print the value one column of each DataRow.
            for (int i = 0; i < rows.Length; i++)
            {
                
                Point p = new Point(Convert.ToInt32(rows[i]["LocationX"]), Convert.ToInt32(rows[i]["LocationY"]));
                Size siz = new Size(Convert.ToInt32(rows[i]["SizeW"]), Convert.ToInt32(rows[i]["SizeH"]));

                Pipeline pip = new Pipeline(p);
                pip.Name = rows[i]["Name"].ToString();
                
                pip.Location = p;
                pip.Size = siz;
                s.Add(pip);
                canvas1.Shapes.Add(s[i]);

            }
            
            
            MessageBox.Show("Файл открыт");
        }
    }
    }
