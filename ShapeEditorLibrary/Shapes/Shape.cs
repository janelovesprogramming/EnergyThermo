using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Drawing.Design;
using Npgsql;
using System.Data;
using System.Data.Common;



namespace ShapeEditorLibrary.Shapes
{
    public abstract class Shape
    {
        protected Shape() : this(Point.Empty)
        {
        }
        private PropertyFontConfig propfonconfig;

        protected Shape(Point location)
        {
            string type_f = GetShapeTypeName();
            this.MinimumSize = new Size(10, 10);
            if (type_f == "TK")
            {
                this.Bounds = new Rectangle(location, new Size(40, 40));
            }
            else if (type_f == "Object")
            {
                this.Bounds = new Rectangle(location, new Size(90, 40));
            }
            else if (type_f == "Manometr")
            {
                this.Bounds = new Rectangle(location, new Size(42, 70));
            }
            else 
            {
                
                this.Bounds = new Rectangle(location, this.DefaultSize);
            }
            this.BackColor = Color.White;
            this.Locked = false;
            this.FontField = new Font("Arial", 16);
            this.TextField = "Текст";
            this.m_LogicField = false;
            /*
            if (type_f != "Text" || type_f != "Object")
            {
                HiddenProp("FontField", false);
                HiddenProp("TextField", false);
            }
            */
        }

        #region Enums

        public enum HitStatus
        {
            None,
            Drag,
            ResizeTopLeft,
            ResizeTopRight,
            ResizeBottomLeft,
            ResizeBottomRight,
            ResizeLeft,
            ResizeTop,
            ResizeRight,
            ResizeBottom
        }

        #endregion

        #region Events

        public event EventHandler LocationChanged;
        public event EventHandler SizeChanged;
        public event EventHandler AppearanceChanged;
        public event EventHandler FontChange;
        public event EventHandler TextChanged;

        protected virtual void OnLocationChanged(EventArgs e)
        {
            if (this.LocationChanged != null) this.LocationChanged(this, e);
        }

        protected virtual void OnTextChanged(EventArgs e)
        {
            if (this.TextChanged != null) this.TextChanged(this, e);
        }

        protected virtual void OnSizeChanged(EventArgs e)
        {
            if (this.SizeChanged != null) this.SizeChanged(this, e);
        }

        protected virtual void OnAppearanceChanged(EventArgs e)
        {
            if (this.AppearanceChanged != null) this.AppearanceChanged(this, e);
        }

        protected virtual void OnFontChanged(EventArgs e)
        {
            if (this.FontChange != null) this.FontChange(this, e);
        }

        #endregion

        #region Properties

        private string _Name = String.Empty;
        /// <summary>
        /// The Name of this Shape.
        /// </summary>
        [DisplayName("1. Наименование")]
        public string Name
        {
            get { return _Name; }
            set
            {
                if (value.Trim() == String.Empty)
                    throw new ArgumentException("Name cannot be empty.");
                _Name = value;
            }
        }

        Double _cof;
        [Browsable(true)]
        [Description("Коэффициент сопротивления")]
        [DisplayName("Коэффициент сопротивления задвижки")]
        public Double Cof
        {
            get { return _cof; }
            set
            {

                _cof = value;
                this.OnSizeChanged(EventArgs.Empty);
            }
        }

        Double _pre;
        [Browsable(true)]
        [Description("Давление")]
        [DisplayName("Давление в трубопроводе")]
        public Double Pre
        {
            get { return _pre; }
            set
            {

                _pre = value;
                this.OnSizeChanged(EventArgs.Empty);
            }
        }
        String m_TextField;
        [Browsable(true)]
        [Description("Текстовое поле")]
        [DisplayName("Текст")]
        public String TextField
        {
            get { return m_TextField; }
            set {

                m_TextField = value;

                this.OnSizeChanged(EventArgs.Empty);
                Rectangle rect = new Rectangle(this.Bounds.X, this.Bounds.Y, (int)m_FontField.Size * m_TextField.Length, m_FontField.Height);
                this.GrabHandles.SetBounds(rect, this);
            }
        }


     
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        public string _dvn;
        public string _dy;
        public string _thickness;
        public string _weight;
        public string _square_area;
        public string _square_cross;
        public string _material;
        public string myList;

        [Browsable(true)]
        [DisplayName("Материал")]
        [Description("Материал из которого изготовлена труба")]
        [Category("Характеристика стальной трубы")]
        public string material
        {
            get
            {
                return _material;
            }

        }

        [Browsable(true)]
        [DisplayName("Площадь пнаружной поверхности")]
        [Description("Площадь пнаружной поверхности трубы 1 м в см2")]
        [Category("Характеристика стальной трубы")]
        public string square_area
        {
            get
            {
                return _square_area;
            }

        }

        [Browsable(true)]
        [DisplayName("Площадь поперечного сечения")]
        [Description("Площадь поперечного сечения стенки трубы 1см2")]
        [Category("Характеристика стальной трубы")]
        public string square_cross
        {
            get
            {
                return _square_cross;
            }

        }

        [Browsable(true)]
        [DisplayName("Масса трубы")]
        [Description("Масса трубы за 1 метр")]
        [Category("Характеристика стальной трубы")]
        public string weight
        {
            get
            {
                return _weight;
            }

        }

        [Browsable(true)]
        [DisplayName("Толщина стенки трубы")]
        [Category("Характеристика стальной трубы")]
        public string thickness
        {
            get
            {
                return _thickness;
            }

        }
        [Browsable(true)]
        [DisplayName("Диаметр трубопровода")]
        [Description("Условный диаметр трубопровода")]
        [Category("Характеристика стальной трубы")]
        [TypeConverter(typeof(DiametrTypeConverter))]
        public string MyList
        {
            get
            {
                return myList;
            }
            set
            {

               
                myList = value;
                this.OnSizeChanged(EventArgs.Empty);
                try
                {
                    List<string> myList = new List<string>();
                    
                    // PostgeSQL-style connection string
                    string connstring = String.Format("Server = 127.0.0.1; Port = 5432; User Id = postgres; Password =; Database = energy_thermo; ");
                    // Making connection with Npgsql provider
                    NpgsqlConnection conn = new NpgsqlConnection(connstring);
                    conn.Open();
                    // quite complex sql statement
                    string sql = "SELECT dy, dvn, dn, thickness, weight,square_cross,square_area,material FROM steel_tubes WHERE dn = " + this.MyList;
                    // data adapter making request from our connection
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                    // i always reset DataSet before i do
                    // something with it.... i don't know why :-)
                    ds.Reset();
                    // filling DataSet with result from NpgsqlDataAdapter
                    da.Fill(ds);
                    // since it C# DataSet can handle multiple tables, we will select first
                    dt = ds.Tables[0];


                    
                    myList.Add(dt.Rows[0].Field<int>("dvn").ToString());
                    myList.Add(dt.Rows[0].Field<int>("dy").ToString());
                   myList.Add(dt.Rows[0].Field<double>("thickness").ToString());
                   myList.Add(dt.Rows[0].Field<double>("weight").ToString());
                    myList.Add(dt.Rows[0].Field<double>("square_area").ToString());
                    myList.Add(dt.Rows[0].Field<double>("square_cross").ToString());
                    myList.Add(dt.Rows[0].Field<string>("material"));
                    _dvn = myList[0];
                    _dy = myList[1];
                    _thickness = myList[2];
                    _weight = myList[3];
                    _square_area = myList[4];
                    _square_cross = myList[5];
                    _material = myList[5];


                }
                catch (Exception msg)
                {
                    // something went wrong, and you wanna know why
                    throw;
                }

                this.OnAppearanceChanged(EventArgs.Empty);

            }
        }
        

        public string myList1;
        [Browsable(true)]
        [DisplayName("Способ прокладки")]
        [Description("Способ прокладки трубопровода")]
        [TypeConverter(typeof(MethodConvertor))]
        public string Method
        {
            get
            {
                return myList1;
            }
            set
            {


                myList1 = value;
                this.OnSizeChanged(EventArgs.Empty);
               
                this.OnAppearanceChanged(EventArgs.Empty);

            }
        }
        [Browsable(true)]
        [DisplayName("Условный диаметр")]
        [Description("Условный диаметр трубопровода")]
        [Category("Характеристика стальной трубы")]
        public string Dy
        {
            get
            {
                return _dy;
            }

        }


        [Browsable(true)]
        [DisplayName("Внутренний диаметр")]
        [Description("Внутренний диаметр трубопровода")]
        [Category("Характеристика стальной трубы")]
        public string Dvn
        {
            get
            {
                return _dvn;
            }
            
        }

        private Rectangle _Bounds;
        /// <summary>
        /// The location and size of this Shape.
        /// </summary>

        
        [Browsable(false)]
        public Rectangle Bounds
        {
            get { return _Bounds; }
            set
            {
                _Bounds = value;
                this.GrabHandles.SetBounds(value,this);
            }
        }

        
        bool flag_font;
        Font m_FontField;
        /// <summary>
        /// The font of text.
        /// </summary>
        [Browsable(true)]
        [Description("Example of font field")]
        [DisplayName("Font field")]
        public Font FontField
        {
            get { return m_FontField; }
            set {
                m_FontField = value;
                this.OnSizeChanged(EventArgs.Empty);
                
            }

        }


        /// <summary>
        /// The Location of this Shape.
        /// </summary>
        [DisplayName("2. Расположение")]
        [XmlIgnore]        
        public Point Location
        {
            get { return this.Bounds.Location; }
            set
            {
                if (this.Bounds.Location == value) return;
                string type_f = GetShapeTypeName();
                if (type_f != "Text")
                {
                    Rectangle rect = this.Bounds;
                    rect.Location = value;
                    this.Bounds = rect;
                    
                }
                else
                {               
                    Rectangle rect = new Rectangle(this.Bounds.X, this.Bounds.Y, (int) m_FontField.Size * m_TextField.Length, m_FontField.Height);
                    rect.Location = value;
                    this.Bounds = rect;
                    this.GrabHandles.SetBounds(rect,this);
                }
                this.OnLocationChanged(EventArgs.Empty);
            }
        }

        public DateTime _d;
        [Description("Год прокладки")]
        [DisplayName("Год")]
        public DateTime Dyear
        {
            get { return _d; }
            set
            {

                _d.ToString("yyyy");
                _d = value;

            }
        }

        public double m_Distance;
        [Browsable(false)]
        [Description("3. Длина трубопровода")]
        [DisplayName("Длина")]
        public double Distance
        {
            get { return m_Distance; }
        }

        /// <summary>
        /// The Size of this Shape.
        /// </summary>
        [DisplayName("Размер")]
        [XmlIgnore]
        public Size Size
        {
            get { return this.Bounds.Size; }
            set
            {
                if (this.Bounds.Size == value) return;

                Rectangle rect = this.Bounds;
                rect.Size = value;
                this.Bounds = rect;


                this.OnSizeChanged(EventArgs.Empty);
            }
        }

        bool m_LogicField;
        [Browsable(true)]
        [Description("Отобразить диаметр трубопровода")]
        [DisplayName("Показать")]
        public bool LogicField
        {
            get { return m_LogicField; }
            set {
                
                m_LogicField = value;
                
                //MessageBox.Show(m_LogicField.ToString());
                this.OnSizeChanged(EventArgs.Empty);
                this.OnLocationChanged(EventArgs.Empty);
            }
        }


        private bool locked;

        /// <summary>
        /// Whether this Shape is locked (moving disabled) or not.
        /// </summary>
        [DisplayName("Заблокировать")]
        public virtual bool Locked
        {
            get { return locked; }
            set 
            { 
                locked = value;
                this.GrabHandles.Locked = value;
                
            }
        }

        private GrabHandles _GrabHandles;
        /// <summary>
        /// The GrabHandles used to move or resize this Shape.
        /// </summary>
        [XmlIgnore]
        internal GrabHandles GrabHandles
        {
            get
            {
                string type_f = GetShapeTypeName();
                //if (type_f != "Text")
                //{
                    if (_GrabHandles == null) _GrabHandles = new GrabHandles(this);


                    return _GrabHandles;
                //}
                //return null;
            }
        }

    


        private Size _MinimumSize;
        /// <summary>
        /// The minimum size that this Shape can get while resizing.
        /// </summary>
        [Browsable(false)]
        public Size MinimumSize
        {
            get { return _MinimumSize; }
            set
            {
                if (value.Width < 0 || value.Height < 0)
                    throw new ArgumentOutOfRangeException("MinimumSize Width or Height must be at least zero.");
                _MinimumSize = value;
            }
        }

        internal Point MoveStart { get; set; }

        [XmlIgnore]
        protected virtual Size DefaultSize
        {
            get { return new Size(20, 20); }
        }

        /// <summary>
        /// The background color of this Shape.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public Color BackColor { get; set; }

        [Browsable(false)]
        public int XmlBackColor
        {
            get { return this.BackColor.ToArgb(); }
            set { this.BackColor = Color.FromArgb(value); }
        }

        /// <summary>
        /// The ContextMenuStrip shown when right-clicked.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public ContextMenuStrip ContextMenuStrip { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Draws this Shape.
        /// </summary>
        /// <param name="g">The Graphics object provided to draw this Shape.</param>
        public abstract void Draw(Graphics g);

        public abstract void DrawText(Graphics g);

        internal virtual void DrawGrabHandles(Graphics g, bool firstSelection)
        {
            /*
            string type_f = GetShapeTypeName();
            if (type_f == "Text" || type_f == "Object")
            {
                HiddenProp("FontField", true);
                HiddenProp("TextField", true);
            }
            else
            {
                
                HiddenProp("FontField", false);
                HiddenProp("TextField", false);
            }
            */
            this.GrabHandles.Draw(g, firstSelection,this);
          
        }

        public void Move(Point newLocation)
        {
            if (this.Locked) return;
            this.Location = newLocation;
        }

        #region Resizing

        /// <summary>
        /// Resizes this Shape.
        /// </summary>
        /// <param name="hitStatus">Which kind of resizing needs to be done.</param>
        /// <param name="mouseLocation">The current mouse location.</param>
        public void Resize(HitStatus hitStatus, Point mouseLocation)
        {
            
            this.Resize(hitStatus, mouseLocation.X, mouseLocation.Y);
        }

        /// <summary>
        /// Resizes this Shape.
        /// </summary>
        /// <param name="hitStatus">Which kind of resizing needs to be done.</param>
        /// <param name="x">The X coordinate of the current mouse location.</param>
        /// <param name="y">The X coordinate of the current mouse location.</param>
        public void Resize(HitStatus hitStatus, int x, int y)
        {
            if (this.Locked) return;

            switch (hitStatus)
            {
                case HitStatus.ResizeBottomLeft:
                    this.ResizeBottomLeft(x, y);
                    break;

                case HitStatus.ResizeBottomRight:
                    this.ResizeBottomRight(x, y);
                    break;

                case HitStatus.ResizeTopLeft:
                    this.ResizeTopLeft(x, y);
                    break;

                case HitStatus.ResizeTopRight:
                    this.ResizeTopRight(x, y);
                    break;

                case HitStatus.ResizeLeft:
                    this.ResizeLeft(x, y);
                    break;

                case HitStatus.ResizeRight:
                    this.ResizeRight(x, y);
                    break;

                case  HitStatus.ResizeTop:
                    this.ResizeTop(x, y);
                    break;

                case HitStatus.ResizeBottom:
                    this.ResizeBottom(x, y);
                    break;
            }
        }
        private bool MovingStartEndPoint = false;
        private void ResizeBottomLeft(int x, int y)
        {
            Rectangle oldBounds = this.Bounds;
            int newTop = oldBounds.Top;
            int newLeft = x;
            int newWidth = oldBounds.Right - x;
            int newHeight = y - oldBounds.Top;
            string type_f = GetShapeTypeName();
            //MessageBox.Show(type_f);
                         
                newWidth = this.MinimumSize.Width;
                newLeft = oldBounds.Right - newWidth;
            
            
                newHeight = this.MinimumSize.Height;
            
          
            this.Bounds = new Rectangle(newLeft, newTop, newWidth, newHeight);
        }

        private void ResizeBottomRight(int x, int y)
        {
            Rectangle oldBounds = this.Bounds;
            int newTop = oldBounds.Top;
            int newLeft = oldBounds.Left;
            int newWidth = x - newLeft;
            int newHeight = y - oldBounds.Top;
            string type_f = GetShapeTypeName();
            if (newWidth < this.MinimumSize.Width && type_f != "ObrPipeline" && type_f != "Pipeline" && type_f != "Vodoprovod")
            {
                newWidth = this.MinimumSize.Width;
            }
            if (newHeight < this.MinimumSize.Height && type_f != "ObrPipeline" && type_f != "Pipeline" && type_f != "Vodoprovod")
            {
                newHeight = this.MinimumSize.Height;
            }
           
            this.Bounds = new Rectangle(newLeft, newTop, newWidth, newHeight);
        }

        private void ResizeTopLeft(int x, int y)
        {
            Rectangle oldBounds = this.Bounds;
            int newTop = y;
            int newLeft = x;
            int newWidth = oldBounds.Right - x;
            int newHeight = oldBounds.Bottom - y;
            string type_f = GetShapeTypeName();
            if (newWidth < this.MinimumSize.Width && type_f != "ObrPipeline" && type_f != "Pipeline" && type_f != "Vodoprovod")
            {
                newWidth = this.MinimumSize.Width;
                newLeft = oldBounds.Right - newWidth;
            }
            if (newHeight < this.MinimumSize.Height && type_f != "ObrPipeline" && type_f != "Pipeline" && type_f != "Vodoprovod")
            {
                newHeight = this.MinimumSize.Height;
                newTop = oldBounds.Bottom - newHeight;
            }
           
            this.Bounds = new Rectangle(newLeft, newTop, newWidth, newHeight);
        }

        private void ResizeTopRight(int x, int y)
        {
            Rectangle oldBounds = this.Bounds;
            int newTop = y;
            int newLeft = oldBounds.Left;
            int newWidth = x - newLeft;
            int newHeight = oldBounds.Bottom - y;
            string type_f = GetShapeTypeName();
            
                newWidth = this.MinimumSize.Width;
            
            
                newHeight = this.MinimumSize.Height;
                newTop = oldBounds.Bottom - newHeight;
            
            
            this.Bounds = new Rectangle(newLeft, newTop, newWidth, newHeight);
        }

        private void ResizeTop(int x, int y)
        {
            Rectangle oldBounds = this.Bounds;
            int newTop = y;
            int newLeft = oldBounds.Left;
            int newWidth = oldBounds.Width;
            int newHeight = oldBounds.Bottom - y;

            if (newHeight < this.MinimumSize.Height)
            {
                newHeight = this.MinimumSize.Height;
                newTop = oldBounds.Bottom - newHeight;
            }
            this.Bounds = new Rectangle(newLeft, newTop, newWidth, newHeight);
        }

        private void ResizeLeft(int x, int y)
        {
            Rectangle oldBounds = this.Bounds;
            int newTop = oldBounds.Top;
            int newLeft = x;
            int newWidth = oldBounds.Right - x;
            int newHeight = oldBounds.Height;

            if (newWidth < this.MinimumSize.Width)
            {
                newWidth = this.MinimumSize.Width;
                newLeft = oldBounds.Right - newWidth;
            }
            this.Bounds = new Rectangle(newLeft, newTop, newWidth, newHeight);
        }

        private void ResizeRight(int x, int y)
        {
            Rectangle oldBounds = this.Bounds;
            int newTop = oldBounds.Top;
            int newLeft = oldBounds.Left;
            int newWidth = x - newLeft;
            int newHeight = oldBounds.Height;

            if (newWidth < this.MinimumSize.Width)
            {
                newWidth = this.MinimumSize.Width;
            }
            this.Bounds = new Rectangle(newLeft, newTop, newWidth, newHeight);
        }

        private void ResizeBottom(int x, int y)
        {
            Rectangle oldBounds = this.Bounds;
            int newTop = oldBounds.Top;
            int newLeft = oldBounds.Left;
            int newWidth = oldBounds.Width;
            int newHeight = y - oldBounds.Top;

            if (newHeight < this.MinimumSize.Height)
            {
                newHeight = this.MinimumSize.Height;
            }
            this.Bounds = new Rectangle(newLeft, newTop, newWidth, newHeight);
        }

        #endregion

        public void HiddenProp(string type_prop, bool flag)
        {
            PropertyDescriptor descriptor =
            TypeDescriptor.GetProperties(this.GetType())[type_prop];
            BrowsableAttribute attrib =
              (BrowsableAttribute)descriptor.Attributes[typeof(BrowsableAttribute)];
            FieldInfo isBrow =
              attrib.GetType().GetField("browsable", BindingFlags.NonPublic | BindingFlags.Instance);
            isBrow.SetValue(attrib, flag);
        }

        /// <summary>
        /// Gets the HitStatus belonging to the specified location. Used to determine if the Shape should be resized, and in which direction, or moved.
        /// </summary>
        /// <param name="location">The mouse location.</param>
        /// <returns>The HitStatus belonging to the specified location.</returns>
        public HitStatus GetHitTest(Point location)
        {
            
            string type_f = GetShapeTypeName();
            if (this.GrabHandles.TotalBounds.Contains(location) && type_f != "Pipeline" && type_f != "Manometr" && type_f != "ObrPipeline" && type_f != "TK" && type_f !="Text" && type_f != "TriangleShape" && type_f != "CircleShape" && type_f != "Compensator" && type_f != "DistanseDiametr" && type_f != "Vodoprovod")
            {
                
                // Diagonal resizing (has precedence over normal resizing)
                if (this.GrabHandles.TopLeft.Contains(location))
                    return HitStatus.ResizeTopLeft;
                else if (this.GrabHandles.TopRight.Contains(location))
                    return HitStatus.ResizeTopRight;
                else if (this.GrabHandles.BottomLeft.Contains(location))
                    return HitStatus.ResizeBottomLeft;
                else if (this.GrabHandles.BottomRight.Contains(location))
                    return HitStatus.ResizeBottomRight;

                // Horizontal/Vertical resizing (has precedence over dragging)
                if (Rectangle.Union(this.GrabHandles.TopLeft, this.GrabHandles.TopRight).Contains(location))
                    return HitStatus.ResizeTop;
                else if (Rectangle.Union(this.GrabHandles.TopRight, this.GrabHandles.BottomRight).Contains(location))
                    return HitStatus.ResizeRight;
                else if (Rectangle.Union(this.GrabHandles.BottomRight, this.GrabHandles.BottomLeft).Contains(location))
                    return HitStatus.ResizeBottom;
                else if (Rectangle.Union(this.GrabHandles.BottomLeft, this.GrabHandles.TopLeft).Contains(location))
                    return HitStatus.ResizeLeft;

                // If all else fails: drag
                return HitStatus.Drag;
            }
            else if(this.GrabHandles.TotalBounds.Contains(location) && type_f == "Pipeline")
            {
                // Diagonal resizing (has precedence over normal resizing)
                if (this.GrabHandles.TopLeft.Contains(location))
                    return HitStatus.ResizeTopLeft;               
                else if (this.GrabHandles.BottomRight.Contains(location))
                    return HitStatus.ResizeBottomRight;               

                // If all else fails: drag
                return HitStatus.Drag;
            }
            else if (this.GrabHandles.TotalBounds.Contains(location) && type_f == "ObrPipeline")
            {
                // Diagonal resizing (has precedence over normal resizing)
                if (this.GrabHandles.TopLeft.Contains(location))
                    return HitStatus.ResizeTopLeft;
                else if (this.GrabHandles.BottomRight.Contains(location))
                    return HitStatus.ResizeBottomRight;

                // If all else fails: drag
                return HitStatus.Drag;
            }
            else if (this.GrabHandles.TotalBounds.Contains(location) && type_f == "Vodoprovod")
            {
                // Diagonal resizing (has precedence over normal resizing)
                if (this.GrabHandles.TopLeft.Contains(location))
                    return HitStatus.ResizeTopLeft;
                else if (this.GrabHandles.BottomRight.Contains(location))
                    return HitStatus.ResizeBottomRight;

                // If all else fails: drag
                return HitStatus.Drag;
            }
            else if (this.GrabHandles.TotalBounds.Contains(location) && type_f == "Text")
            {
                //MessageBox.Show(this.FontField.Height.ToString() + "  " + this.FontField.Size.ToString());
                return HitStatus.Drag;
            }
            else if (this.GrabHandles.TotalBounds.Contains(location) && type_f == "TriangleShape")
            {
                //MessageBox.Show(this.FontField.Height.ToString() + "  " + this.FontField.Size.ToString());
                return HitStatus.Drag;
            }
            else if (this.GrabHandles.TotalBounds.Contains(location) && type_f == "CircleShape")
            {
                //MessageBox.Show(this.FontField.Height.ToString() + "  " + this.FontField.Size.ToString());
                return HitStatus.Drag;
            }
            else if (this.GrabHandles.TotalBounds.Contains(location) && type_f == "Manometr")
            {
                //MessageBox.Show(this.FontField.Height.ToString() + "  " + this.FontField.Size.ToString());
                return HitStatus.Drag;
            }
            else if (this.GrabHandles.TotalBounds.Contains(location) && type_f == "DistanseDiametr")
            {
                return HitStatus.Drag;
            }
            else if (this.GrabHandles.TotalBounds.Contains(location) && type_f == "TK")
            {
                return HitStatus.Drag;
            }
            else if (this.GrabHandles.TotalBounds.Contains(location) && type_f == "Compensator")
            {

                return HitStatus.Drag;
            }
            else
            {
                return HitStatus.None;
            }
        }
        public virtual Shape GetShape(Shape s)
        {
            return s;
        }

        public virtual string GetShapeTypeName()
        {
            return this.GetType().Name;
        }

            #endregion

        }
}
