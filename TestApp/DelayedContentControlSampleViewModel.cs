using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateControls.Fields;

namespace TestApp
{
    class DelayedContentControlSampleViewModel
    {
        readonly Independent<bool> IsSecondModelValue = new Independent<bool>();

        public bool IsSecondModel { get { return IsSecondModelValue.Value; } set { IsSecondModelValue.Value = value; } }
        public object Content { get { return !IsSecondModelValue.Value ? (object)new DelayedContentControlFirstViewModel() : new DelayedContentControlSecondViewModel(); } }
    }

    class DelayedContentControlFirstViewModel { public string FirstContent { get { return "First content"; } } }
    class DelayedContentControlSecondViewModel { public string SecondContent { get { return "Second content"; } } }
}
