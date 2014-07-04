using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JungleControls
{
    static class FacadeHelpers
    {
        static HashSet<Type> Initialized = new HashSet<Type>();

        public static void Initialize(IFacadeControl control)
        {
            if (!Initialized.Contains(control.FacadeType))
            {
                Initialized.Add(control.FacadeType);
                var resources = (ResourceDictionary)Application.LoadComponent(new Uri("/JungleControls;component/UniversalTemplate.xaml", UriKind.Relative));
                Control.TemplateProperty.OverrideMetadata(control.FacadeType, new FrameworkPropertyMetadata((ControlTemplate)resources["JungleControlsUniversalTemplate"]));
                control.FacadeStyleKey.OverrideMetadata(control.FacadeType, new FrameworkPropertyMetadata((object)null));
            }
        }

        public static void ApplyTemplate<T>(T control)
            where T : Control, IFacadeControl
        {
            var presenter = control.GetFacadeChild("InternalPresenter") as ContentPresenter;
            if (presenter != null)
            {
                var view = (FrameworkElement)Activator.CreateInstance(control.FacadeType.Assembly.GetType(control.FacadeType.FullName + "View"));
                var modelType = control.FacadeType.Assembly.GetType(control.FacadeType.FullName + "ViewModel");
                if (modelType != null)
                    view.DataContext = Activator.CreateInstance(modelType, control);
                else
                    view.DataContext = control;
                presenter.Content = view;
            }
        }
    }
}
