using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JungleControls
{
    public class DataContentControl : ContentControl
    {
        public static readonly DependencyProperty ContentDataContextProperty = DependencyProperty.Register("ContentDataContext", typeof(object), typeof(DataContentControl));
        public object ContentDataContext { get { return GetValue(ContentDataContextProperty); } set { SetValue(ContentDataContextProperty, value); } }

        static DataContentControl()
        {
            ControlFacade.InitializeFacade<DataContentControl>(DefaultStyleKeyProperty);
        }

        public override void OnApplyTemplate()
        {
            ControlFacade.TemplateFacade<DataContentControlView>(GetTemplateChild("InternalPresenter"), this);
        }
    }
}
