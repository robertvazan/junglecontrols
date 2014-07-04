using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JungleControls
{
    public class FacadeControl : Control, IFacadeControl
    {
        public Type FacadeType { get; private set; }

        static FacadeControl() { FacadeHelpers.Initialize<FacadeControl>(DefaultStyleKeyProperty); }
        public FacadeControl(Type type) { FacadeType = type; }

        public DependencyObject GetFacadeChild(string name) { return GetTemplateChild(name); }
        public override void OnApplyTemplate() { FacadeHelpers.ApplyTemplate(this); }
    }
}
