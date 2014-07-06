using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JungleControls
{
    public class FacadeContentControl : ContentControl, IFacadeControl
    {
        public Type FacadeType { get; private set; }

        static FacadeContentControl() { FacadeHelpers.Initialize<FacadeContentControl>(DefaultStyleKeyProperty); }
        public FacadeContentControl() { FacadeType = FacadeHelpers.GetFacadeType(this); }

        public DependencyObject GetFacadeChild(string name) { return GetTemplateChild(name); }
        public override void OnApplyTemplate() { FacadeHelpers.ApplyTemplate(this); }
    }
}
