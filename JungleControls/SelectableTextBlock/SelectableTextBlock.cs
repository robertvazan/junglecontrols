using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace JungleControls
{
    public class SelectableTextBlock : Control
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(SelectableTextBlock), new FrameworkPropertyMetadata(""));
        public string Text { get { return (string)GetValue(TextProperty); } set { SetValue(TextProperty, value); } }

        static SelectableTextBlock() { ControlFacade.InitializeFacade<SelectableTextBlock>(); }
        public override void OnApplyTemplate() { ControlFacade.TemplateFacade<SelectableTextBlockView>(GetTemplateChild("InternalPresenter"), this); }
    }
}
