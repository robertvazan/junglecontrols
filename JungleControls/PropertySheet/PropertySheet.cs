using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace JungleControls
{
    public class PropertySheet : FacadeItemsControl
    {
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.RegisterAttached("Header", typeof(object), typeof(PropertySheet));
        public static void SetHeader(FrameworkElement element, object value) { element.SetValue(HeaderProperty, value); }
        public static object GetHeader(FrameworkElement element) { return element.GetValue(HeaderProperty); }

        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(PropertySheet));
        public DataTemplate HeaderTemplate { get { return (DataTemplate)GetValue(HeaderTemplateProperty); } set { SetValue(HeaderTemplateProperty, value); } }

        public static readonly DependencyProperty HeaderMarginProperty = DependencyProperty.Register("HeaderMargin", typeof(Thickness), typeof(PropertySheet), new FrameworkPropertyMetadata(new Thickness(5, 5, 15, 5)));
        public Thickness HeaderMargin { get { return (Thickness)GetValue(HeaderMarginProperty); } set { SetValue(HeaderMarginProperty, value); } }

        public static readonly DependencyProperty HeaderForegroundProperty = DependencyProperty.Register("HeaderForeground", typeof(Brush), typeof(PropertySheet), new FrameworkPropertyMetadata(Brushes.Black));
        public Brush HeaderForeground { get { return (Brush)GetValue(HeaderForegroundProperty); } set { SetValue(HeaderForegroundProperty, value); } }

        public static readonly DependencyProperty HeaderFontWeightProperty = DependencyProperty.Register("HeaderFontWeight", typeof(FontWeight), typeof(PropertySheet), new FrameworkPropertyMetadata(FontWeights.Bold));
        public FontWeight HeaderFontWeight { get { return (FontWeight)GetValue(HeaderFontWeightProperty); } set { SetValue(HeaderFontWeightProperty, value); } }

        public static readonly DependencyProperty CellMarginProperty = DependencyProperty.Register("CellMargin", typeof(Thickness), typeof(PropertySheet), new FrameworkPropertyMetadata(new Thickness(5)));
        public Thickness CellMargin { get { return (Thickness)GetValue(CellMarginProperty); } set { SetValue(CellMarginProperty, value); } }

        public PropertySheet() : base(typeof(PropertySheet)) { }
    }
}
