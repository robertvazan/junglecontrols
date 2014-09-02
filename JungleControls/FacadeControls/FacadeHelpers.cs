using Assisticant;
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
        public static void Initialize<T>(DependencyProperty styleKey)
        {
            var resources = (ResourceDictionary)Application.LoadComponent(new Uri("/JungleControls;component/UniversalTemplate.xaml", UriKind.Relative));
            Control.TemplateProperty.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata((ControlTemplate)resources["JungleControlsUniversalTemplate"]));
            styleKey.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata((object)null));
        }
    }
}
