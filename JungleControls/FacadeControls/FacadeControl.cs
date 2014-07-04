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
        public DependencyProperty FacadeStyleKey { get { return DefaultStyleKeyProperty; } }

        public FacadeControl(Type type)
        {
            FacadeType = type;
            FacadeHelpers.Initialize(this);
        }

        public DependencyObject GetFacadeChild(string name) { return GetTemplateChild(name); }
        public override void OnApplyTemplate() { FacadeHelpers.ApplyTemplate(this); }
    }
}
