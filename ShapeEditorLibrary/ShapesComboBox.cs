using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ShapeEditorLibrary.Shapes;
using System.Drawing;

namespace ShapeEditorLibrary
{
    public class ShapesComboBox : ComboBox
    {
        public ShapesComboBox()
        {
            this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            if (e.Index < 0) return;

            var s = this.Items[e.Index] as Shape;
            if (s == null) return;

            Font boldFont;
            // Not all fonts allow Bold, not sure how else to handle this except for a try/catch...
            try
            {
                boldFont = new Font(this.Font, FontStyle.Bold);
            }
            catch (Exception)
            {
                boldFont = this.Font;
            }

            string name = s.Name;
            string type = s.GetShapeTypeName();

            SizeF nameSize = e.Graphics.MeasureString(name, boldFont);
            float y = (e.Bounds.Height - nameSize.Height) / 2f;

            e.DrawBackground();

            var selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            var foreColor = selected ? Color.White : this.ForeColor;
            e.Graphics.DrawString(name, boldFont, new SolidBrush(foreColor), e.Bounds.X, e.Bounds.Y + y);
            e.Graphics.DrawString(type, this.Font, new SolidBrush(foreColor), e.Bounds.X + nameSize.Width + 5, e.Bounds.Y + y);
            e.DrawFocusRectangle();
        }
    }
}
