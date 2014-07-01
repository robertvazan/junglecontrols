﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UpdateControls.XAML;

namespace JungleControls
{
    [ContentProperty("Items")]
    public partial class PropertySheet : UserControl
    {
        static readonly DependencyPropertyKey ItemsPropertyKey = DependencyProperty.RegisterReadOnly("Items", typeof(ObservableCollection<FrameworkElement>), typeof(PropertySheet), new FrameworkPropertyMetadata());
        public static readonly DependencyProperty ItemsProperty = ItemsPropertyKey.DependencyProperty;
        public ObservableCollection<FrameworkElement> Items
        {
            get
            {
                var result = (ObservableCollection<FrameworkElement>)GetValue(ItemsProperty);
                if (result == null)
                    SetValue(ItemsPropertyKey, result = new ObservableCollection<FrameworkElement>());
                return result;
            }
        }
        
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.RegisterAttached("Header", typeof(object), typeof(PropertySheet));
        public static void SetHeader(FrameworkElement element, object value) { element.SetValue(HeaderProperty, value); }
        public static object GetHeader(FrameworkElement element) { return element.GetValue(HeaderProperty); }

        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof(FrameworkTemplate), typeof(PropertySheet));
        public FrameworkTemplate HeaderTemplate { get { return (FrameworkTemplate)GetValue(HeaderTemplateProperty); } set { SetValue(HeaderTemplateProperty, value); } }

        public static readonly DependencyProperty HeaderMarginProperty = DependencyProperty.Register("HeaderMargin", typeof(Thickness), typeof(PropertySheet), new FrameworkPropertyMetadata(new Thickness(5, 5, 15, 5)));
        public Thickness HeaderMargin { get { return (Thickness)GetValue(HeaderMarginProperty); } set { SetValue(HeaderMarginProperty, value); } }

        public static readonly DependencyProperty HeaderForegroundProperty = DependencyProperty.Register("HeaderForeground", typeof(Brush), typeof(PropertySheet), new FrameworkPropertyMetadata(Brushes.Black));
        public Brush HeaderForeground { get { return (Brush)GetValue(HeaderForegroundProperty); } set { SetValue(HeaderForegroundProperty, value); } }

        public static readonly DependencyProperty HeaderFontWeightProperty = DependencyProperty.Register("HeaderFontWeight", typeof(FontWeight), typeof(PropertySheet), new FrameworkPropertyMetadata(FontWeights.Bold));
        public FontWeight HeaderFontWeight { get { return (FontWeight)GetValue(HeaderFontWeightProperty); } set { SetValue(HeaderFontWeightProperty, value); } }

        public PropertySheet()
        {
            InitializeComponent();
            ((FrameworkElement)Content).DataContext = ForView.Wrap(new PropertySheetViewModel(this));
        }
    }
}
