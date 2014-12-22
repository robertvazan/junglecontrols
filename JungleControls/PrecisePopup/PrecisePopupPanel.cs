using System;
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
        protected override Size MeasureOverride(Size availableSize)
        {
            var x = 0.0;
            var y = 0.0;
            foreach (UIElement element in InternalChildren)
            {
                element.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                x = Math.Max(x, element.DesiredSize.Width);
                y = Math.Max(y, element.DesiredSize.Height);
            }
            return new Size(x, y);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (UIElement element in InternalChildren)
                element.Arrange(new Rect(new Point(), DesiredSize));
            return DesiredSize;
        }
    }
}
