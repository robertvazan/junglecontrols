using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UpdateControls.Collections;

namespace JungleControls
{
    class PropertySheetViewModel
    {
        readonly IndependentList<PropertySheetRowViewModel> RowsIndependent = new IndependentList<PropertySheetRowViewModel>();

        public PropertySheet Control { get; private set; }
        public IList<PropertySheetRowViewModel> Rows { get { return RowsIndependent; } }

        public PropertySheetViewModel(PropertySheet control)
        {
            Control = control;
            FacadeMapper.Lift(control.Items, RowsIndependent, elem => new PropertySheetRowViewModel(this, (FrameworkElement)elem));
        }
    }
}
