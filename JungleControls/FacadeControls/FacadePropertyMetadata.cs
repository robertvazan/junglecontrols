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
        internal int Index = -1;

        public FacadePropertyMetadata() : base(FacadeType.NotifyModel) { }
        public FacadePropertyMetadata(object defaultValue) : base(defaultValue, FacadeType.NotifyModel) { }
    }
}
