// Part of JungleControls: https://blog.machinezoo.com/junglecontrols-free-wpf-controls-for
using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class DelayedContentControlModel
    {
        public readonly Observable<bool> IsSecondModel = new Observable<bool>();

        public object Content { get { return !IsSecondModel.Value ? (object)new DelayedContentControlFirst() : new DelayedContentControlSecond(); } }
    }

    class DelayedContentControlFirst { public string FirstContent { get { return "First content"; } } }
    class DelayedContentControlSecond { public string SecondContent { get { return "Second content"; } } }
}
