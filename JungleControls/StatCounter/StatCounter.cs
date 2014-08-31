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
    public class StatCounter : HeaderedContentControl
    {
        static int ObservableCount;
        FacadeProperty[] ModelFields = new FacadeProperty[ObservableCount];
        StatCounterModel Model = new StatCounterModel();

        public static readonly DependencyProperty HeaderPositionProperty = CreateObservable<StatCounterHeaderPosition>("HeaderPosition");
        public StatCounterHeaderPosition HeaderPosition { get { return (StatCounterHeaderPosition)GetValue(HeaderPositionProperty); } set { SetValue(HeaderPositionProperty, value); } }

        public static readonly DependencyProperty SpacingProperty = CreateObservable<double>("Spacing", 0.0);
        public double Spacing { get { return (double)GetValue(SpacingProperty); } set { SetValue(SpacingProperty, value); } }

        public static readonly DependencyProperty HeaderForegroundProperty = CreateObservable<Brush>("HeaderForeground");
        public Brush HeaderForeground { get { return (Brush)GetValue(HeaderForegroundProperty); } set { SetValue(HeaderForegroundProperty, value); } }

        public static readonly DependencyProperty HeaderFontSizeProperty = CreateObservable<double?>("HeaderFontSize");
        [TypeConverter(typeof(FontSizeConverter))]
        public double? HeaderFontSize { get { return (double?)GetValue(HeaderFontSizeProperty); } set { SetValue(HeaderFontSizeProperty, value); } }

        public static readonly DependencyProperty HeaderAlignmentProperty = DependencyProperty.Register("HeaderAlignment", typeof(HorizontalAlignment), typeof(StatCounter), new FrameworkPropertyMetadata(HorizontalAlignment.Left));
        public HorizontalAlignment HeaderAlignment { get { return (HorizontalAlignment)GetValue(HeaderAlignmentProperty); } set { SetValue(HeaderAlignmentProperty, value); } }

        public static readonly DependencyProperty HeaderFontWeightProperty = CreateObservable<FontWeight?>("HeaderFontWeight");
        public FontWeight? HeaderFontWeight { get { return (FontWeight?)GetValue(HeaderFontWeightProperty); } set { SetValue(HeaderFontWeightProperty, value); } }

        public static readonly DependencyProperty ContentForegroundProperty = CreateObservable<Brush>("ContentForeground");
        public Brush ContentForeground { get { return (Brush)GetValue(ContentForegroundProperty); } set { SetValue(ContentForegroundProperty, value); } }

        public static readonly DependencyProperty ContentFontSizeProperty = CreateObservable<double?>("ContentFontSize");
        [TypeConverter(typeof(FontSizeConverter))]
        public double? ContentFontSize { get { return (double?)GetValue(ContentFontSizeProperty); } set { SetValue(ContentFontSizeProperty, value); } }

        public static readonly DependencyProperty ContentAlignmentProperty = DependencyProperty.Register("ContentAlignment", typeof(HorizontalAlignment), typeof(StatCounter), new FrameworkPropertyMetadata(HorizontalAlignment.Left));
        public HorizontalAlignment ContentAlignment { get { return (HorizontalAlignment)GetValue(ContentAlignmentProperty); } set { SetValue(ContentAlignmentProperty, value); } }

        public static readonly DependencyProperty ContentFontWeightProperty = CreateObservable<FontWeight?>("ContentFontWeight");
        public FontWeight? ContentFontWeight { get { return (FontWeight?)GetValue(ContentFontWeightProperty); } set { SetValue(ContentFontWeightProperty, value); } }

        public static readonly DependencyProperty ModeProperty = CreateObservable("Mode", StatCounterMode.Auto);
        public StatCounterMode Mode { get { return (StatCounterMode)GetValue(ModeProperty); } set { SetValue(ModeProperty, value); } }

        static StatCounter()
        {
            IsTabStopProperty.OverrideMetadata(typeof(StatCounter), new FrameworkPropertyMetadata(false));
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StatCounter), new FrameworkPropertyMetadata(typeof(StatCounter)));
            var properties = (from field in typeof(HeaderedContentControl).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                              where field.FieldType == typeof(DependencyProperty) && field.Name.EndsWith("Property")
                              select (DependencyProperty)field.GetValue(null)).ToList();
            var fields = typeof(StatCounterModel).GetFields().Where(f => f.FieldType.IsGenericType && f.FieldType.GetGenericTypeDefinition() == typeof(FacadeProperty<>)).ToList();
            foreach (var property in properties)
            {
                var field = fields.FirstOrDefault(f => f.Name == property.Name);
                if (field != null)
                    property.OverrideMetadata(typeof(StatCounter), new FacadePropertyMetadata(ObservableCount++, NotifyModel));
            }
        }

        public StatCounter()
        {
            var properties = (from field in GetType().GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                              where field.FieldType == typeof(DependencyProperty) && field.Name.EndsWith("Property")
                              select (DependencyProperty)field.GetValue(null)).ToList();
            foreach (var field in Model.GetType().GetFields().Where(f => f.FieldType.IsGenericType && f.FieldType.GetGenericTypeDefinition() == typeof(FacadeProperty<>)))
            {
                var property = properties.FirstOrDefault(p => p.Name == field.Name);
                if (property == null)
                    throw new InvalidOperationException("No property corresponds to FacadeProperty: " + field.Name);
                var metadata = property.GetMetadata(this) as FacadePropertyMetadata;
                if (metadata == null)
                    throw new InvalidOperationException("FacadePropertyMetadata must be used for observable property: " + field.Name);
                if (field.FieldType.GenericTypeArguments[0] != property.PropertyType)
                    throw new InvalidOperationException("Field associated to dependency property has different value type: " + field.Name);
                var computed = Activator.CreateInstance(typeof(FacadeProperty<>).MakeGenericType(property.PropertyType), this, property);
                ModelFields[metadata.ObservableIndex] = (FacadeProperty)computed;
                field.SetValue(Model, computed);
            }
        }

        public override void OnApplyTemplate()
        {
            var root = GetTemplateChild("Root") as FrameworkElement;
            if (root != null)
                root.DataContext = ForView.Wrap(Model);
            base.OnApplyTemplate();
        }

        static void NotifyModel(object sender, DependencyPropertyChangedEventArgs args)
        {
            var self = (StatCounter)sender;
            var metadata = args.Property.GetMetadata(self) as FacadePropertyMetadata;
            var computed = self.ModelFields[metadata.ObservableIndex];
            computed.Invalidate();
        }

        static DependencyProperty CreateObservable<T>(string name)
        {
            return CreateObservable<T>(name, new FacadePropertyMetadata(ObservableCount++, NotifyModel));
        }

        static DependencyProperty CreateObservable<T>(string name, T value)
        {
            return CreateObservable<T>(name, new FacadePropertyMetadata(ObservableCount++, value, NotifyModel));
        }

        static DependencyProperty CreateObservable<T>(string name, FacadePropertyMetadata metadata)
        {
            return DependencyProperty.Register(name, typeof(T), typeof(StatCounter), metadata);
        }
    }
}
