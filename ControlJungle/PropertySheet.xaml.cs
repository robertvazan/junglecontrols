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
using UpdateControls.XAML;

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

        readonly PropertySheetViewModel ViewModel = new PropertySheetViewModel();
        readonly List<Action> HeaderHandlers = new List<Action>();

        public PropertySheet()
        {
            InitializeComponent();
            View.DataContext = ForView.Wrap(ViewModel);
            Items.CollectionChanged += (s, a) => RefreshList();
        }

        void RefreshList()
        {
            foreach (var item in Items.Skip(ViewModel.Rows.Count))
            {
                var row = new PropertySheetRowViewModel(ViewModel)
                {
                    Header = GetHeader(item),
                    Content = item
                };
                ViewModel.Rows.Add(row);
                HeaderHandlers.Add(() => row.Header = GetHeader(item));
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
