using Assisticant.Collections;
using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace JungleControls
{
    class PropertySheetViewModel
    {
        readonly ObservableList<PropertySheetRowViewModel> RowsIndependent = new ObservableList<PropertySheetRowViewModel>();
        readonly Observable<FontWeight> FontWeightIndependent = new Observable<FontWeight>();
        readonly Observable<FontWeight?> HeaderFontWeightIndependent = new Observable<FontWeight?>();
        readonly Observable<Brush> ForegroundIndependent = new Observable<Brush>();
        readonly Observable<Brush> HeaderForegroundIndependent = new Observable<Brush>();

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
