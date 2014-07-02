using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UpdateControls.Fields;
using UpdateControls.XAML;

namespace JungleControls
{
    public partial class StatCounter : UserControl
    {
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(string), typeof(StatCounter));
        public string Header { get { return (string)GetValue(HeaderProperty); } set { SetValue(HeaderProperty, value); } }

        public static readonly DependencyProperty HeaderPositionProperty = DependencyProperty.Register("HeaderPosition", typeof(StatCounterHeaderPosition), typeof(StatCounter));
        public StatCounterHeaderPosition HeaderPosition { get { return (StatCounterHeaderPosition)GetValue(HeaderPositionProperty); } set { SetValue(HeaderPositionProperty, value); } }

        public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register("Spacing", typeof(double), typeof(StatCounter), new FrameworkPropertyMetadata(0.0));
        public double Spacing { get { return (double)GetValue(SpacingProperty); } set { SetValue(SpacingProperty, value); } }

        public static readonly DependencyProperty HeaderForegroundProperty = DependencyProperty.Register("HeaderForeground", typeof(Brush), typeof(StatCounter), new FrameworkPropertyMetadata(Brushes.Black));
        public Brush HeaderForeground { get { return (Brush)GetValue(HeaderForegroundProperty); } set { SetValue(HeaderForegroundProperty, value); } }

        public static readonly DependencyProperty HeaderFontSizeProperty = DependencyProperty.Register("HeaderFontSize", typeof(double), typeof(StatCounter), new FrameworkPropertyMetadata(SystemFonts.MessageFontSize));
        [TypeConverter(typeof(FontSizeConverter))]
        public double HeaderFontSize { get { return (double)GetValue(HeaderFontSizeProperty); } set { SetValue(HeaderFontSizeProperty, value); } }

        public static readonly DependencyProperty HeaderAlignmentProperty = DependencyProperty.Register("HeaderAlignment", typeof(HorizontalAlignment), typeof(StatCounter), new FrameworkPropertyMetadata(HorizontalAlignment.Left));
        public HorizontalAlignment HeaderAlignment { get { return (HorizontalAlignment)GetValue(HeaderAlignmentProperty); } set { SetValue(HeaderAlignmentProperty, value); } }

        public static readonly DependencyProperty HeaderFontWeightProperty = DependencyProperty.Register("HeaderFontWeight", typeof(FontWeight), typeof(StatCounter), new FrameworkPropertyMetadata(FontWeights.Normal));
        public FontWeight HeaderFontWeight { get { return (FontWeight)GetValue(HeaderFontWeightProperty); } set { SetValue(HeaderFontWeightProperty, value); } }

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(object), typeof(StatCounter));
        public object Data { get { return GetValue(DataProperty); } set { SetValue(DataProperty, value); } }

        public static readonly DependencyProperty DataForegroundProperty = DependencyProperty.Register("DataForeground", typeof(Brush), typeof(StatCounter), new FrameworkPropertyMetadata(Brushes.Black));
        public Brush DataForeground { get { return (Brush)GetValue(DataForegroundProperty); } set { SetValue(DataForegroundProperty, value); } }

        public static readonly DependencyProperty DataFontSizeProperty = DependencyProperty.Register("DataFontSize", typeof(double), typeof(StatCounter), new FrameworkPropertyMetadata(2 * SystemFonts.MessageFontSize));
        [TypeConverter(typeof(FontSizeConverter))]
        public double DataFontSize { get { return (double)GetValue(DataFontSizeProperty); } set { SetValue(DataFontSizeProperty, value); } }

        public static readonly DependencyProperty DataAlignmentProperty = DependencyProperty.Register("DataAlignment", typeof(HorizontalAlignment), typeof(StatCounter), new FrameworkPropertyMetadata(HorizontalAlignment.Left));
        public HorizontalAlignment DataAlignment { get { return (HorizontalAlignment)GetValue(DataAlignmentProperty); } set { SetValue(DataAlignmentProperty, value); } }

        public static readonly DependencyProperty DataFontWeightProperty = DependencyProperty.Register("DataFontWeight", typeof(FontWeight), typeof(StatCounter), new FrameworkPropertyMetadata(FontWeights.Normal));
        public FontWeight DataFontWeight { get { return (FontWeight)GetValue(DataFontWeightProperty); } set { SetValue(DataFontWeightProperty, value); } }

        public StatCounter()
        {
            InitializeComponent();
            ((FrameworkElement)Content).DataContext = ForView.Wrap(new ViewModel(this));
        }

        class ViewModel
        {
            readonly Independent<StatCounterHeaderPosition> HeaderPositionIndependent = new Independent<StatCounterHeaderPosition>();
            readonly Independent<double> SpacingIndependent = new Independent<double>();
            readonly Independent<object> DataIndependent = new Independent<object>();

            public StatCounter Control { get; private set; }
            public bool IsTop { get { return HeaderPositionIndependent.Value == StatCounterHeaderPosition.Top; } }
            public bool IsBottom { get { return HeaderPositionIndependent.Value == StatCounterHeaderPosition.Bottom; } }
            public int HeaderRow { get { return IsTop ? 0 : 1; } }
            public string Data { get { return DataIndependent.Value != null ? DataIndependent.Value.ToString() : ""; } }
            public int DataRow { get { return 1 - HeaderRow; } }
            public double HalfSpacing { get { return SpacingIndependent.Value / 2; } }
            public Thickness HeaderMargin { get { return new Thickness(0, IsBottom ? HalfSpacing : 0, 0, IsTop ? HalfSpacing : 0); } }
            public Thickness DataMargin { get { return new Thickness(0, IsTop ? HalfSpacing : 0, 0, IsBottom ? HalfSpacing : 0); } }

            public ViewModel(StatCounter element)
            {
                Control = element;
                ControlFacade.LiftAll(element, this);
            }
        }
    }
}
