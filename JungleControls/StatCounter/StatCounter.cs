using Assisticant;
using Assisticant.Facades;
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
    public class StatCounter : HeaderedContentControl
    {
        readonly StatCounterModel Model = new StatCounterModel();

        public static readonly DependencyProperty HeaderPositionProperty = DependencyProperty.Register("HeaderPosition", typeof(StatCounterHeaderPosition), typeof(StatCounter));
        public StatCounterHeaderPosition HeaderPosition { get { return (StatCounterHeaderPosition)GetValue(HeaderPositionProperty); } set { SetValue(HeaderPositionProperty, value); } }

        public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register("Spacing", typeof(double), typeof(StatCounter), new FrameworkPropertyMetadata(0.0));
        public double Spacing { get { return (double)GetValue(SpacingProperty); } set { SetValue(SpacingProperty, value); } }

        public static readonly DependencyProperty HeaderForegroundProperty = DependencyProperty.Register("HeaderForeground", typeof(Brush), typeof(StatCounter));
        public Brush HeaderForeground { get { return (Brush)GetValue(HeaderForegroundProperty); } set { SetValue(HeaderForegroundProperty, value); } }

        public static readonly DependencyProperty HeaderFontSizeProperty = DependencyProperty.Register("HeaderFontSize", typeof(double?), typeof(StatCounter));
        [TypeConverter(typeof(FontSizeConverter))]
        public double? HeaderFontSize { get { return (double?)GetValue(HeaderFontSizeProperty); } set { SetValue(HeaderFontSizeProperty, value); } }

        public static readonly DependencyProperty HeaderAlignmentProperty = DependencyProperty.Register("HeaderAlignment", typeof(HorizontalAlignment), typeof(StatCounter), new FrameworkPropertyMetadata(HorizontalAlignment.Left));
        public HorizontalAlignment HeaderAlignment { get { return (HorizontalAlignment)GetValue(HeaderAlignmentProperty); } set { SetValue(HeaderAlignmentProperty, value); } }

        public static readonly DependencyProperty HeaderFontWeightProperty = DependencyProperty.Register("HeaderFontWeight", typeof(FontWeight?), typeof(StatCounter));
        public FontWeight? HeaderFontWeight { get { return (FontWeight?)GetValue(HeaderFontWeightProperty); } set { SetValue(HeaderFontWeightProperty, value); } }

        public static readonly DependencyProperty ContentForegroundProperty = DependencyProperty.Register("ContentForeground", typeof(Brush), typeof(StatCounter));
        public Brush ContentForeground { get { return (Brush)GetValue(ContentForegroundProperty); } set { SetValue(ContentForegroundProperty, value); } }

        public static readonly DependencyProperty ContentFontSizeProperty = DependencyProperty.Register("ContentFontSize", typeof(double?), typeof(StatCounter));
        [TypeConverter(typeof(FontSizeConverter))]
        public double? ContentFontSize { get { return (double?)GetValue(ContentFontSizeProperty); } set { SetValue(ContentFontSizeProperty, value); } }

        public static readonly DependencyProperty ContentAlignmentProperty = DependencyProperty.Register("ContentAlignment", typeof(HorizontalAlignment), typeof(StatCounter), new FrameworkPropertyMetadata(HorizontalAlignment.Left));
        public HorizontalAlignment ContentAlignment { get { return (HorizontalAlignment)GetValue(ContentAlignmentProperty); } set { SetValue(ContentAlignmentProperty, value); } }

        public static readonly DependencyProperty ContentFontWeightProperty = DependencyProperty.Register("ContentFontWeight", typeof(FontWeight?), typeof(StatCounter));
        public FontWeight? ContentFontWeight { get { return (FontWeight?)GetValue(ContentFontWeightProperty); } set { SetValue(ContentFontWeightProperty, value); } }

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register("Mode", typeof(StatCounterMode), typeof(StatCounter), new FrameworkPropertyMetadata(StatCounterMode.Auto));
        public StatCounterMode Mode { get { return (StatCounterMode)GetValue(ModeProperty); } set { SetValue(ModeProperty, value); } }

        static StatCounter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StatCounter), new FrameworkPropertyMetadata(typeof(StatCounter)));
            IsTabStopProperty.OverrideMetadata(typeof(StatCounter), new FrameworkPropertyMetadata(false));
        }

        public StatCounter()
        {
            FacadeModel.UpdateAll(Model, this);
        }

        public override void OnApplyTemplate()
        {
            FacadeModel.Wrap(Model, GetTemplateChild("Root"));
            base.OnApplyTemplate();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            FacadeModel.Update(Model, this, args);
        }
    }
}
