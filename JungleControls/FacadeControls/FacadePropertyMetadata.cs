using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JungleControls
{
    public class FacadePropertyMetadata : FrameworkPropertyMetadata
    {
        public readonly int ObservableIndex;

        public FacadePropertyMetadata(int index, PropertyChangedCallback changeCallback)
            : base(changeCallback)
        {
            ObservableIndex = index;
        }

        public FacadePropertyMetadata(int index, object defaultValue, PropertyChangedCallback changeCallback)
            : base(defaultValue, changeCallback)
        {
            ObservableIndex = index;
        }
    }
}
