using System;
using System.Collections.Generic;
using System.Text;
using ShapeEditorLibrary.Shapes;

namespace ShapeEditorLibrary
{
    public class SelectedShapeChangedEventArgs : System.EventArgs
    {
        private readonly Shape _Previous;
        private readonly Shape _Current;

        /// <summary>
        /// The previously selected <see cref="Shape"/>.
        /// </summary>
        public Shape Previous { get { return _Previous; } }

        /// <summary>
        /// The currently selected <see cref="Shape"/>.
        /// </summary>
        public Shape Current { get { return _Current; } }

        public SelectedShapeChangedEventArgs(Shape previous, Shape current)
        {
            _Previous = previous;
            _Current = current;
        }
    }
}
