// Part of JungleControls: https://blog.machinezoo.com/junglecontrols-free-wpf-controls-for
using Assisticant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class ViewModelLocator : ViewModelLocatorBase
    {
        public object StatCounter { get { return ViewModel(() => new StatCounterModel()); } }
        public object PropertySheet { get { return ViewModel(() => new PropertySheetModel()); } }
        public object DelayedContentControl { get { return ViewModel(() => new DelayedContentControlModel()); } }
        public object LazyControl { get { return ViewModel(() => new LazyControlModel()); } }
        public object ExposeControl { get { return ViewModel(() => new ExposeControlModel()); } }
        public object PrecisePopup { get { return ViewModel(() => new PrecisePopupModel()); } }
        public object DataPipe { get { return ViewModel(() => new DataPipeModel()); } }
        public object ColoredImage { get { return ViewModel(() => new ColoredImageModel()); } }
    }
}
