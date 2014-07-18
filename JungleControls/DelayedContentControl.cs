using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace JungleControls
{
    public class DelayedContentControl : ContentControl
    {
        ContentPresenter Presenter;
        bool IsDirty;

        static DelayedContentControl()
        {
            var resources = (ResourceDictionary)Application.LoadComponent(new Uri("/JungleControls;component/UniversalTemplate.xaml", UriKind.Relative));
            Control.TemplateProperty.OverrideMetadata(typeof(DelayedContentControl), new FrameworkPropertyMetadata((ControlTemplate)resources["JungleControlsUniversalTemplate"]));
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DelayedContentControl), new FrameworkPropertyMetadata((object)null));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Presenter = GetTemplateChild("InternalPresenter") as ContentPresenter;
            Invalidate();
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            Invalidate();
        }

        protected override void OnContentTemplateChanged(DataTemplate oldContentTemplate, DataTemplate newContentTemplate)
        {
            base.OnContentTemplateChanged(oldContentTemplate, newContentTemplate);
            Invalidate();
        }

        protected override void OnContentTemplateSelectorChanged(DataTemplateSelector oldContentTemplateSelector, DataTemplateSelector newContentTemplateSelector)
        {
            base.OnContentTemplateSelectorChanged(oldContentTemplateSelector, newContentTemplateSelector);
            Invalidate();
        }

        protected override void OnContentStringFormatChanged(string oldContentStringFormat, string newContentStringFormat)
        {
            base.OnContentStringFormatChanged(oldContentStringFormat, newContentStringFormat);
            Invalidate();
        }

        void Invalidate()
        {
            if (!IsDirty)
            {
                IsDirty = true;
                Dispatcher.BeginInvoke(Refresh, DispatcherPriority.DataBind);
            }
        }

        void Refresh()
        {
            IsDirty = false;
            if (Presenter != null)
            {
                bool templateChanging = ContentTemplate != Presenter.ContentTemplate;
                if (templateChanging)
                    Presenter.ContentTemplate = null;
                bool selectorChanging = ContentTemplateSelector != Presenter.ContentTemplateSelector;
                if (selectorChanging)
                    Presenter.ContentTemplateSelector = null;
                bool formatChanging = ContentStringFormat != Presenter.ContentStringFormat;
                if (formatChanging)
                    Presenter.ContentStringFormat = null;
                Presenter.Content = Content;
                if (formatChanging)
                    Presenter.ContentStringFormat = ContentStringFormat;
                if (selectorChanging)
                    Presenter.ContentTemplateSelector = ContentTemplateSelector;
                if (templateChanging)
                    Presenter.ContentTemplate = ContentTemplate;
            }
        }
    }
}
