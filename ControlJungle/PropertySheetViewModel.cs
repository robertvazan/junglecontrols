using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateControls.Collections;
using UpdateControls.Fields;

namespace ControlJungle
{
    class PropertySheetViewModel
    {
        readonly Independent<object> DataContextValue = new Independent<object>();
        readonly IndependentList<PropertySheetRowViewModel> RowsValue = new IndependentList<PropertySheetRowViewModel>();

        public object DataContext { get { return DataContextValue.Value; } set { DataContextValue.Value = value; } }
        public IList<PropertySheetRowViewModel> Rows { get { return RowsValue; } }
    }
}
