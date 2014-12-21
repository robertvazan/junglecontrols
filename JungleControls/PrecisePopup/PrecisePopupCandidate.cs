﻿using System;
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
        public double X
        {
            get
            {
                var x = Popup.TargetX.Value;
                if (Placement.HorizontalTargetAlignment.Value == HorizontalAlignment.Right)
                    x += Popup.TargetWidth.Value;
                else if (Placement.HorizontalTargetAlignment.Value != HorizontalAlignment.Left)
                    x += Popup.TargetWidth.Value / 2;
                if (Placement.HorizontalPopupAlignment.Value == HorizontalAlignment.Right)
                    x -= PopupWidth;
                else if (Placement.HorizontalPopupAlignment.Value != HorizontalAlignment.Left)
                    x -= PopupWidth / 2;
                return x + Placement.HorizontalOffset.Value;
            }
        }
        public double Y
        {
            get
            {
                var y = Popup.TargetY.Value;
                if (Placement.VerticalTargetAlignment.Value == VerticalAlignment.Bottom)
                    y += Popup.TargetHeight.Value;
                else if (Placement.VerticalTargetAlignment.Value != VerticalAlignment.Top)
                    y += Popup.TargetHeight.Value / 2;
                if (Placement.VerticalPopupAlignment.Value == VerticalAlignment.Bottom)
                    y -= PopupHeight;
                else if (Placement.VerticalPopupAlignment.Value != VerticalAlignment.Top)
                    y -= PopupHeight / 2;
                return y + Placement.VerticalOffset.Value;
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
