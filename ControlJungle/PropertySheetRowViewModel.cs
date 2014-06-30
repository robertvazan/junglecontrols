using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UpdateControls.Fields;

namespace ControlJungle
{
    class PropertySheetRowViewModel
    {
        readonly PropertySheetViewModel SheetValue;
        readonly Independent<object> HeaderValue = new Independent<object>();
        readonly Independent<FrameworkElement> ContentValue = new Independent<FrameworkElement>();

        public PropertySheetViewModel Sheet { get { return SheetValue; } }
        public object Header { get { return HeaderValue.Value; } set { HeaderValue.Value = value; } }
        public FrameworkElement Content { get { return ContentValue.Value; } set { ContentValue.Value = value; } }

        public PropertySheetRowViewModel(PropertySheetViewModel sheet)
        {
            SheetValue = sheet;
        }
    }
}
