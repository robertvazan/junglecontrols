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
        readonly Observable<bool> IsSecondModelValue = new Observable<bool>();

        public bool IsSecondModel { get { return IsSecondModelValue.Value; } set { IsSecondModelValue.Value = value; } }
        public object Content { get { return !IsSecondModelValue.Value ? (object)new DelayedContentControlFirst() : new DelayedContentControlSecond(); } }
    }

    class DelayedContentControlFirst { public string FirstContent { get { return "First content"; } } }
    class DelayedContentControlSecond { public string SecondContent { get { return "Second content"; } } }
}
