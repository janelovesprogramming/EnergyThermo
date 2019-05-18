using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace ShapeEditorLibrary.Shapes
{
    public class ShapeCollection : System.Collections.ObjectModel.Collection<Shape>
    {
        public ShapeCollection(Canvas c)
        {
            canvas = c;
        }

        #region Private Fields

        private readonly Canvas canvas;

        #endregion

        #region Events

        public event EventHandler CollectionChanged;

        protected virtual void OnCollectionChanged(EventArgs e)
        {
            if (this.CollectionChanged != null) this.CollectionChanged(this, e);
        }

        #endregion

        #region Methods

        protected override void ClearItems()
        {
            foreach (var shape in this)
                canvas.UnregisterShapeEvents(shape);
            base.ClearItems();
            this.OnCollectionChanged(EventArgs.Empty);
        }

        protected override void RemoveItem(int index)
        {
            canvas.UnregisterShapeEvents(this[index]);
            base.RemoveItem(index);
            this.OnCollectionChanged(EventArgs.Empty);
        }

        protected override void SetItem(int index, Shape item)
        {
            base.SetItem(index, item);
            this.OnCollectionChanged(EventArgs.Empty);
        }

        protected override void InsertItem(int index, Shape item)
        {
            // If the name is empty, this is a new shape, so we give it the next free name
            if (item.Name.Trim() == String.Empty)
            {
                item.Name = this.GetNextFreeName(item.GetType());
            }

            canvas.RegisterShapeEvents(item);
            base.InsertItem(index, item);
            canvas.Invalidate();

            this.OnCollectionChanged(EventArgs.Empty);
        }

        private string GetNextFreeName(Type t)
        {
            // If the type passed is not any Shape, return an empty string
            if (t.IsSubclassOf(typeof(Shape)))
            {
                // Keep a list of all shapes of type t so we can check their names
                var shapes = new List<Shape>();
                foreach (Shape s in this)
                {
                    if (t == s.GetType())
                    {
                        shapes.Add(s);
                    }
                }

                // Create a Hashtable with all names
                var h = new Hashtable(shapes.Count);
                foreach (Shape s in shapes)
                    h[s.Name] = null;
                //MessageBox.Show(t.Name.ToString());
                if (t.Name != "DistanseDiametr")
                {
                    Shape instance = (Shape)Activator.CreateInstance(t, new object[] { Point.Empty });

                    string defaultName = instance.GetShapeTypeName();
                    int i = 1;
                    // As long as the hashtable contains the name, we need to keep looking
                    // Once we find a name that is not in the hashtable, we found one empty spot, so we take it
                    while (h.ContainsKey(defaultName + i))
                        i++;

                    return defaultName + i;
                }
                else
                {
                    return "d+s";
                }
            }
            else
            {
                return String.Empty;
            }
        }

        #endregion
    }
}
