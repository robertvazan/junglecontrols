// Part of JungleControls: https://blog.machinezoo.com/junglecontrols-free-wpf-controls-for
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JungleControls
{
    class VisibilityEx
    {
        public static Visibility If(bool condition) { return condition ? Visibility.Visible : Visibility.Collapsed; }
    }
}
