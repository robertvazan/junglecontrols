using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JungleControls
{
    class PrecisePopupPlacementModel
    {
        public readonly PrecisePopupPlacement PlacementControl;
        public readonly Observable<HorizontalAlignment> HorizontalTargetAlignment = new Observable<HorizontalAlignment>();
        public readonly Observable<VerticalAlignment> VerticalTargetAlignment = new Observable<VerticalAlignment>();
        public readonly Observable<HorizontalAlignment> HorizontalPopupAlignment = new Observable<HorizontalAlignment>();
        public readonly Observable<VerticalAlignment> VerticalPopupAlignment = new Observable<VerticalAlignment>();
        public readonly Observable<double> HorizontalOffset = new Observable<double>();
        public readonly Observable<double> VerticalOffset = new Observable<double>();

        public PrecisePopupPlacementModel(PrecisePopupPlacement control) { PlacementControl = control; }
    }
}
