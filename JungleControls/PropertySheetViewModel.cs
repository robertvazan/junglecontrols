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
        readonly Independent<object> OuterDataContextIndependent = new Independent<object>();
        readonly IndependentList<PropertySheetRowViewModel> RowsIndependent = new IndependentList<PropertySheetRowViewModel>();
        readonly Independent<FrameworkTemplate> HeaderTemplateIndependent = new Independent<FrameworkTemplate>();
        readonly Independent<Thickness> HeaderMarginIndependent = new Independent<Thickness>();
        readonly Independent<Brush> HeaderForegroundIndependent = new Independent<Brush>();
        readonly Independent<FontWeight> HeaderFontWeightIndependent = new Independent<FontWeight>();
        readonly Independent<Thickness> CellMarginIndependent = new Independent<Thickness>();

        public object OuterDataContext { get { return OuterDataContextIndependent.Value; } }
        public IList<PropertySheetRowViewModel> Rows { get { return RowsIndependent; } }
        public FrameworkTemplate HeaderTemplate { get { return HeaderTemplateIndependent.Value; } }
        public Thickness HeaderMargin { get { return HeaderMarginIndependent.Value; } }
        public Brush HeaderForeground { get { return HeaderForegroundIndependent.Value; } }
        public FontWeight HeaderFontWeight { get { return HeaderFontWeightIndependent.Value; } }
        public Thickness CellMargin { get { return CellMarginIndependent.Value; } }

        public PropertySheetViewModel(PropertySheet element)
        {
            ControlFacade.Lift(element, FrameworkElement.DataContextProperty, OuterDataContextIndependent);
            ControlFacade.Lift(element.Items, RowsIndependent, elem => new PropertySheetRowViewModel(this, elem));
            ControlFacade.LiftAll(element, this);
        }
    }
}
