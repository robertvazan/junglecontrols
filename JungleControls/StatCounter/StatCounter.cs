// Part of JungleControls: https://blog.machinezoo.com/junglecontrols-free-wpf-controls-for
using Assisticant;
using Assisticant.Facades;
using Assisticant.Fields;
using AutoDependencyPropertyMarker;
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
    [AutoDependencyProperty]
    public class StatCounter : HeaderedContentControl
    {
        readonly StatCounterModel Model = new StatCounterModel();

        public StatCounterHeaderPosition HeaderPosition { get; set; }
        public double Spacing { get; set; }
        public Brush HeaderForeground { get; set; }
        public HorizontalAlignment HeaderAlignment { get; set; }
        public FontWeight? HeaderFontWeight { get; set; }
        public Brush ContentForeground { get; set; }
        public HorizontalAlignment ContentAlignment { get; set; }
        public FontWeight? ContentFontWeight { get; set; }
        public StatCounterMode Mode { get; set; }

        public static readonly DependencyProperty HeaderFontSizeProperty = DependencyProperty.Register("HeaderFontSize", typeof(double?), typeof(StatCounter));
        [TypeConverter(typeof(FontSizeConverter))]
        public double? HeaderFontSize { get { return (double?)GetValue(HeaderFontSizeProperty); } set { SetValue(HeaderFontSizeProperty, value); } }

        public static readonly DependencyProperty ContentFontSizeProperty = DependencyProperty.Register("ContentFontSize", typeof(double?), typeof(StatCounter));
        [TypeConverter(typeof(FontSizeConverter))]
        public double? ContentFontSize { get { return (double?)GetValue(ContentFontSizeProperty); } set { SetValue(ContentFontSizeProperty, value); } }

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
