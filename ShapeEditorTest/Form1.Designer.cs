namespace ShapeEditorTest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addRectangleButton = new System.Windows.Forms.ToolStripButton();
            this.addEllipseButton = new System.Windows.Forms.ToolStripButton();
            this.addTriangleButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.наПереднийПланToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.наЗаднийПланToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.свойстваToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.масштабToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сеткаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.canvas1 = new ShapeEditorLibrary.Canvas();
            this.pic = new System.Windows.Forms.PictureBox();
            this.shapesComboBox1 = new ShapeEditorLibrary.ShapesComboBox();
            this.загрузитьИхXmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.canvas1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRectangleButton,
            this.addEllipseButton,
            this.addTriangleButton,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(166, 547);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // addRectangleButton
            // 
            this.addRectangleButton.Image = ((System.Drawing.Image)(resources.GetObject("addRectangleButton.Image")));
            this.addRectangleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addRectangleButton.Name = "addRectangleButton";
            this.addRectangleButton.Size = new System.Drawing.Size(163, 20);
            this.addRectangleButton.Text = "Объекты";
            this.addRectangleButton.Click += new System.EventHandler(this.addRectangleButton_Click);
            // 
            // addEllipseButton
            // 
            this.addEllipseButton.Image = ((System.Drawing.Image)(resources.GetObject("addEllipseButton.Image")));
            this.addEllipseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addEllipseButton.Name = "addEllipseButton";
            this.addEllipseButton.Size = new System.Drawing.Size(163, 20);
            this.addEllipseButton.Text = "Тепловая камера";
            this.addEllipseButton.Click += new System.EventHandler(this.addEllipseButton_Click);
            // 
            // addTriangleButton
            // 
            this.addTriangleButton.Image = ((System.Drawing.Image)(resources.GetObject("addTriangleButton.Image")));
            this.addTriangleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addTriangleButton.Name = "addTriangleButton";
            this.addTriangleButton.Size = new System.Drawing.Size(163, 20);
            this.addTriangleButton.Text = "Компенсатор";
            this.addTriangleButton.Click += new System.EventHandler(this.addTriangleButton_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(163, 20);
            this.toolStripButton1.Text = "Подающий трубопровод";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(163, 20);
            this.toolStripButton2.Text = "Текст";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(163, 20);
            this.toolStripButton3.Text = "Тройник";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(166, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.canvas1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainer1.Panel2.Controls.Add(this.shapesComboBox1);
            this.splitContainer1.Size = new System.Drawing.Size(812, 547);
            this.splitContainer1.SplitterDistance = 606;
            this.splitContainer1.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.удалитьToolStripMenuItem,
            this.наПереднийПланToolStripMenuItem,
            this.наЗаднийПланToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(176, 70);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // наПереднийПланToolStripMenuItem
            // 
            this.наПереднийПланToolStripMenuItem.Name = "наПереднийПланToolStripMenuItem";
            this.наПереднийПланToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.наПереднийПланToolStripMenuItem.Text = "На передний план";
            this.наПереднийПланToolStripMenuItem.Click += new System.EventHandler(this.наПереднийПланToolStripMenuItem_Click);
            // 
            // наЗаднийПланToolStripMenuItem
            // 
            this.наЗаднийПланToolStripMenuItem.Name = "наЗаднийПланToolStripMenuItem";
            this.наЗаднийПланToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.наЗаднийПланToolStripMenuItem.Text = "На задний план";
            this.наЗаднийПланToolStripMenuItem.Click += new System.EventHandler(this.наЗаднийПланToolStripMenuItem_Click);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 21);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(202, 526);
            this.propertyGrid1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.свойстваToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(978, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьToolStripMenuItem,
            this.сохранитьКакToolStripMenuItem,
            this.загрузитьИхXmlToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // сохранитьКакToolStripMenuItem
            // 
            this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.сохранитьКакToolStripMenuItem.Text = "Сохранить как";
            this.сохранитьКакToolStripMenuItem.Click += new System.EventHandler(this.сохранитьКакToolStripMenuItem_Click);
            // 
            // свойстваToolStripMenuItem
            // 
            this.свойстваToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.масштабToolStripMenuItem,
            this.сеткаToolStripMenuItem});
            this.свойстваToolStripMenuItem.Name = "свойстваToolStripMenuItem";
            this.свойстваToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.свойстваToolStripMenuItem.Text = "Свойства";
            // 
            // масштабToolStripMenuItem
            // 
            this.масштабToolStripMenuItem.Name = "масштабToolStripMenuItem";
            this.масштабToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.масштабToolStripMenuItem.Text = "Масштаб";
            // 
            // сеткаToolStripMenuItem
            // 
            this.сеткаToolStripMenuItem.Name = "сеткаToolStripMenuItem";
            this.сеткаToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.сеткаToolStripMenuItem.Text = "Сетка";
            this.сеткаToolStripMenuItem.Click += new System.EventHandler(this.сеткаToolStripMenuItem_Click);
            // 
            // canvas1
            // 
            this.canvas1.BackColor = System.Drawing.Color.White;
            this.canvas1.BorderSnapDistance = 25;
            this.canvas1.ContextMenuStrip = this.contextMenuStrip1;
            this.canvas1.Controls.Add(this.pic);
            this.canvas1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas1.Location = new System.Drawing.Point(0, 0);
            this.canvas1.Name = "canvas1";
            this.canvas1.ShapeAlignDistance = 15;
            this.canvas1.ShapeSnapDistance = 15;
            this.canvas1.Size = new System.Drawing.Size(606, 547);
            this.canvas1.SnapMode = ShapeEditorLibrary.Canvas.SnapModes.SnapLines;
            this.canvas1.TabIndex = 0;
            this.canvas1.SelectedShapeChanged += new System.EventHandler(this.canvas1_SelectedShapeChanged);
            this.canvas1.ShapesCollectionChanged += new System.EventHandler(this.canvas1_ShapesCollectionChanged);
            this.canvas1.DoubleClick += new System.EventHandler(this.canvas1_DoubleClick);
            this.canvas1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.canvas1_MouseClick);
            // 
            // pic
            // 
            this.pic.Location = new System.Drawing.Point(269, 83);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(100, 50);
            this.pic.TabIndex = 0;
            this.pic.TabStop = false;
            this.pic.Visible = false;
            // 
            // shapesComboBox1
            // 
            this.shapesComboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.shapesComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.shapesComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shapesComboBox1.FormattingEnabled = true;
            this.shapesComboBox1.Location = new System.Drawing.Point(0, 0);
            this.shapesComboBox1.Name = "shapesComboBox1";
            this.shapesComboBox1.Size = new System.Drawing.Size(202, 21);
            this.shapesComboBox1.TabIndex = 1;
            this.shapesComboBox1.SelectedIndexChanged += new System.EventHandler(this.shapesComboBox1_SelectedIndexChanged);
            // 
            // загрузитьИхXmlToolStripMenuItem
            // 
            this.загрузитьИхXmlToolStripMenuItem.Name = "загрузитьИхXmlToolStripMenuItem";
            this.загрузитьИхXmlToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.загрузитьИхXmlToolStripMenuItem.Text = "Загрузить их xml";
            this.загрузитьИхXmlToolStripMenuItem.Click += new System.EventHandler(this.загрузитьИхXmlToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 571);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "EnergyThermo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.canvas1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton addRectangleButton;
        private System.Windows.Forms.ToolStripButton addEllipseButton;
        private System.Windows.Forms.ToolStripButton addTriangleButton;
        private ShapeEditorLibrary.Canvas canvas1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private ShapeEditorLibrary.ShapesComboBox shapesComboBox1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem наПереднийПланToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem наЗаднийПланToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem свойстваToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem масштабToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сеткаToolStripMenuItem;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.ToolStripMenuItem загрузитьИхXmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

