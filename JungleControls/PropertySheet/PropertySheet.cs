using Assisticant.Facades;
using AutoDependencyPropertyMarker;
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
    [AutoDependencyProperty]
    public class PropertySheet : ItemsControl
    {
        readonly PropertySheetModel Model = new PropertySheetModel();

        public DataTemplate HeaderTemplate { get; set; }
        public Thickness HeaderMargin { get; set; }
        public Brush HeaderForeground { get; set; }
        public FontWeight? HeaderFontWeight { get; set; }
        public Thickness CellMargin { get; set; }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.RegisterAttached("Header", typeof(object), typeof(PropertySheet), new FrameworkPropertyMetadata(HandleHeaderChanged));
        public static void SetHeader(FrameworkElement element, object value) { element.SetValue(HeaderProperty, value); }
        public static object GetHeader(FrameworkElement element) { return element.GetValue(HeaderProperty); }

        static PropertySheet()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertySheet), new FrameworkPropertyMetadata(typeof(PropertySheet)));
            IsTabStopProperty.OverrideMetadata(typeof(PropertySheet), new FrameworkPropertyMetadata(false));
        }

        public PropertySheet()
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
