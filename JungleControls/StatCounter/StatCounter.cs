using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace JungleControls
{
    public class StatCounter : FacadeHeaderedContentControl
    {
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

        public static readonly DependencyProperty ContentForegroundProperty = DependencyProperty.Register("ContentForeground", typeof(Brush), typeof(StatCounter), new FrameworkPropertyMetadata(Brushes.Black));
        public Brush ContentForeground { get { return (Brush)GetValue(ContentForegroundProperty); } set { SetValue(ContentForegroundProperty, value); } }

        public static readonly DependencyProperty ContentFontSizeProperty = DependencyProperty.Register("ContentFontSize", typeof(double), typeof(StatCounter), new FrameworkPropertyMetadata(2 * SystemFonts.MessageFontSize));
        [TypeConverter(typeof(FontSizeConverter))]
        public double ContentFontSize { get { return (double)GetValue(ContentFontSizeProperty); } set { SetValue(ContentFontSizeProperty, value); } }

        public static readonly DependencyProperty ContentAlignmentProperty = DependencyProperty.Register("ContentAlignment", typeof(HorizontalAlignment), typeof(StatCounter), new FrameworkPropertyMetadata(HorizontalAlignment.Left));
        public HorizontalAlignment ContentAlignment { get { return (HorizontalAlignment)GetValue(ContentAlignmentProperty); } set { SetValue(ContentAlignmentProperty, value); } }

        public static readonly DependencyProperty ContentFontWeightProperty = DependencyProperty.Register("ContentFontWeight", typeof(FontWeight), typeof(StatCounter), new FrameworkPropertyMetadata(FontWeights.Normal));
        public FontWeight ContentFontWeight { get { return (FontWeight)GetValue(ContentFontWeightProperty); } set { SetValue(ContentFontWeightProperty, value); } }

        public StatCounter() : base(typeof(StatCounter)) { }
    }
}
