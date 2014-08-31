using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateControls.XAML;

namespace TestApp
{
    class DesignViewModelLocator : ViewModelLocatorBase
    {
        public object StatCounter { get { return ViewModel(() => new StatCounterModel()); } }
        public object PropertySheet { get { return ViewModel(() => new PropertySheetModel()); } }
        public object DelayedContentControl { get { return ViewModel(() => new DelayedContentControlModel()); } }
        public object LazyControl { get { return ViewModel(() => new LazyControlModel()); } }
        public object ExposeControl { get { return ViewModel(() => new ExposeControlModel()); } }
    }
}
