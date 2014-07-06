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
        public object StatCounter { get { return ViewModel(() => new StatCounterSampleViewModel()); } }
        public object PropertySheet { get { return ViewModel(() => new PropertySheetSampleViewModel()); } }
    }
}
