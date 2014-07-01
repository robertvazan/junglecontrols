using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace JungleControls
{
    [ContentProperty("Items")]
    public partial class PropertySheet : UserControl
    {
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.RegisterReadOnly("Items", typeof(ObservableCollection<FrameworkElement>), typeof(PropertySheet), new FrameworkPropertyMetadata(new ObservableCollection<FrameworkElement>())).DependencyProperty;
        public ObservableCollection<FrameworkElement> Items { get { return (ObservableCollection<FrameworkElement>)GetValue(ItemsProperty); } }
        
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.RegisterAttached("Header", typeof(object), typeof(PropertySheet));
        public static void SetHeader(FrameworkElement element, object value) { element.SetValue(HeaderProperty, value); }
        public static object GetHeader(FrameworkElement element) { return element.GetValue(HeaderProperty); }

        public PropertySheet()
        {
            InitializeComponent();
            var vm = new PropertySheetViewModel();
            ControlFacade.Lift(Items, vm.Rows, elem => new PropertySheetRowViewModel(elem));
            ((FrameworkElement)Content).DataContext = vm;
        }
    }
}
