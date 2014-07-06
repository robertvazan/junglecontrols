using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JungleControls
{
    public class DataContentControl : FacadeContentControl
    {
        public static readonly DependencyProperty ContentDataContextProperty = DependencyProperty.Register("ContentDataContext", typeof(object), typeof(DataContentControl));
        public object ContentDataContext { get { return GetValue(ContentDataContextProperty); } set { SetValue(ContentDataContextProperty, value); } }
    }
}
