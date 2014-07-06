using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JungleControls
{
    public class FacadeHeaderedContentControl : HeaderedContentControl, IFacadeControl
    {
        public Type FacadeType { get; private set; }

        static FacadeHeaderedContentControl() { FacadeHelpers.Initialize<FacadeHeaderedContentControl>(DefaultStyleKeyProperty); }
        public FacadeHeaderedContentControl(Type type) { FacadeType = type; }

        public DependencyObject GetFacadeChild(string name) { return GetTemplateChild(name); }
        public override void OnApplyTemplate() { FacadeHelpers.ApplyTemplate(this); }
    }
}
