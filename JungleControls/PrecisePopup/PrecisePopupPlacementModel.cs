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
        public readonly Observable<PrecisePopupHorizontalAlignment> HorizontalTargetAlignment = new Observable<PrecisePopupHorizontalAlignment>();
        public readonly Observable<PrecisePopupVerticalAlignment> VerticalTargetAlignment = new Observable<PrecisePopupVerticalAlignment>();
        public readonly Observable<PrecisePopupHorizontalAlignment> HorizontalPopupAlignment = new Observable<PrecisePopupHorizontalAlignment>();
        public readonly Observable<PrecisePopupVerticalAlignment> VerticalPopupAlignment = new Observable<PrecisePopupVerticalAlignment>();
        public readonly Observable<double> HorizontalOffset = new Observable<double>();
        public readonly Observable<double> VerticalOffset = new Observable<double>();
        public readonly Observable<bool> ClipToScreen = new Observable<bool>();

        public PrecisePopupPlacementModel(PrecisePopupPlacement control) { PlacementControl = control; }
    }
}
