using System;
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
using UpdateControls.Collections;
using UpdateControls.Fields;
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

        public PropertySheet()
        {
            InitializeComponent();
            ((FrameworkElement)Content).DataContext = ForView.Wrap(new ViewModel(this));
        }

        class ViewModel
        {
            readonly IndependentList<RowViewModel> RowsIndependent = new IndependentList<RowViewModel>();

            public PropertySheet Control { get; private set; }
            public IList<RowViewModel> Rows { get { return RowsIndependent; } }

            public ViewModel(PropertySheet element)
            {
                Control = element;
                DependencyInverter.Lift(element.Items, RowsIndependent, elem => new RowViewModel(this, elem));
            }
        }

        class RowViewModel
        {
            readonly Independent<object> HeaderIndependent = new Independent<object>();

            public ViewModel Sheet { get; private set; }
            public PropertySheet Control { get { return Sheet.Control; } }
            public FrameworkElement Content { get; private set; }
            public object Header { get { return HeaderIndependent.Value; } }
            public bool IsSimpleHeader { get { return Header is string && Control.HeaderTemplate == null; } }
            public Visibility SimpleHeaderVisible { get { return VisibilityEx.If(IsSimpleHeader); } }
            public Visibility TemplatedHeaderVisible { get { return VisibilityEx.If(!IsSimpleHeader); } }

            public RowViewModel(ViewModel sheet, FrameworkElement element)
            {
                Sheet = sheet;
                Content = element;
                DependencyInverter.Lift(element, PropertySheet.HeaderProperty, HeaderIndependent);
            }
        }
    }
}
