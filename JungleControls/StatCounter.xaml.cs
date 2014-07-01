using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UpdateControls.XAML;

namespace JungleControls
{
    public partial class StatCounter : UserControl
    {
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(string), typeof(StatCounter));
        public string Header { get { return (string)GetValue(HeaderProperty); } set { SetValue(HeaderProperty, value); } }

        public static readonly DependencyProperty HeaderPositionProperty = DependencyProperty.Register("HeaderPosition", typeof(StatCounterHeaderPosition), typeof(StatCounter));
        public StatCounterHeaderPosition HeaderPosition { get { return (StatCounterHeaderPosition)GetValue(HeaderPositionProperty); } set { SetValue(HeaderPositionProperty, value); } }

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(object), typeof(StatCounter));
        public object Data { get { return GetValue(DataProperty); } set { SetValue(DataProperty, value); } }

        public StatCounter()
        {
            InitializeComponent();
            ((FrameworkElement)Content).DataContext = ForView.Wrap(new StatCounterViewModel(this));
        }
    }
}
