using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UpdateControls.Collections;
using UpdateControls.Fields;

namespace JungleControls
{
    class PropertySheetViewModel
    {
        readonly Independent<object> OuterDataContextIndependent = new Independent<object>();
        readonly IndependentList<PropertySheetRowViewModel> RowsIndependent = new IndependentList<PropertySheetRowViewModel>();

        public object OuterDataContext { get { return OuterDataContextIndependent.Value; } }
        public IList<PropertySheetRowViewModel> Rows { get { return RowsIndependent; } }

        public PropertySheetViewModel(PropertySheet element)
        {
            ControlFacade.Lift(element, FrameworkElement.DataContextProperty, OuterDataContextIndependent);
            ControlFacade.Lift(element.Items, RowsIndependent, elem => new PropertySheetRowViewModel(this, elem));
        }
    }
}
