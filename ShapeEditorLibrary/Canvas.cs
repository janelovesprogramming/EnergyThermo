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
    public class Canvas : Panel
    {
        public Canvas()
        {
            _SelectedShapes = new List<Shape>();
            this.SetShapes(new ShapeCollection(this));
            base.DoubleBuffered = true;

            this.ShapeAlignDistance = 15;
            this.BorderSnapDistance = 25;
            this.ShapeSnapDistance = 15;

            this.SnapMode = SnapModes.SnapLines;
        }

        private void SetShapes(ShapeCollection shapes)
        {
            if (_Shapes != null) _Shapes.CollectionChanged -= CollectionChanged;
            _Shapes = shapes;
            _Shapes.CollectionChanged += CollectionChanged;
            this.Invalidate();
        }

        #region Events
        
        /// <summary>
        /// Raised when the selected Shape changes.
        /// </summary>
        [Description("Raised when the selected Shape changes.")]
        public event EventHandler SelectedShapeChanged;

        /// <summary>
        /// Raised when the collection of Shapes changes.
        /// </summary>
        [Description("Raised when the collection of Shapes changes.")]
        public event EventHandler ShapesCollectionChanged;

        protected virtual void OnSelectedShapeChanged(EventArgs e)
        {
            if (this.SelectedShapeChanged != null) this.SelectedShapeChanged(this, e);
        }

        private void CollectionChanged(object sender, EventArgs e)
        {
            this.OnShapesCollectionChanged(e);
        }

        protected virtual void OnShapesCollectionChanged(EventArgs e)
        {
            if (this.ShapesCollectionChanged != null) this.ShapesCollectionChanged(this, e);
        }

#endregion

        #region Private Fields

        private Point moveStart;
        private Shape.HitStatus hitStatus = Shape.HitStatus.None;
        private bool mouseDown;
        private List<SnapLine> snapLines;
        private bool isSnapping;
        private List<Shape> clickedShapes;
        private Point lastClick;
        private int clickedShapeIterator;

        #endregion

        #region Enums

        public enum SnapModes
        {
            None,
            SnapLines,
            SnapToGrid
        }

        #endregion

        #region Properties

        private ShapeCollection _Shapes;
        /// <summary>
        /// The collection of Shapes in this Canvas.
        /// </summary>
        public ShapeCollection Shapes
        {
            get { return _Shapes; }
        }

        //private Shape _SelectedShape;
        /// <summary>
        /// The currently selected Shape.
        /// </summary>
        [Browsable(false)]
        public Shape SelectedShape
        {
            get
            {
                if (this.SelectedShapes != null && this.SelectedShapes.Count > 0) return this.SelectedShapes[0];
                return null;
            }
        }

        private List<Shape> _SelectedShapes;
        public List<Shape> SelectedShapes
        {
            get { return _SelectedShapes; }
        }

        public int ShapeAlignDistance { get; set; }
        public int BorderSnapDistance { get; set; }
        public int ShapeSnapDistance { get; set; }
        public SnapModes SnapMode { get; set; }

        #endregion

        #region Methods 
               
        public void AddRemoveSelection(Shape shape)
        {
            if (shape == null) return;

            if (this.SelectedShapes == null) _SelectedShapes = new List<Shape>();
            if (this.SelectedShapes.Contains(shape))
                this.SelectedShapes.Remove(shape);
            else
                this.SelectedShapes.Insert(0, shape);
            //MessageBox.Show(shape.ToString());
            this.OnSelectedShapeChanged(EventArgs.Empty);
            this.Invalidate();
        }

      /*  public void RemoveShape(Shape shape)
        {
            if (shape == null) return;

            if (this.SelectedShapes == null) _SelectedShapes = new List<Shape>();
            if (this.SelectedShapes.Contains(shape))
                this.SelectedShapes.Remove(shape);
            else
                this.SelectedShapes.Insert(0, shape);
            //MessageBox.Show(shape.ToString());
            this.OnSelectedShapeChanged(EventArgs.Empty);
            this.Invalidate();
        }
        */
        public void SetSelection(Shape shape)
        {
            if (shape == null)
            {
                _SelectedShapes = new List<Shape>();
                this.OnSelectedShapeChanged(EventArgs.Empty);
                
                return;
            }
            if (this.SelectedShapes == null) _SelectedShapes = new List<Shape>();
            if (this.SelectedShapes.Contains(shape))
            {
                this.SelectedShapes.Remove(shape);
                this.SelectedShapes.Insert(0, shape);
               
            }
            else
            {
                _SelectedShapes = new List<Shape>();
                this.SelectedShapes.Add(shape);
            }

            this.OnSelectedShapeChanged(EventArgs.Empty);
            this.Invalidate();
        }

        /// <summary>
        /// Brings the specified Shape to the front of the Canvas, above all other Shapes.
        /// </summary>
        /// <param name="s">The Shape to bring to the front.</param>
        public void BringToFront(Shape s)
        {
            if (this.Shapes.Contains(s))
            {
                this.Shapes.Remove(s);
                this.Shapes.Add(s);
                this.Invalidate();
                
            }
        }
        public void RemoveShape(Shape s)
        {
            if (this.Shapes.Contains(s))
            {
                this.Shapes.Remove(s);
                this.Invalidate();
            }
        }
        /// <summary>
        /// Sends the specified Shape to the back of the Canvas, beneath all other Shapes.
        /// </summary>
        /// <param name="s">The Shape to send to the back.</param>
        public void SendToBack(Shape s)
        {
            if (this.Shapes.Contains(s))
            {
                this.Shapes.Remove(s);
                this.Shapes.Insert(0, s);
                this.Invalidate();
            } 
        }

        /// <summary>
        /// Notifies the Canvas that the specified Rectangle should be re-painted.
        /// </summary>
        /// <param name="rect">The Rectangle that should be re-painted.</param>
        public void InvalidateRectangle(Rectangle rect)
        {
            rect.Inflate(1, 1);
            this.Invalidate(rect);
        }

        /// <summary>
        /// Notifies the Canvas that the specified Shape should be re-painted.
        /// </summary>
        /// <param name="s">The Shape that should be re-painted.</param>
        public void InvalidateShape(Shape s)
        {
            this.InvalidateRectangle(s.Bounds);
        }

        /// <summary>
        /// Notifies the Canvas that the specified Shapes should be re-painted.
        /// </summary>
        /// <param name="shapes">The Shapes that should be re-painted.</param>
        public void InvalidateShapes(IEnumerable<Shape> shapes)
        {
            foreach (Shape s in shapes) this.InvalidateShape(s);
            this.Update();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.SnapMode == SnapModes.SnapToGrid)
            {
                this.PaintGrid(e.Graphics);
            }
            else if (this.SnapMode == SnapModes.SnapLines)
            {
                if (snapLines != null && snapLines.Count > 0)
                {
                    foreach (var snapLine in snapLines)
                        snapLine.Draw(e.Graphics);
                }
            }

            foreach (var s in this.Shapes)
            {
                s.Draw(e.Graphics);
                if(s.GetShapeTypeName() == "TK" || s.GetShapeTypeName() == "Object" || s.GetShapeTypeName() == "Pipeline")
                    s.DrawText(e.Graphics);
                if(flag)
                    s.DrawText(e.Graphics);

            }
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            foreach (var s in this.SelectedShapes)
            {
                s.DrawGrabHandles(e.Graphics, this.SelectedShapes.IndexOf(s) == 0);
            }
        }

        public void PaintGrid(Graphics g)
        {
            const int gridSize = 10;
            for (var x = 0; x < this.Width; x += gridSize)
            {
                for (var y = 0; y < this.Height; y += gridSize)
                {
                    g.DrawLine(Pens.LightGray, 0, y, this.Width, y);
                    g.DrawLine(Pens.LightGray, x, 0, x, this.Height);
                }
            }
        }

        public void PaintPoint(Graphics g)
        {
            const int gridSize = 20;
            Pen pen = new Pen(Brushes.Gray);
            
            for (var x = 0; x < this.Width; x += gridSize)
            {
                for (var y = 0; y < this.Height; y += gridSize)
                {
                    g.DrawLine(pen, 0, y, this.Width, y);
                    g.DrawLine(pen, x, 0, x, this.Height);
                }
            }
        }
        #region SnapLines

        private void SnapShape(Shape movingShape)
        {
            if (isSnapping) return;

            snapLines = new List<SnapLine>();
            foreach (var fixedShape in this.Shapes.Where(s => s != movingShape && !this.SelectedShapes.Contains(s)))
            {
                this.AlignShapes(movingShape, fixedShape);
                this.SnapShapes(movingShape, fixedShape);
            }
            this.SnapCanvas(movingShape);
            this.Invalidate();
        }

        private void AlignShapes(Shape movingShape, Shape fixedShape)
        {
            isSnapping = true;
            if (this.ShouldAlignShape(movingShape.Bounds.Top, fixedShape.Bounds.Top))
            {
                this.AlignTop(fixedShape, movingShape);
            }
            if (this.ShouldAlignShape(movingShape.Bounds.Bottom, fixedShape.Bounds.Bottom))
            {
                this.AlignBottom(fixedShape, movingShape);
            }
            if (this.ShouldAlignShape(movingShape.Bounds.Left, fixedShape.Bounds.Left))
            {
                this.AlignLeft(fixedShape, movingShape);
            }
            if (this.ShouldAlignShape(movingShape.Bounds.Right, fixedShape.Bounds.Right))
            {
                this.AlignRight(fixedShape, movingShape);
            }
            isSnapping = false;
        }

        private void SnapShapes(Shape movingShape, Shape fixedShape)
        {
            isSnapping = true;
            if (this.ShouldSnapShape(movingShape.Bounds.Left, movingShape.Bounds.Width, fixedShape.Bounds.Left, fixedShape.Bounds.Width))
            {
                if (movingShape.Bounds.Bottom < fixedShape.Bounds.Top)
                {
                    if (movingShape.Bounds.Bottom.IsBetween(fixedShape.Bounds.Top - this.ShapeSnapDistance, fixedShape.Bounds.Top + this.ShapeSnapDistance))
                        this.SnapTop(fixedShape, movingShape);
                }
                else if (movingShape.Bounds.Top > fixedShape.Bounds.Bottom)
                {
                    if (movingShape.Bounds.Top.IsBetween(fixedShape.Bounds.Bottom - this.ShapeSnapDistance, fixedShape.Bounds.Bottom + this.ShapeSnapDistance))
                        this.SnapBottom(fixedShape, movingShape);
                }
            }
            if (this.ShouldSnapShape(movingShape.Bounds.Top, movingShape.Bounds.Height, fixedShape.Bounds.Top, fixedShape.Bounds.Height))
            {
                if (movingShape.Bounds.Right < fixedShape.Bounds.Left)
                {
                    if (movingShape.Bounds.Right.IsBetween(fixedShape.Bounds.Left - this.ShapeSnapDistance, fixedShape.Bounds.Left + this.ShapeSnapDistance))
                        this.SnapLeft(fixedShape, movingShape);
                }
                else if (movingShape.Bounds.Left > fixedShape.Bounds.Right)
                {
                    if (movingShape.Bounds.Left.IsBetween(fixedShape.Bounds.Right - this.ShapeSnapDistance, fixedShape.Bounds.Right + this.ShapeSnapDistance))
                        this.SnapRight(fixedShape, movingShape);
                }
            }
            isSnapping = false;
        }

        private void SnapCanvas(Shape shape)
        {
            isSnapping = true;
            if (this.ShouldSnapCanvas(shape.Bounds.Top, 0))
            {
                this.SnapCanvasTop(shape);
            }
            if (this.ShouldSnapCanvas(shape.Bounds.Bottom, this.Height))
            {
                this.SnapCanvasBottom(shape);
            }
            if (this.ShouldSnapCanvas(shape.Bounds.Left, 0))
            {
                this.SnapCanvasLeft(shape);
            }
            if (this.ShouldSnapCanvas(shape.Bounds.Right, this.Width))
            {
                this.SnapCanvasRight(shape);
            }
            isSnapping = false;
        }

        private void AlignLeft(Shape fixedShape, Shape movingShape)
        {
            movingShape.Move(new Point(fixedShape.Bounds.Left, movingShape.Bounds.Y));
            SnapLine snapLine;
            if (movingShape.Bounds.Y > fixedShape.Bounds.Y)
            {
                snapLine = new SnapLine(movingShape.Bounds.Left, fixedShape.Bounds.Top, movingShape.Bounds.Left,
                                        movingShape.Bounds.Bottom, Color.Blue);
            }
            else
            {
                snapLine = new SnapLine(movingShape.Bounds.Left, movingShape.Bounds.Top, movingShape.Bounds.Left,
                                        fixedShape.Bounds.Bottom, Color.Blue);
            }
            snapLines.Add(snapLine);
        }

        private void AlignRight(Shape fixedShape, Shape movingShape)
        {
            movingShape.Move(new Point(fixedShape.Bounds.Right - movingShape.Bounds.Width, movingShape.Bounds.Y));
            SnapLine snapLine;
            if (movingShape.Bounds.Y > fixedShape.Bounds.Y)
            {
                snapLine = new SnapLine(movingShape.Bounds.Right, fixedShape.Bounds.Top, movingShape.Bounds.Right,
                                        movingShape.Bounds.Bottom, Color.Blue);
            }
            else
            {
                snapLine = new SnapLine(movingShape.Bounds.Right, movingShape.Bounds.Top, movingShape.Bounds.Right,
                                        fixedShape.Bounds.Bottom, Color.Blue);
            }
            snapLines.Add(snapLine);
        }

        private void AlignTop(Shape fixedShape, Shape movingShape)
        {
            movingShape.Move(new Point(movingShape.Bounds.X, fixedShape.Bounds.Top));
            SnapLine snapLine;
            if (movingShape.Bounds.X > fixedShape.Bounds.X)
            {
                snapLine = new SnapLine(fixedShape.Bounds.Left, movingShape.Bounds.Top, movingShape.Bounds.Right,
                                        movingShape.Bounds.Top, Color.Blue);
            }
            else
            {
                snapLine = new SnapLine(movingShape.Bounds.Left, movingShape.Bounds.Top, fixedShape.Bounds.Right,
                                        movingShape.Bounds.Top, Color.Blue);
            }
            snapLines.Add(snapLine);
        }

        private void AlignBottom(Shape fixedShape, Shape movingShape)
        {
            movingShape.Move(new Point(movingShape.Location.X, fixedShape.Bounds.Bottom - movingShape.Bounds.Height));
            SnapLine snapLine;
            if (movingShape.Bounds.X > fixedShape.Bounds.X)
            {
                snapLine = new SnapLine(fixedShape.Bounds.Left, movingShape.Bounds.Bottom, movingShape.Bounds.Right,
                                        movingShape.Bounds.Bottom, Color.Blue);
            }
            else
            {
                snapLine = new SnapLine(movingShape.Bounds.Left, movingShape.Bounds.Bottom, fixedShape.Bounds.Right,
                                        movingShape.Bounds.Bottom, Color.Blue);
            }
            snapLines.Add(snapLine);
        }

        private void SnapTop(Shape fixedShape, Shape movingShape)
        {
            movingShape.Move(new Point(movingShape.Location.X,
                                       fixedShape.Bounds.Top - movingShape.Bounds.Height - this.ShapeSnapDistance));
            int x;
            if (movingShape.Location.X < fixedShape.Location.X)
                x = (fixedShape.Bounds.Left + movingShape.Bounds.Right)/2;
            else
                x = (movingShape.Bounds.Left + fixedShape.Bounds.Right)/2;
            var snapLine = new SnapLine(x, movingShape.Bounds.Bottom, x, fixedShape.Bounds.Top, Color.Purple);
            snapLines.Add(snapLine);
        }

        private void SnapBottom(Shape fixedShape, Shape movingShape)
        {
            movingShape.Move(new Point(movingShape.Location.X,
                                       fixedShape.Bounds.Bottom + this.ShapeSnapDistance));
            int x;
            if (movingShape.Location.X < fixedShape.Location.X)
                x = (fixedShape.Bounds.Left + movingShape.Bounds.Right) / 2;
            else
                x = (movingShape.Bounds.Left + fixedShape.Bounds.Right) / 2;
            var snapLine = new SnapLine(x, movingShape.Bounds.Top, x, fixedShape.Bounds.Bottom, Color.Purple);
            snapLines.Add(snapLine);
        }

        private void SnapLeft(Shape fixedShape, Shape movingShape)
        {
            movingShape.Move(new Point(fixedShape.Bounds.Left - movingShape.Bounds.Width - this.ShapeSnapDistance,
                                       movingShape.Location.Y));
            int y;
            if (movingShape.Location.Y < fixedShape.Location.Y)
                y = (movingShape.Bounds.Bottom + fixedShape.Bounds.Top)/2;
            else
                y = (fixedShape.Bounds.Bottom + movingShape.Bounds.Top) / 2;
            var snapLine = new SnapLine(movingShape.Bounds.Right, y, fixedShape.Bounds.Left, y, Color.Purple);
            snapLines.Add(snapLine);
        }

        private void SnapRight(Shape fixedShape, Shape movingShape)
        {
            movingShape.Move(new Point(fixedShape.Bounds.Right + this.ShapeSnapDistance,
                                       movingShape.Location.Y));
            int y;
            if (movingShape.Location.Y < fixedShape.Location.Y)
                y = (movingShape.Bounds.Bottom + fixedShape.Bounds.Top) / 2;
            else
                y = (fixedShape.Bounds.Bottom + movingShape.Bounds.Top) / 2;
            var snapLine = new SnapLine(movingShape.Bounds.Left, y, fixedShape.Bounds.Right, y, Color.Purple);
            snapLines.Add(snapLine);
        }
        
        private void SnapCanvasTop(Shape shape)
        {
            shape.Move(new Point(shape.Location.X, this.BorderSnapDistance));
            var center = shape.Location.X + (shape.Size.Width/2);
            var snapLine = new SnapLine(center, 0, center, shape.Bounds.Top, Color.Purple);
            snapLines.Add(snapLine);
        }

        private void SnapCanvasBottom(Shape shape)
        {
            shape.Move(new Point(shape.Location.X, this.Height - this.BorderSnapDistance - shape.Size.Height));
            var center = shape.Location.X + (shape.Size.Width / 2);
            var snapLine = new SnapLine(center, shape.Bounds.Bottom, center, this.Height, Color.Purple);
            snapLines.Add(snapLine);
        }

        private void SnapCanvasLeft(Shape shape)
        {
            shape.Move(new Point(this.BorderSnapDistance, shape.Location.Y));
            var center = shape.Location.Y + (shape.Size.Height/2);
            var snapLine = new SnapLine(0, center, shape.Bounds.Left, center, Color.Purple);
            snapLines.Add(snapLine);
        }

        private void SnapCanvasRight(Shape shape)
        {
            shape.Move(new Point(this.Width - this.BorderSnapDistance - shape.Size.Width, shape.Location.Y));
            var center = shape.Location.Y + (shape.Size.Height / 2);
            var snapLine = new SnapLine(shape.Bounds.Right, center, this.Width, center, Color.Purple);
            snapLines.Add(snapLine);
        }

        #endregion

        #region Shape Events

        internal void RegisterShapeEvents(Shape s)
        {
            s.LocationChanged += Shape_LocationChanged;
            s.SizeChanged += Shape_SizeChanged;
        }

        internal void UnregisterShapeEvents(Shape s)
        {
            s.LocationChanged -= Shape_LocationChanged;
            s.SizeChanged -= Shape_SizeChanged;
        }

        internal void Shape_LocationChanged(object sender, EventArgs e)
        {
            var shape = (Shape) sender;
            if (this.SnapMode == SnapModes.SnapLines && this.SelectedShapes.IndexOf(shape) == 0)
                this.SnapShape(shape);
        }

        internal void Shape_SizeChanged(object sender, EventArgs e)
        {
            var shape = (Shape) sender;
            if (this.SnapMode == SnapModes.SnapLines && this.SelectedShapes.IndexOf(shape) == 0)
                this.SnapShape(shape);
        }

        #endregion

      
        #region Mouse Events

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == System.Windows.Forms.MouseButtons.Left
                || e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                mouseDown = true;


                if (Control.ModifierKeys == Keys.Control)
                {
                    var clickedShape = this.GetShapeAtPoint(e.Location);
                    this.AddRemoveSelection(clickedShape);
                }
                else
                {
                    this.HandleClick(e.Location);
                    //this.SetSelection(clickedShape);
                }

                if (this.SelectedShape != null)
                    moveStart = new Point(e.Location.X - this.SelectedShape.Location.X,
                                          e.Location.Y - this.SelectedShape.Location.Y);

                this.DoHitTest(e.Location);
                this.Invalidate();

                if (e.Button == System.Windows.Forms.MouseButtons.Right
                    && this.SelectedShape != null
                    && this.SelectedShape.ContextMenuStrip != null)
                {
                    this.SelectedShape.ContextMenuStrip.Show(this.PointToScreen(e.Location));
                }

                lastClick = e.Location;
            }
        }

        private void HandleClick(Point p)
        {
            if (p == lastClick)
            {
                if (clickedShapes.Count > 0)
                {
                    this.SetSelection(clickedShapes[clickedShapeIterator]);
                    clickedShapeIterator = (clickedShapeIterator + 1) % clickedShapes.Count;
                }
            }
            else
            {
                clickedShapes = this.GetShapesAtPoint(p);
                clickedShapeIterator = 0;
                this.SetSelection(clickedShapes.Count > 0 ? clickedShapes[0] : null);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            mouseDown = false;
            if (snapLines != null) snapLines.Clear();
            this.Invalidate();
        }
        bool flag;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            flag = false;
            base.OnMouseMove(e);
            if (mouseDown)
            {
                var oldBounds = new List<Rectangle>();
                if (this.SelectedShape != null)
                {
                    if (hitStatus == Shape.HitStatus.Drag)
                    {
                        // 1. Store the relative locations of all other selected shapes, if any
                        var relativeLocations = new List<Point>();
                        for (var i = 1; i < this.SelectedShapes.Count; i++)
                        {
                            var shape = this.SelectedShapes[i];
                            relativeLocations.Add(this.SelectedShape.Location.Subtract(shape.Location));
                            oldBounds.Add(shape.GrabHandles.TotalBounds);
                        }

                        // 2. Move the main selected shape
                        oldBounds.Add(this.SelectedShape.GrabHandles.TotalBounds);
                        var newLocation = e.Location.Subtract(moveStart);
                        if (this.SnapMode == SnapModes.SnapToGrid) newLocation = newLocation.Floor(10);
                        this.SelectedShape.Move(newLocation);
                        this.InvalidateShape(this.SelectedShape);

                        // 3. Move the remaining selected shapes back to their relative positions
                        for (var i = 1; i < this.SelectedShapes.Count; i++)
                        {
                            var shape = this.SelectedShapes[i];
                            oldBounds.Add(shape.GrabHandles.TotalBounds);
                            shape.Location = this.SelectedShape.Location.Subtract(relativeLocations[i - 1]);
                            this.InvalidateShape(shape);
                        }
                        Point p = new Point(e.X, e.Y);
                        List<Shape> s = GetShapesAtPoint(p);
                        if (this.SelectedShape.GetShapeTypeName() == "Compensator")
                        {
                            if (s.Count > 1)
                            {
                                for (int i = 0; i < s.Count; i++)
                                {
                                    if (s[i].GetShapeTypeName() == "Pipeline")
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                                //MessageBox.Show(s.Count.ToString());
                            }
                            else
                                flag = false;
                        }

                    }
                    else if (hitStatus != Shape.HitStatus.None)
                    {
                        var oldRect = this.SelectedShape.GrabHandles.TotalBounds;

                        // 1. Resize main selected shape
                        oldBounds.Add(oldRect);
                        this.SelectedShape.Resize(hitStatus, e.X, e.Y);
                        this.InvalidateShape(this.SelectedShape);

                        var newRect = this.SelectedShape.GrabHandles.TotalBounds;
                        var dx = newRect.Width - oldRect.Width;

                        // 2. Resize remaining selected shapes
                        for (var i = 1; i < this.SelectedShapes.Count; i++)
                        {
                            var shape = this.SelectedShapes[i];
                            var currentShapeBounds = shape.GrabHandles.TotalBounds;

                            oldBounds.Add(currentShapeBounds);
                            var oldSize = shape.Size;
                            oldSize.Width += dx;
                            if (oldSize.Width < shape.MinimumSize.Width) oldSize.Width = shape.MinimumSize.Width;
                            shape.Size = oldSize;
                            this.InvalidateShape(shape);
                        }
                    }
                }

                foreach (var rect in oldBounds)
                {
                    this.InvalidateRectangle(rect);
                }
            }
            else
            {
                this.DoHitTest(e.Location);
            }
        }
        
        private void DoHitTest(Point p)
        {
            if (this.SelectedShape != null)
                hitStatus = this.SelectedShape.GetHitTest(p);
            else
                hitStatus = Shape.HitStatus.None;

            this.SetCursor();
        }

        #endregion
        
        #region Util

        private bool ShouldAlignShape(int shapeLocation, int otherShapeLocation)
        {
            return shapeLocation.IsBetween(otherShapeLocation + this.ShapeAlignDistance,
                                           otherShapeLocation - this.ShapeAlignDistance);

        }

        private bool ShouldSnapCanvas(int shapeLocation, int canvasLocation)
        {
            return shapeLocation.IsBetween(canvasLocation - this.BorderSnapDistance,
                                           canvasLocation + this.BorderSnapDistance);
        }

        private bool ShouldSnapShape(int shapeLocation, int shapeDimension, int otherShapeLocation, int otherShapeDimension)
        {
            return (shapeLocation.IsBetween(otherShapeLocation, otherShapeLocation + otherShapeDimension) ||
                    otherShapeLocation.IsBetween(shapeLocation, shapeLocation + shapeDimension));
        }

        private Shape GetShapeAtPoint(Point p)
        {
            var shapes = this.GetShapesAtPoint(p);
            return shapes.Count > 0 ? shapes[0] : null;
        }

        public List<Shape> GetShapesAtPoint(Point p)
        {
            return (from Shape s in this.Shapes
                    where s.GrabHandles.TotalBounds.Contains(p)
                    orderby this.Shapes.IndexOf(s) descending 
                    select s).ToList();
        }

        public List<Shape> GetShapes()
        {
            return (from Shape s in this.Shapes
                    orderby this.Shapes.IndexOf(s) descending
                    select s).ToList();
        }

        private void SetCursor()
        {
            if (this.SelectedShape != null && this.SelectedShape.Locked)
            {
                this.Cursor = Cursors.Default;
                return;
            }

            if (hitStatus == Shape.HitStatus.Drag)
                this.Cursor = Cursors.SizeAll;
            else if (hitStatus == Shape.HitStatus.ResizeBottom | hitStatus == Shape.HitStatus.ResizeTop)
                this.Cursor = Cursors.SizeNS;
            else if (hitStatus == Shape.HitStatus.ResizeLeft | hitStatus == Shape.HitStatus.ResizeRight)
                this.Cursor = Cursors.SizeWE;
            else if (hitStatus == Shape.HitStatus.ResizeBottomLeft | hitStatus == Shape.HitStatus.ResizeTopRight)
                this.Cursor = Cursors.SizeNESW;
            else if (hitStatus == Shape.HitStatus.ResizeBottomRight | hitStatus == Shape.HitStatus.ResizeTopLeft)
                this.Cursor = Cursors.SizeNWSE;
            else
                this.Cursor = Cursors.Default;
        }

        #endregion
        
        #endregion
    }
}
