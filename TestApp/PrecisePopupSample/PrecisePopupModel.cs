using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class PrecisePopupModel
    {
        public readonly Observable<bool> IsOpen = new Observable<bool>();
        public readonly Observable<double> HorizontalOffsetV = new Observable<double>();
        public double HorizontalOffset { get { return HorizontalOffsetV.Value; } set { HorizontalOffsetV.Value = value; } }
        public readonly Observable<double> VerticalOffset = new Observable<double>();

        public void OpenPopup() { IsOpen.Value = true; }
    }
}
