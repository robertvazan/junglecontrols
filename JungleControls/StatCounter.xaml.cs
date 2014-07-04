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

        public static readonly DependencyProperty DataStringFormatProperty = DependencyProperty.Register("DataStringFormat", typeof(string), typeof(StatCounter));
        public string DataStringFormat { get { return (string)GetValue(DataStringFormatProperty); } set { SetValue(DataStringFormatProperty, value); } }

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
            readonly Independent<string> DataStringFormatIndependent = new Independent<string>();

            public StatCounter Control { get; private set; }
            public bool IsTop { get { return HeaderPositionIndependent.Value == StatCounterHeaderPosition.Top; } }
            public bool IsBottom { get { return HeaderPositionIndependent.Value == StatCounterHeaderPosition.Bottom; } }
            public int HeaderRow { get { return IsTop ? 0 : 1; } }
            public int DataRow { get { return 1 - HeaderRow; } }
            public double HalfSpacing { get { return SpacingIndependent.Value / 2; } }
            public Thickness HeaderMargin { get { return new Thickness(0, IsBottom ? HalfSpacing : 0, 0, IsTop ? HalfSpacing : 0); } }
            public Thickness DataMargin { get { return new Thickness(0, IsTop ? HalfSpacing : 0, 0, IsBottom ? HalfSpacing : 0); } }

            public string Data
            {
                get
                {
                    var value = DataIndependent.Value;
                    if (value == null)
                        return "";
                    if (DataStringFormatIndependent.Value != null)
                        return String.Format("{0:" + DataStringFormatIndependent.Value + "}", value);
                    if (value is int || value is long || value is short || value is byte || value is sbyte || value is uint || value is ulong || value is ushort)
                        return String.Format("{0:N0}", value);
                    if (value is double || value is float || value is decimal)
                    {
                        var fp = Convert.ToDouble(value);
                        if (fp >= 0.1 && fp <= 1)
                            return fp.ToString("P0");
                        if (fp >= 0.01 && fp < 0.1)
                            return fp.ToString("P1");
                        if (fp > 0 && fp < 0.01)
                            return fp.ToString("P");
                        if (fp == 0)
                            return "0";
                        return fp.ToString("#,##0.##");
                    }
                    if (value is TimeSpan)
                    {
                        var ts = (TimeSpan)value;
                        string sign = ts < TimeSpan.Zero ? "-" : "";
                        ts = ts < TimeSpan.Zero ? -ts : ts;
                        string positive;
                        if (ts >= TimeSpan.FromDays(10) || ts >= TimeSpan.FromDays(1) && ts.Hours == 0)
                            positive = String.Format("{0}d", ts.Days);
                        else if (ts >= TimeSpan.FromDays(1))
                            positive = String.Format("{0}d {1}h", ts.Days, ts.Hours);
                        else if (ts >= TimeSpan.FromHours(1))
                            positive = ts.Minutes == 0 ? String.Format("{0}h", ts.Hours) : String.Format("{0}h {1}m", ts.Hours, ts.Minutes);
                        else if (ts >= TimeSpan.FromMinutes(1))
                            positive = ts.Seconds == 0 ? String.Format("{0}m", ts.Minutes) : String.Format("{0}m {1}s", ts.Minutes, ts.Seconds);
                        else if (ts >= TimeSpan.FromSeconds(10))
                            positive = String.Format("{0}s", ts.Seconds);
                        else if (ts >= TimeSpan.FromSeconds(1))
                            positive = String.Format("{0:0.#}s", ts.TotalSeconds);
                        else if (ts > TimeSpan.Zero)
                            positive = String.Format("{0}ms", ts.Milliseconds);
                        else
                            positive = "0";
                        return sign + positive;
                    }
                    return value.ToString();
                }
            }

            public ViewModel(StatCounter element)
            {
                Control = element;
                DependencyInverter.LiftAll(element, this);
            }
        }
    }
}
