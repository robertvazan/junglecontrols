// Part of JungleControls: https://blog.machinezoo.com/junglecontrols-free-wpf-controls-for
﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace JungleControls
{
    public class LazyControl : ContentControl
    {
        ContentPresenter LazyChild;

        static LazyControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LazyControl), new FrameworkPropertyMetadata(typeof(LazyControl)));
            IsTabStopProperty.OverrideMetadata(typeof(LazyControl), new FrameworkPropertyMetadata(false));
        }

        public override void OnApplyTemplate()
        {
            LazyChild = GetTemplateChild("Presenter") as ContentPresenter;
            if (LazyChild != null)
                LayoutUpdated += HandleLayoutUpdated;
            base.OnApplyTemplate();
        }

        FrameworkElement FindContainer()
        {
            for (var ancestor = VisualTreeHelper.GetParent(this); ancestor != null; ancestor = VisualTreeHelper.GetParent(ancestor))
                if (ancestor is ScrollContentPresenter)
                    return ancestor as FrameworkElement;
            return null;
        }

        bool IsInView()
        {
            var container = FindContainer();
            if (container != null)
            {
                var transform = TransformToAncestor(container);
                var mappedRect = transform.TransformBounds(new Rect(0.0, 0.0, ActualWidth, ActualHeight));
                var containerRect = new Rect(0.0, 0.0, container.ActualWidth, container.ActualHeight);
                return containerRect.IntersectsWith(mappedRect);
            }
            else
                return false;
        }

        void Materialize()
        {
            LazyChild.Content = Content;
            LazyChild.ContentTemplate = ContentTemplate;
        }

        void HandleLayoutUpdated(object sender, EventArgs args)
        {
            if (IsInView())
            {
                LayoutUpdated -= HandleLayoutUpdated;
                Materialize();
            }
        }
    }
}
