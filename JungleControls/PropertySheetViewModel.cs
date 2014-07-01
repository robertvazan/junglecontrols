using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateControls.Collections;

namespace JungleControls
{
    class PropertySheetViewModel
    {
        readonly IndependentList<PropertySheetRowViewModel> RowsIndependent = new IndependentList<PropertySheetRowViewModel>();

        public IList<PropertySheetRowViewModel> Rows { get { return RowsIndependent; } }
    }
}
