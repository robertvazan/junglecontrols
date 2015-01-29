using AutoDependencyPropertyMarker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace JungleControls
{
    [AutoDependencyProperty]
    public class ColoredImage : Control
    {
        public ImageSource Source { get; set; }
        public Stretch Stretch { get; set; }

        static ColoredImage() { DefaultStyleKeyProperty.OverrideMetadata(typeof(ColoredImage), new FrameworkPropertyMetadata(typeof(ColoredImage))); }
        public ColoredImage() { Stretch = Stretch.Uniform; }
    }
}
