using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ControlJungle
{
    public class PropertySheet : ItemsControl
    {
        static PropertySheet()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertySheet), new FrameworkPropertyMetadata(typeof(PropertySheet)));
        }
    }
}
