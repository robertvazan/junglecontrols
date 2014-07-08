using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using UpdateControls.Collections;
using UpdateControls.Fields;

namespace JungleControls
{
    class PropertySheetViewModel
    {
        readonly IndependentList<PropertySheetRowViewModel> RowsIndependent = new IndependentList<PropertySheetRowViewModel>();
        readonly Independent<FontWeight> FontWeightIndependent = new Independent<FontWeight>();
        readonly Independent<FontWeight?> HeaderFontWeightIndependent = new Independent<FontWeight?>();
        readonly Independent<Brush> ForegroundIndependent = new Independent<Brush>();
        readonly Independent<Brush> HeaderForegroundIndependent = new Independent<Brush>();

        public PropertySheet Control { get; private set; }
        public IList<PropertySheetRowViewModel> Rows { get { return RowsIndependent; } }
        public FontWeight HeaderFontWeight { get { return HeaderFontWeightIndependent.Value ?? FontWeightIndependent.Value; } }
        public Brush HeaderForeground { get { return HeaderForegroundIndependent.Value ?? ForegroundIndependent.Value; } }

        public PropertySheetViewModel(PropertySheet control)
        {
            Control = control;
            FacadeMapper.Lift(control.Items, RowsIndependent, elem => new PropertySheetRowViewModel(this, (FrameworkElement)elem));
            FacadeMapper.LiftAll(control, this);
        }
    }
}
