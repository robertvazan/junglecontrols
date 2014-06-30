using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace ControlJungle
{
    [ContentProperty("Items")]
    public partial class PropertySheet : UserControl
    {
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(ObservableCollection<FrameworkElement>), typeof(PropertySheet), new UIPropertyMetadata(new ObservableCollection<FrameworkElement>()));
        public ObservableCollection<FrameworkElement> Items { get { return (ObservableCollection<FrameworkElement>)GetValue(ItemsProperty); } set { SetValue(ItemsProperty, value); } }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.RegisterAttached("Header", typeof(object), typeof(PropertySheet));
        public static void SetHeader(UIElement element, object value) { element.SetValue(HeaderProperty, value); }
        public static object GetHeader(UIElement element) { return element.GetValue(HeaderProperty); }

        readonly List<Action> HeaderHandlers = new List<Action>();

        public PropertySheet()
        {
            InitializeComponent();
            Items.CollectionChanged += (s, a) => RefreshList();
        }

        void RefreshList()
        {
            foreach (var item in Items.Skip(SheetGrid.RowDefinitions.Count))
            {
                int offset = SheetGrid.RowDefinitions.Count;
                SheetGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                var label = new ContentPresenter() { Content = GetHeader(item) };
                var content = new ContentPresenter() { Content = item };
                Grid.SetColumn(content, 1);
                Grid.SetRow(content, offset);
                Grid.SetRow(label, offset);
                SheetGrid.Children.Add(label);
                SheetGrid.Children.Add(content);
                HeaderHandlers.Add(() => label.Content = GetHeader(item));
                DependencyPropertyDescriptor.FromProperty(HeaderProperty, item.GetType()).AddValueChanged(item, (s, a) => RefreshHeaders());
            }
        }

        void RefreshHeaders()
        {
            foreach (var handler in HeaderHandlers)
                handler();
        }
    }
}
