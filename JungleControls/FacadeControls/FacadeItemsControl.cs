using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JungleControls
{
    public class FacadeItemsControl : ItemsControl, IFacadeControl
    {
        public Type FacadeType { get; private set; }

        static FacadeItemsControl() { FacadeHelpers.Initialize<FacadeItemsControl>(DefaultStyleKeyProperty); }
        public FacadeItemsControl(Type type) { FacadeType = type; }

        public DependencyObject GetFacadeChild(string name) { return GetTemplateChild(name); }
        public override void OnApplyTemplate() { FacadeHelpers.ApplyTemplate(this); }
    }
}
