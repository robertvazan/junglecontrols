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
        readonly Independent<string> HeaderIndependent = new Independent<string>();

        public string Header { get { return HeaderIndependent.Value; } }
        public FrameworkElement Content { get { return Element; } }

        public PropertySheetRowViewModel(FrameworkElement element)
        {
            Element = element;
            ControlFacade.Lift(element, PropertySheet.HeaderProperty, HeaderIndependent);
        }
    }
}
