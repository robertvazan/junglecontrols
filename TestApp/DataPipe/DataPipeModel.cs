// Part of JungleControls: https://blog.machinezoo.com/junglecontrols-free-wpf-controls-for
using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class DataPipeModel
    {
        public readonly Observable<double> Width = new Observable<double>();
        public readonly Observable<double> Height = new Observable<double>();
    }
}
