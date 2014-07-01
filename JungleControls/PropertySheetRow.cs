using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JungleControls
{
    public class PropertySheetRow : HeaderedContentControl
    {
        static PropertySheetRow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertySheetRow), new FrameworkPropertyMetadata(typeof(PropertySheetRow)));
        }
    }
}
