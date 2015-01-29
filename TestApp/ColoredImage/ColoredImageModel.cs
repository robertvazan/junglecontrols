using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TestApp
{
    class ColoredImageModel
    {
        public readonly Observable<double> Red = new Observable<double>(1);
        public readonly Observable<double> Green = new Observable<double>(1);
        public readonly Observable<double> Blue = new Observable<double>(1);
        public readonly Observable<double> Alpha = new Observable<double>(1);
        public Color Fill { get { return Color.FromScRgb((float)Alpha.Value, (float)Red.Value, (float)Green.Value, (float)Blue.Value); } }
        public IEnumerable<Stretch> StretchOptions { get { return Enum.GetValues(typeof(Stretch)).OfType<Stretch>(); } }
        public readonly Observable<Stretch> SelectedStretch = new Observable<Stretch>(Stretch.Uniform);
    }
}
