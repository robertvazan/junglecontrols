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

namespace JungleControls
{
    public partial class DataPresenter : UserControl
    {
        public static readonly DependencyProperty InnerProperty = DependencyProperty.Register("Inner", typeof(object), typeof(DataPresenter));
        public object Inner { get { return GetValue(InnerProperty); } set { SetValue(InnerProperty, value); } }

        public static readonly DependencyProperty InnerTemplateProperty = DependencyProperty.Register("InnerTemplate", typeof(FrameworkTemplate), typeof(DataPresenter));
        public FrameworkTemplate InnerTemplate { get { return (FrameworkTemplate)GetValue(InnerTemplateProperty); } set { SetValue(InnerTemplateProperty, value); } }

        public static readonly DependencyProperty InnerDataContextProperty = DependencyProperty.Register("InnerDataContext", typeof(object), typeof(DataPresenter));
        public object InnerDataContext { get { return GetValue(InnerDataContextProperty); } set { SetValue(InnerDataContextProperty, value); } }

        public DataPresenter()
        {
            InitializeComponent();
        }
    }
}
