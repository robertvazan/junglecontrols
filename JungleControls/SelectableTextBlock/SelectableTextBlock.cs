// Part of JungleControls: https://blog.machinezoo.com/junglecontrols-free-wpf-controls-for
﻿﻿using AutoDependencyPropertyMarker;
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
    [AutoDependencyProperty]
    public class SelectableTextBlock : Control
    {
        public TextAlignment TextAlignment { get; set; }
        public TextDecorationCollection TextDecorations { get; set; }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(SelectableTextBlock), new FrameworkPropertyMetadata(""));
        public string Text { get { return (string)GetValue(TextProperty); } set { SetValue(TextProperty, value); } }

        public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register("TextWrapping", typeof(TextWrapping), typeof(SelectableTextBlock), new FrameworkPropertyMetadata(TextWrapping.NoWrap));
        public TextWrapping TextWrapping { get { return (TextWrapping)GetValue(TextWrappingProperty); } set { SetValue(TextWrappingProperty, value); } }

        static SelectableTextBlock()
        {
            IsTabStopProperty.OverrideMetadata(typeof(SelectableTextBlock), new FrameworkPropertyMetadata(false));
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectableTextBlock), new FrameworkPropertyMetadata(typeof(SelectableTextBlock)));
        }

        public SelectableTextBlock()
        {
            TextDecorations = new TextDecorationCollection();
        }
    }
}
