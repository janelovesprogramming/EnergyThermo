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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.наПереднийПланToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.наЗаднийПланToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.canvas1 = new ShapeEditorLibrary.Canvas();
            this.shapesComboBox1 = new ShapeEditorLibrary.ShapesComboBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRectangleButton,
            this.addEllipseButton,
            this.addTriangleButton,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(978, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // addRectangleButton
            // 
            this.addRectangleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.addRectangleButton.Image = ((System.Drawing.Image)(resources.GetObject("addRectangleButton.Image")));
            this.addRectangleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addRectangleButton.Name = "addRectangleButton";
            this.addRectangleButton.Size = new System.Drawing.Size(88, 22);
            this.addRectangleButton.Text = "Add Rectangle";
            this.addRectangleButton.Click += new System.EventHandler(this.addRectangleButton_Click);
            // 
            // addEllipseButton
            // 
            this.addEllipseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.addEllipseButton.Image = ((System.Drawing.Image)(resources.GetObject("addEllipseButton.Image")));
            this.addEllipseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addEllipseButton.Name = "addEllipseButton";
            this.addEllipseButton.Size = new System.Drawing.Size(69, 22);
            this.addEllipseButton.Text = "Add Ellipse";
            this.addEllipseButton.Click += new System.EventHandler(this.addEllipseButton_Click);
            // 
            // addTriangleButton
            // 
            this.addTriangleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.addTriangleButton.Image = ((System.Drawing.Image)(resources.GetObject("addTriangleButton.Image")));
            this.addTriangleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addTriangleButton.Name = "addTriangleButton";
            this.addTriangleButton.Size = new System.Drawing.Size(79, 22);
            this.addTriangleButton.Text = "Add Triangle";
            this.addTriangleButton.Click += new System.EventHandler(this.addTriangleButton_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
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
            this.splitContainer1.Size = new System.Drawing.Size(978, 546);
            this.splitContainer1.SplitterDistance = 732;
            this.splitContainer1.TabIndex = 1;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 21);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(242, 525);
            this.propertyGrid1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.удалитьToolStripMenuItem,
            this.наПереднийПланToolStripMenuItem,
            this.наЗаднийПланToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(176, 92);
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
            // canvas1
            // 
            this.canvas1.BorderSnapDistance = 25;
            this.canvas1.ContextMenuStrip = this.contextMenuStrip1;
            this.canvas1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas1.Location = new System.Drawing.Point(0, 0);
            this.canvas1.Name = "canvas1";
            this.canvas1.ShapeAlignDistance = 15;
            this.canvas1.ShapeSnapDistance = 15;
            this.canvas1.Size = new System.Drawing.Size(732, 546);
            this.canvas1.SnapMode = ShapeEditorLibrary.Canvas.SnapModes.SnapLines;
            this.canvas1.TabIndex = 0;
            this.canvas1.SelectedShapeChanged += new System.EventHandler(this.canvas1_SelectedShapeChanged);
            this.canvas1.ShapesCollectionChanged += new System.EventHandler(this.canvas1_ShapesCollectionChanged);
            this.canvas1.DoubleClick += new System.EventHandler(this.canvas1_DoubleClick);
            // 
            // shapesComboBox1
            // 
            this.shapesComboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.shapesComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.shapesComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shapesComboBox1.FormattingEnabled = true;
            this.shapesComboBox1.Location = new System.Drawing.Point(0, 0);
            this.shapesComboBox1.Name = "shapesComboBox1";
            this.shapesComboBox1.Size = new System.Drawing.Size(242, 21);
            this.shapesComboBox1.TabIndex = 1;
            this.shapesComboBox1.SelectedIndexChanged += new System.EventHandler(this.shapesComboBox1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 571);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
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
    }
}

