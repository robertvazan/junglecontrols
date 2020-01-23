// Part of JungleControls: https://blog.machinezoo.com/junglecontrols-free-wpf-controls-for
using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JungleControls
{
    class PropertySheetRow
    {
        public readonly PropertySheetModel Sheet;
        public readonly object Content;
        public readonly object Header;

        public bool IsSimpleHeader { get { return Header is string && Sheet.HeaderTemplate.Value == null; } }
        public Visibility SimpleHeaderVisible { get { return VisibilityEx.If(IsSimpleHeader); } }
        public Visibility TemplatedHeaderVisible { get { return VisibilityEx.If(!IsSimpleHeader); } }

        public PropertySheetRow(PropertySheetModel sheet, object content, object header)
        {
            Sheet = sheet;
            Content = content;
            Header = header;
        }
    }
}
