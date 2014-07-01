using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JungleControls
{
    public class PropertySheet : ItemsControl
    {
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.RegisterAttached("Header", typeof(object), typeof(PropertySheet));
        public static void SetHeader(DependencyObject target, object value) { target.SetValue(HeaderProperty, value); }
        public static object GetHeader(DependencyObject target) { return target.GetValue(HeaderProperty); }

        static PropertySheet()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertySheet), new FrameworkPropertyMetadata(typeof(PropertySheet)));
        }
    }
}
