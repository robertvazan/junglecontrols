using Assisticant;
using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace JungleControls
{
    public class StatCounter : HeaderedContentControl, IFacadeObject
    {
        static readonly FacadeType FacadeType;
        readonly FacadeInstance FacadeInstanceRef;
        FacadeInstance IFacadeObject.FacadeInstance { get { return FacadeInstanceRef; } }

        public static readonly DependencyProperty HeaderPositionProperty = DependencyProperty.Register("HeaderPosition", typeof(StatCounterHeaderPosition), typeof(StatCounter), new FacadePropertyMetadata());
        public StatCounterHeaderPosition HeaderPosition { get { return (StatCounterHeaderPosition)GetValue(HeaderPositionProperty); } set { SetValue(HeaderPositionProperty, value); } }

        public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register("Spacing", typeof(double), typeof(StatCounter), new FacadePropertyMetadata(0.0));
        public double Spacing { get { return (double)GetValue(SpacingProperty); } set { SetValue(SpacingProperty, value); } }

        public static readonly DependencyProperty HeaderForegroundProperty = DependencyProperty.Register("HeaderForeground", typeof(Brush), typeof(StatCounter), new FacadePropertyMetadata());
        public Brush HeaderForeground { get { return (Brush)GetValue(HeaderForegroundProperty); } set { SetValue(HeaderForegroundProperty, value); } }

        public static readonly DependencyProperty HeaderFontSizeProperty = DependencyProperty.Register("HeaderFontSize", typeof(double?), typeof(StatCounter), new FacadePropertyMetadata());
        [TypeConverter(typeof(FontSizeConverter))]
        public double? HeaderFontSize { get { return (double?)GetValue(HeaderFontSizeProperty); } set { SetValue(HeaderFontSizeProperty, value); } }

        public static readonly DependencyProperty HeaderAlignmentProperty = DependencyProperty.Register("HeaderAlignment", typeof(HorizontalAlignment), typeof(StatCounter), new FrameworkPropertyMetadata(HorizontalAlignment.Left));
        public HorizontalAlignment HeaderAlignment { get { return (HorizontalAlignment)GetValue(HeaderAlignmentProperty); } set { SetValue(HeaderAlignmentProperty, value); } }

        public static readonly DependencyProperty HeaderFontWeightProperty = DependencyProperty.Register("HeaderFontWeight", typeof(FontWeight?), typeof(StatCounter), new FacadePropertyMetadata());
        public FontWeight? HeaderFontWeight { get { return (FontWeight?)GetValue(HeaderFontWeightProperty); } set { SetValue(HeaderFontWeightProperty, value); } }

        public static readonly DependencyProperty ContentForegroundProperty = DependencyProperty.Register("ContentForeground", typeof(Brush), typeof(StatCounter), new FacadePropertyMetadata());
        public Brush ContentForeground { get { return (Brush)GetValue(ContentForegroundProperty); } set { SetValue(ContentForegroundProperty, value); } }

        public static readonly DependencyProperty ContentFontSizeProperty = DependencyProperty.Register("ContentFontSize", typeof(double?), typeof(StatCounter), new FacadePropertyMetadata());
        [TypeConverter(typeof(FontSizeConverter))]
        public double? ContentFontSize { get { return (double?)GetValue(ContentFontSizeProperty); } set { SetValue(ContentFontSizeProperty, value); } }

        public static readonly DependencyProperty ContentAlignmentProperty = DependencyProperty.Register("ContentAlignment", typeof(HorizontalAlignment), typeof(StatCounter), new FrameworkPropertyMetadata(HorizontalAlignment.Left));
        public HorizontalAlignment ContentAlignment { get { return (HorizontalAlignment)GetValue(ContentAlignmentProperty); } set { SetValue(ContentAlignmentProperty, value); } }

        public static readonly DependencyProperty ContentFontWeightProperty = DependencyProperty.Register("ContentFontWeight", typeof(FontWeight?), typeof(StatCounter), new FacadePropertyMetadata());
        public FontWeight? ContentFontWeight { get { return (FontWeight?)GetValue(ContentFontWeightProperty); } set { SetValue(ContentFontWeightProperty, value); } }

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register("Mode", typeof(StatCounterMode), typeof(StatCounter), new FacadePropertyMetadata(StatCounterMode.Auto));
        public StatCounterMode Mode { get { return (StatCounterMode)GetValue(ModeProperty); } set { SetValue(ModeProperty, value); } }

        static StatCounter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StatCounter), new FrameworkPropertyMetadata(typeof(StatCounter)));
            IsTabStopProperty.OverrideMetadata(typeof(StatCounter), new FrameworkPropertyMetadata(false));
            FacadeType = new FacadeType(typeof(StatCounter), typeof(StatCounterModel));
        }

        public StatCounter()
        {
            FacadeInstanceRef = new FacadeInstance(FacadeType, this);
        }

        public override void OnApplyTemplate()
        {
            FacadeInstanceRef.ApplyModel(GetTemplateChild("Root"));
            base.OnApplyTemplate();
        }
    }
}
