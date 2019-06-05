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
using Xceed.Words.NET;
using System.Data.Common;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;



namespace ShapeEditorTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            splitContainer1.Panel1.Controls.Add(gMapControl1);
            gMapControl1.Controls.Add(canvas1);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            canvas1.BackColor = Color.Transparent;
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

            

            pip.MyList = 108.ToString();

            canvas1.SendToBack(pip);





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
            DataTable dt_obr = new DataTable("ObrPipeline");
            DataTable dt_vod = new DataTable("Vodoprovod");
            DataTable dt_tk = new DataTable("TK");
            DataTable dt_zadvizhka = new DataTable("Z");
            DataTable dt_text = new DataTable("Text");
            DataTable dt_obj= new DataTable("Obj");
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

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Method";
            column.AutoIncrement = false;
            column.ReadOnly = false;
            column.Unique = false;

            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.DateTime");
            column.ColumnName = "DYear";
            column.AutoIncrement = false;
            column.ReadOnly = false;
            column.Unique = false;

            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Boolean");
            column.ColumnName = "Show";
            column.AutoIncrement = false;
            column.ReadOnly = false;
            column.Unique = false;

            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Dn";
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
                    Row["Show"] = s[i].LogicField;
                    Row["DYear"] = s[i].Dyear;
                    Row["Method"] = s[i].Method;
                    Row["Dn"] = s[i].MyList;
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
                pip.LogicField = Convert.ToBoolean(rows[i]["Show"]);
                if(pip.LogicField == true)
                {
                    Shape dis = new DistanseDiametr(Point.Empty, pip);
                    this.AddShape(dis);
                }
                pip.Method = rows[i]["Method"].ToString();
                pip.Dyear = Convert.ToDateTime(rows[i]["DYear"]);
                pip.MyList = rows[i]["Dn"].ToString();
                
                s.Add(pip);
                canvas1.Shapes.Add(s[i]);
                
            }
            
            
            MessageBox.Show("Файл открыт");
        }

        private void добавитьГидравлическоеКольцоToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //this.AddShape(new Pipeline(Point.Empty));
            Point p = new Point(100, 100);
            Shape pip = new ObrPipeline(p);
            Shape dis = new DistanseDiametr(p, pip);

            this.AddShape(dis);
            this.AddShape(pip);

            pip.MyList = 108.ToString();
            canvas1.SendToBack(pip);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //this.AddShape(new Pipeline(Point.Empty));
            Shape pip = new Vodoprovod(Point.Empty);
            Shape dis = new DistanseDiametr(Point.Empty, pip);

            this.AddShape(dis);
            this.AddShape(pip);

            pip.MyList = 108.ToString();
            canvas1.SendToBack(pip);

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            this.AddShape(new Manometr(Point.Empty));
        }

        private void вWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            /*  int width = canvas1.Size.Width;
              int height = canvas1.Size.Height;

              Bitmap bm = new Bitmap(width, height);
              canvas1.DrawToBitmap(bm, new Rectangle(0, 0, width, height));
           bm.Save("C:\\Users\\Игорь\\Desktop\\www.png", System.Drawing.Imaging.ImageFormat.Jpeg);


          // путь к документу
          string pathDocument = "C:\\Users\\Игорь\\Desktop\\www.docx";

          // создаём документ
          DocX document = DocX.Load(pathDocument);
          Xceed.Words.NET.Image image = document.AddImage("C:\\Users\\Игорь\\Desktop\\www.png");
          // вставляем параграф и передаём текст
          document.InsertParagraph("Описание схемы тепловой сети").
                   // устанавливаем шрифт
                   Font("Times New Roman").
                   // устанавливаем размер шрифта
                   FontSize(14).
                   // устанавливаем цвет
                   Color(Color.Black).
                   // делаем текст жирным
                   Bold().                    
                   // выравниваем текст по центру
                   Alignment = Alignment.center;

          Paragraph paragraph = document.InsertParagraph();
          // вставка изображения в параграф
          paragraph.AppendPicture(image.CreatePicture());
          // выравнивание параграфа по центру
          paragraph.Alignment = Alignment.center;

          document.Save();*/
        }

        private void скрытьСхемуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            canvas1.Visible = false;
            gMapControl1.Visible = true;
        }

        private void показатьСхемуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            canvas1.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            gMapControl1.Bearing = 45;

            gMapControl1.CanDragMap = true;

            gMapControl1.DragButton = MouseButtons.Left;

            gMapControl1.GrayScaleMode = true;

            gMapControl1.MarkersEnabled = true;

            gMapControl1.MaxZoom = 18;

            gMapControl1.MinZoom = 2;

            gMapControl1.MouseWheelZoomType = MouseWheelZoomType.MousePositionWithoutCenter;

            gMapControl1.NegativeMode = false;

            gMapControl1.PolygonsEnabled = true;

            gMapControl1.RoutesEnabled = true;

            gMapControl1.ShowTileGridLines = false;

            gMapControl1.Zoom = 10;
            gMapControl1.ShowCenter = false;
            gMapControl1.MapProvider = GMap.NET.MapProviders.GMapProviders.GoogleMap;

            GMap.NET.GMaps.Instance.Mode = AccessMode.ServerOnly;

            gMapControl1.Position = new PointLatLng(57.1425322914133, 65.5206906795502);
            gMapControl1.Visible = false;
        }

        public bool CanvasProp
        {
            get { return gMapControl1.Visible; }
            set { gMapControl1.Visible = value; }
        }
        public static Form2 CreateForm2()
        {
            foreach (Form frm in Application.OpenForms)
                if (frm is Form2)
                {
                    frm.Activate();
                    return frm as Form2;
                }
            Form2 form = new Form2();
            form.Show();
            return form;
        }
        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateForm2();
            Form2 f2 = new Form2();
            f2.Hide();
        }
    }

}
    
