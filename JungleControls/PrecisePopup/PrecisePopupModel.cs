using Assisticant.Collections;
using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace JungleControls
{
    class PrecisePopupModel
    {
        public readonly PrecisePopup PopupControl;
        PrecisePopupWindow PopupWindow;
        public readonly Observable<FrameworkElement> PlacementTarget = new Observable<FrameworkElement>();
        public readonly Observable<FrameworkElement> SuppressTarget = new Observable<FrameworkElement>();
        public readonly ObservableList<PrecisePopupPlacement> Placements = new ObservableList<PrecisePopupPlacement>();
        public readonly Observable<double> TargetX = new Observable<double>();
        public readonly Observable<double> TargetY = new Observable<double>();
        public readonly Observable<double> TargetWidth = new Observable<double>();
        public readonly Observable<double> TargetHeight = new Observable<double>();
        public readonly Observable<double> PopupWidth = new Observable<double>();
        public readonly Observable<double> PopupHeight = new Observable<double>();
        double PopupWidthExtreme;
        double PopupHeightExtreme;
        public double MaxPopupWidth
        {
            get
            {
                if (PopupWidth.Value > PopupWidthExtreme)
                    PopupWidthExtreme = PopupWidth.Value;
                return PopupWidthExtreme;
            }
        }
        public double MaxPopupHeight
        {
            get
            {
                if (PopupHeight.Value > PopupHeightExtreme)
                    PopupHeightExtreme = PopupHeight.Value;
                return PopupHeightExtreme;
            }
        }
        public readonly Observable<Rect> ScreenBounds = new Observable<Rect>();
        PrecisePopupCandidate[] Candidates
        {
            get { return (Placements.Count != 0 ? Placements.ToArray() : new[] { new PrecisePopupPlacement() }).Select(p => new PrecisePopupCandidate(this, p.Model)).ToArray(); }
        }
        public PrecisePopupCandidate SelectedCandidate { get { return Candidates.OrderBy(c => c.Extreme.ClippedArea).First(); } }
        public bool IsOpen
        {
            get { return PopupWindow != null; }
            set
            {
                if (!value && PopupWindow != null)
                {
                    PopupWindow.Shutdown();
                    PopupWindow = null;
                    PopupControl.UpdateSelectedPlacement();
                }
                else if (value && PopupWindow == null && PlacementTarget.Value != null)
                {
                    var targetPosNative = PlacementTarget.Value.PointToScreen(new Point());
                    var transform = PresentationSource.FromVisual(PlacementTarget.Value).CompositionTarget.TransformFromDevice;
                    var targetPos = transform.Transform(targetPosNative);
                    TargetX.Value = targetPos.X;
                    TargetY.Value = targetPos.Y;
                    TargetWidth.Value = PlacementTarget.Value.ActualWidth;
                    TargetHeight.Value = PlacementTarget.Value.ActualHeight;
                    PopupWidth.Value = 0;
                    PopupHeight.Value = 0;
                    PopupWidthExtreme = 0;
                    PopupHeightExtreme = 0;
                    var targetCenterNative = PlacementTarget.Value.PointToScreen(new Point(TargetWidth.Value / 2, TargetHeight.Value / 2));
                    var targetCoordinates = new System.Drawing.Point(Convert.ToInt32(targetCenterNative.X), Convert.ToInt32(targetCenterNative.Y));
                    var targetScreen = Screen.AllScreens.FirstOrDefault(s => s.Bounds.Contains(targetCoordinates))
                        ?? (from screen in Screen.AllScreens
                            let center = screen.Bounds.Location + new System.Drawing.Size(screen.Bounds.Size.Width / 2, screen.Bounds.Size.Height / 2)
                            let deltaX = center.X - targetCoordinates.X
                            let deltaY = center.Y - targetCoordinates.Y
                            orderby deltaX * deltaX + deltaY * deltaY
                            select screen).First();
                    var boundsPos = transform.Transform(new Point(targetScreen.WorkingArea.Left, targetScreen.WorkingArea.Top));
                    var boundsSize = transform.Transform(new Point(targetScreen.WorkingArea.Right, targetScreen.WorkingArea.Bottom)) - boundsPos;
                    ScreenBounds.Value = new Rect(boundsPos, boundsSize);
                    PopupWindow = new PrecisePopupWindow(this);
                    PopupWindow.Owner = Window.GetWindow(PopupControl);
                    TextOptions.SetTextFormattingMode(PopupWindow, TextOptions.GetTextFormattingMode(PopupControl));
                    PopupWindow.Show();
                }
            }
        }

        public PrecisePopupModel(PrecisePopup control) { PopupControl = control; }
    }
}
