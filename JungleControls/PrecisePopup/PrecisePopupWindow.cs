using Assisticant;
using Assisticant.Facades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace JungleControls
{
    class PrecisePopupWindow : Window
    {
        readonly PrecisePopupModel Model;
        readonly ComputedJob PositioningJob;
        bool IsHidden;

        static PrecisePopupWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PrecisePopupWindow), new FrameworkPropertyMetadata(typeof(PrecisePopupWindow)));
            TopmostProperty.OverrideMetadata(typeof(PrecisePopupWindow), new FrameworkPropertyMetadata(true));
            WindowStyleProperty.OverrideMetadata(typeof(PrecisePopupWindow), new FrameworkPropertyMetadata(WindowStyle.None));
            ResizeModeProperty.OverrideMetadata(typeof(PrecisePopupWindow), new FrameworkPropertyMetadata(ResizeMode.NoResize));
            SizeToContentProperty.OverrideMetadata(typeof(PrecisePopupWindow), new FrameworkPropertyMetadata(SizeToContent.WidthAndHeight));
            ShowInTaskbarProperty.OverrideMetadata(typeof(PrecisePopupWindow), new FrameworkPropertyMetadata(false));
            AllowsTransparencyProperty.OverrideMetadata(typeof(PrecisePopupWindow), new FrameworkPropertyMetadata(true));
        }

        internal PrecisePopupWindow(PrecisePopupModel model)
        {
            Model = model;
            DataContext = FacadeModel.Wrap(Model);
            PositioningJob = new ComputedJob(() =>
            {
                Left = Model.SelectedCandidate.X;
                Top = Model.SelectedCandidate.Y;
                Model.PopupControl.UpdateSelectedPlacement();
            });
            PositioningJob.Start();
            Loaded += (s, args) => Mouse.Capture(this, CaptureMode.SubTree);
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            OnPreviewMouseButton(e);
            base.OnPreviewMouseLeftButtonDown(e);
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            OnPreviewMouseButton(e);
            base.OnPreviewMouseLeftButtonUp(e);
        }

        protected override void OnPreviewMouseRightButtonDown(MouseButtonEventArgs e)
        {
            OnPreviewMouseButton(e);
            base.OnPreviewMouseRightButtonDown(e);
        }

        protected override void OnPreviewMouseRightButtonUp(MouseButtonEventArgs e)
        {
            OnPreviewMouseButton(e);
            base.OnPreviewMouseRightButtonUp(e);
        }

        void OnPreviewMouseButton(MouseButtonEventArgs e)
        {
            if (InputHitTest(e.GetPosition(this)) == null)
            {
                if (Model.SuppressTarget.Value.InputHitTest(e.GetPosition(Model.SuppressTarget.Value)) != null)
                    e.Handled = true;
                Model.PopupControl.IsOpen = false;
            }
        }

        protected override void OnDeactivated(EventArgs e)
        {
            Model.PopupControl.IsOpen = false;
            base.OnDeactivated(e);
        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            base.OnLostMouseCapture(e);
            if (Mouse.Captured == null && !IsHidden)
                Mouse.Capture(this, CaptureMode.SubTree);
        }

        public void Shutdown()
        {
            IsHidden = true;
            PositioningJob.Stop();
            if (Mouse.Captured == this)
                Mouse.Capture(null);
            Hide();
            Dispatcher.BeginInvoke(new Action(() => Close()), DispatcherPriority.Input);
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            if (Math.Abs(Model.PopupWidth.Value - sizeInfo.NewSize.Width) > 0.001 || Math.Abs(Model.PopupHeight.Value - sizeInfo.NewSize.Height) > 0.001)
            {
                Model.PopupWidth.Value = sizeInfo.NewSize.Width;
                Model.PopupHeight.Value = sizeInfo.NewSize.Height;
                Model.WindowVisibility.Value = Visibility.Visible;
            }
        }
    }
}
