using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UpdateControls.XAML;

namespace JungleControls
{
    static class FacadeHelpers
    {
        public static void Initialize<T>(DependencyProperty styleKey)
        {
            var resources = (ResourceDictionary)Application.LoadComponent(new Uri("/JungleControls;component/UniversalTemplate.xaml", UriKind.Relative));
            Control.TemplateProperty.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata((ControlTemplate)resources["JungleControlsUniversalTemplate"]));
            styleKey.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata((object)null));
        }

        public static void ApplyTemplate(IFacadeControl control)
        {
            var presenter = control.GetFacadeChild("InternalPresenter") as ContentPresenter;
            if (presenter != null)
            {
                var view = (FrameworkElement)Activator.CreateInstance(control.FacadeType.Assembly.GetType(control.FacadeType.FullName + "View"));
                var modelType = control.FacadeType.Assembly.GetType(control.FacadeType.FullName + "ViewModel");
                if (modelType != null)
                    view.DataContext = ForView.Wrap(Activator.CreateInstance(modelType, control));
                else
                    view.DataContext = control;
                presenter.Content = view;
            }
        }
    }
}
