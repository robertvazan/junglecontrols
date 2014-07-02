using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateControls.Fields;

namespace JungleControls
{
    class StatCounterViewModel
    {
        readonly Independent<string> HeaderIndependent = new Independent<string>();
        readonly Independent<StatCounterHeaderPosition> HeaderPositionIndependent = new Independent<StatCounterHeaderPosition>();
        readonly Independent<object> DataIndependent = new Independent<object>();

        public string Header { get { return HeaderIndependent.Value; } }
        public int HeaderRow { get { return HeaderPositionIndependent.Value == StatCounterHeaderPosition.Top ? 0 : 1; } }
        public string Data { get { return DataIndependent.Value != null ? DataIndependent.Value.ToString() : ""; } }
        public int DataRow { get { return 1 - HeaderRow; } }

        public StatCounterViewModel(StatCounter element)
        {
            ControlFacade.LiftAll(element, this);
        }
    }
}
