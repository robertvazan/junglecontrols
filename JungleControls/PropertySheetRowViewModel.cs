using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UpdateControls;
using UpdateControls.Fields;

namespace JungleControls
{
    class PropertySheetRowViewModel
    {
        readonly FrameworkElement Element;
        readonly Independent<object> HeaderIndependent = new Independent<object>();

        public PropertySheetViewModel Sheet { get; private set; }
        public object Header { get { return HeaderIndependent.Value; } }
        public FrameworkElement Content { get { return Element; } }

        public PropertySheetRowViewModel(PropertySheetViewModel sheet, FrameworkElement element)
        {
            Sheet = sheet;
            Element = element;
            ControlFacade.Lift(element, PropertySheet.HeaderProperty, HeaderIndependent);
        }
    }
}
