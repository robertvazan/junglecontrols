// Part of JungleControls: https://blog.machinezoo.com/junglecontrols-free-wpf-controls-for
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
            ShowInTaskbarProperty.OverrideMetadata(typeof(PrecisePopupWindow), new FrameworkPropertyMetadata(false));
            AllowsTransparencyProperty.OverrideMetadata(typeof(PrecisePopupWindow), new FrameworkPropertyMetadata(true));
        }

        internal PrecisePopupWindow(PrecisePopupModel model)
        {
            Model = model;
            DataContext = FacadeModel.Wrap(Model);
            Width = 0;
            Height = 0;
            PositioningJob = new ComputedJob(() =>
            {
                Console.WriteLine("Placing window at ({0},{1})", Model.SelectedCandidate.X, Model.SelectedCandidate.Y);
                Left = Model.SelectedCandidate.X;
                Top = Model.SelectedCandidate.Y;
                Console.WriteLine("Max window size set to {0}x{1}", Model.SelectedCandidate.MaxWidth, Model.SelectedCandidate.MaxHeight);
                MaxWidth = Model.SelectedCandidate.MaxWidth;
                MaxHeight = Model.SelectedCandidate.MaxHeight;
                SizeToContent = SizeToContent.WidthAndHeight;
                Model.PopupControl.UpdateSelectedPlacement();
            });
            PositioningJob.Start();
            Loaded += (s, args) => Mouse.Capture(this, CaptureMode.SubTree);
        }

        public override void OnApplyTemplate()
        {
            (GetTemplateChild("Panel") as PrecisePopupPanel).Model = Model;
            base.OnApplyTemplate();
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
                if (Model.SuppressTarget.Value != null && Model.SuppressTarget.Value.InputHitTest(e.GetPosition(Model.SuppressTarget.Value)) != null)
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
    }
}
