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
        public readonly Observable<PrecisePopupHorizontalClip> HorizontalClipToScreen = new Observable<PrecisePopupHorizontalClip>();
        public readonly Observable<PrecisePopupVerticalClip> VerticalClipToScreen = new Observable<PrecisePopupVerticalClip>();
        public bool ClipLeft { get { return HorizontalClipToScreen.Value == PrecisePopupHorizontalClip.Left || HorizontalClipToScreen.Value == PrecisePopupHorizontalClip.Both; } }
        public bool ClipRight { get { return HorizontalClipToScreen.Value == PrecisePopupHorizontalClip.Right || HorizontalClipToScreen.Value == PrecisePopupHorizontalClip.Both; } }
        public bool ClipTop { get { return VerticalClipToScreen.Value == PrecisePopupVerticalClip.Top || VerticalClipToScreen.Value == PrecisePopupVerticalClip.Both; } }
        public bool ClipBottom { get { return VerticalClipToScreen.Value == PrecisePopupVerticalClip.Bottom || VerticalClipToScreen.Value == PrecisePopupVerticalClip.Both; } }

        public PrecisePopupPlacementModel(PrecisePopupPlacement control) { PlacementControl = control; }
    }
}
