// Part of JungleControls: https://blog.machinezoo.com/junglecontrols-free-wpf-controls-for
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JungleControls
{
    class PrecisePopupCandidate
    {
        readonly PrecisePopupModel Popup;
        public readonly PrecisePopupPlacementModel Placement;
        readonly bool IsMaxSize;
        public readonly PrecisePopupCandidate Extreme;
        double PopupWidth { get { return IsMaxSize ? Popup.MaxPopupWidth : Popup.PopupWidth.Value; } }
        double PopupHeight { get { return IsMaxSize ? Popup.MaxPopupHeight : Popup.PopupHeight.Value; } }
        Rect ScreenBounds { get { return Popup.ScreenBounds.Value; } }
        double TargetX { get { return Popup.TargetX.Value + Placement.HorizontalOffset.Value; } }
        double TargetY { get { return Popup.TargetY.Value + Placement.VerticalOffset.Value; } }
        double PivotX
        {
            get
            {
                switch (Placement.HorizontalTargetAlignment.Value)
                {
                    case PrecisePopupHorizontalAlignment.Left: return TargetX;
                    case PrecisePopupHorizontalAlignment.Right: return TargetX + Popup.TargetWidth.Value;
                    case PrecisePopupHorizontalAlignment.Center: return TargetX + Popup.TargetWidth.Value / 2;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }
        double PivotY
        {
            get
            {
                switch (Placement.VerticalTargetAlignment.Value)
                {
                    case PrecisePopupVerticalAlignment.Top: return TargetY;
                    case PrecisePopupVerticalAlignment.Bottom: return TargetY + Popup.TargetHeight.Value;
                    case PrecisePopupVerticalAlignment.Center: return TargetY + Popup.TargetHeight.Value / 2;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }
        double RawMaxWidth
        {
            get
            {
                switch (Placement.HorizontalPopupAlignment.Value)
                {
                    case PrecisePopupHorizontalAlignment.Left: return ScreenBounds.Right - PivotX;
                    case PrecisePopupHorizontalAlignment.Right: return PivotX - ScreenBounds.Left;
                    case PrecisePopupHorizontalAlignment.Center: return 2 * Math.Min(ScreenBounds.Right - PivotX, PivotX - ScreenBounds.Left);
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }
        double RawMaxHeight
        {
            get
            {
                switch (Placement.VerticalPopupAlignment.Value)
                {
                    case PrecisePopupVerticalAlignment.Top: return ScreenBounds.Bottom - PivotY;
                    case PrecisePopupVerticalAlignment.Bottom: return PivotY - ScreenBounds.Top;
                    case PrecisePopupVerticalAlignment.Center: return 2 * Math.Min(ScreenBounds.Bottom - PivotY, PivotY - ScreenBounds.Top);
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }
        public double MaxWidth { get { return !IsMaxSize && Placement.ClipToScreen.Value ? Math.Max(1, RawMaxWidth) : Double.PositiveInfinity; } }
        public double MaxHeight { get { return !IsMaxSize && Placement.ClipToScreen.Value ? Math.Max(1, RawMaxHeight) : Double.PositiveInfinity; } }
        double LimitedWidth { get { return Math.Min(PopupWidth, MaxWidth); } }
        double LimitedHeight { get { return Math.Min(PopupHeight, MaxHeight); } }
        public double X
        {
            get
            {
                switch (Placement.HorizontalPopupAlignment.Value)
                {
                    case PrecisePopupHorizontalAlignment.Left: return PivotX;
                    case PrecisePopupHorizontalAlignment.Right: return PivotX - LimitedWidth;
                    case PrecisePopupHorizontalAlignment.Center: return PivotX - LimitedWidth / 2;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }
        public double Y
        {
            get
            {
                switch (Placement.VerticalPopupAlignment.Value)
                {
                    case PrecisePopupVerticalAlignment.Top: return PivotY;
                    case PrecisePopupVerticalAlignment.Bottom: return PivotY - LimitedHeight;
                    case PrecisePopupVerticalAlignment.Center: return PivotY - LimitedHeight / 2;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }
        public double ClippedArea
        {
            get
            {
                var visible = new Rect(X, Y, PopupWidth, PopupHeight);
                visible.Intersect(Popup.ScreenBounds.Value);
                return PopupWidth * PopupHeight - visible.Width * visible.Height;
            }
        }

        public PrecisePopupCandidate(PrecisePopupModel popup, PrecisePopupPlacementModel placement, bool max = false)
        {
            Popup = popup;
            Placement = placement;
            IsMaxSize = max;
            if (!IsMaxSize)
                Extreme = new PrecisePopupCandidate(popup, placement, true);
        }
    }
}
