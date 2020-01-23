// Part of JungleControls: https://blog.machinezoo.com/junglecontrols-free-wpf-controls-for
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace JungleControls
{
    public static class ExposeControl
    {
        static readonly object NoObject = new object();

        public static readonly DependencyProperty AsProperty = DependencyProperty.RegisterAttached("As", typeof(object), typeof(ExposeControl),
            new FrameworkPropertyMetadata(NoObject, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, AsChanged));
        public static object GetAs(DependencyObject obj) { return obj.GetValue(AsProperty); }
        public static void SetAs(DependencyObject obj, object value) { obj.SetValue(AsProperty, value); }

        static void AsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue != obj)
                obj.Dispatcher.BeginInvoke(() => obj.SetCurrentValue(AsProperty, obj));
        }
    }
}
