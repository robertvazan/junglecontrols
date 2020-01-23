// Part of JungleControls: https://blog.machinezoo.com/junglecontrols-free-wpf-controls-for
﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JungleControls
{
    class PrecisePopupPanel : Panel
    {
        internal PrecisePopupModel Model;

        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement element in InternalChildren)
            {
                if (Model != null)
                {
                    element.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                    if (Math.Abs(Model.PopupWidth.Value - element.DesiredSize.Width) > 0.001 || Math.Abs(Model.PopupHeight.Value - element.DesiredSize.Height) > 0.001)
                    {
                        Model.PopupWidth.Value = element.DesiredSize.Width;
                        Model.PopupHeight.Value = element.DesiredSize.Height;
                    }
                }
                element.Measure(availableSize);
                return element.DesiredSize;
            }
            return Size.Empty;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (UIElement element in InternalChildren)
                element.Arrange(new Rect(new Point(), finalSize));
            return finalSize;
        }
    }
}
