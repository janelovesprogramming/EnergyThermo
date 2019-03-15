using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShapeEditorLibrary
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
    public class ShapeToolboxItemAttribute : System.Attribute
    {
        public ShapeToolboxItemAttribute(bool visible, string name = "", string group = "")
        {
            _Visible = visible;
            _Name = name;
            _Group = group;
        }

        private readonly bool _Visible;
        public bool Visible
        {
            get { return _Visible;  }
        }

        private readonly string _Group;
        public string Group
        {
            get { return _Group; }
        }

        private readonly string _Name;
        public string Name
        {
            get { return _Name; }
        }
    }

}
