using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace JungleControls
{
    public class PropertySheet : ItemsControl
    {
        static readonly FacadeType FacadeType;
        readonly FacadeInstance FacadeInstance;
        readonly PropertySheetModel Model;

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.RegisterAttached("Header", typeof(object), typeof(PropertySheet), new FrameworkPropertyMetadata(HandleHeaderChanged));
        public static void SetHeader(FrameworkElement element, object value) { element.SetValue(HeaderProperty, value); }
        public static object GetHeader(FrameworkElement element) { return element.GetValue(HeaderProperty); }

        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(PropertySheet), new FacadePropertyMetadata());
        public DataTemplate HeaderTemplate { get { return (DataTemplate)GetValue(HeaderTemplateProperty); } set { SetValue(HeaderTemplateProperty, value); } }

        public static readonly DependencyProperty HeaderMarginProperty = DependencyProperty.Register("HeaderMargin", typeof(Thickness), typeof(PropertySheet));
        public Thickness HeaderMargin { get { return (Thickness)GetValue(HeaderMarginProperty); } set { SetValue(HeaderMarginProperty, value); } }

        public static readonly DependencyProperty HeaderForegroundProperty = DependencyProperty.Register("HeaderForeground", typeof(Brush), typeof(PropertySheet), new FacadePropertyMetadata());
        public Brush HeaderForeground { get { return (Brush)GetValue(HeaderForegroundProperty); } set { SetValue(HeaderForegroundProperty, value); } }

        public static readonly DependencyProperty HeaderFontWeightProperty = DependencyProperty.Register("HeaderFontWeight", typeof(FontWeight?), typeof(PropertySheet), new FacadePropertyMetadata());
        public FontWeight? HeaderFontWeight { get { return (FontWeight?)GetValue(HeaderFontWeightProperty); } set { SetValue(HeaderFontWeightProperty, value); } }

        public static readonly DependencyProperty CellMarginProperty = DependencyProperty.Register("CellMargin", typeof(Thickness), typeof(PropertySheet));
        public Thickness CellMargin { get { return (Thickness)GetValue(CellMarginProperty); } set { SetValue(CellMarginProperty, value); } }

        static PropertySheet()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertySheet), new FrameworkPropertyMetadata(typeof(PropertySheet)));
            IsTabStopProperty.OverrideMetadata(typeof(PropertySheet), new FrameworkPropertyMetadata(false));
            FacadeType = new FacadeType(typeof(PropertySheet), typeof(PropertySheetModel), obj => ((PropertySheet)obj).FacadeInstance);
        }

        public PropertySheet()
        {
            FacadeInstance = new FacadeInstance(FacadeType, this);
            Model = (PropertySheetModel)FacadeInstance.GetModel();
        }

        public override void OnApplyTemplate()
        {
            FacadeInstance.ApplyModel(GetTemplateChild("Root"));
            base.OnApplyTemplate();
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            Model.Items.Clear();
            foreach (var item in Items)
                Model.Items.Add(Tuple.Create(item, item is FrameworkElement ? GetHeader(item as FrameworkElement) : null));
            base.OnItemsChanged(e);
        }

        static void HandleHeaderChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var element = sender as FrameworkElement;
            if (element == null)
                return;
            var sheet = element.Parent as PropertySheet;
            if (sheet == null)
                return;
            var items = sheet.Model.Items;
            for (int i = 0; i < items.Count; ++i)
                if (items[i].Item1 == element)
                    items[i] = Tuple.Create((object)element, GetHeader(element));
        }
    }
}
