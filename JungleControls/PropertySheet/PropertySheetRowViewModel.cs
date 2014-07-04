using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UpdateControls.Fields;

namespace JungleControls
{
    class PropertySheetRowViewModel
    {
        readonly Independent<object> HeaderIndependent = new Independent<object>();

        public PropertySheetViewModel Sheet { get; private set; }
        public PropertySheet Control { get { return Sheet.Control; } }
        public FrameworkElement Content { get; private set; }
        public object Header { get { return HeaderIndependent.Value; } }
        public bool IsSimpleHeader { get { return Header is string && Control.HeaderTemplate == null; } }
        public Visibility SimpleHeaderVisible { get { return VisibilityEx.If(IsSimpleHeader); } }
        public Visibility TemplatedHeaderVisible { get { return VisibilityEx.If(!IsSimpleHeader); } }

        public PropertySheetRowViewModel(PropertySheetViewModel sheet, FrameworkElement element)
        {
            Sheet = sheet;
            Content = element;
            FacadeMapper.Lift(element, PropertySheet.HeaderProperty, HeaderIndependent);
        }
    }
}
