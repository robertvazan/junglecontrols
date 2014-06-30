using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateControls.Fields;

namespace TestApp
{
    class PropertySheetSampleViewModel
    {
        readonly Independent<string> InputValue = new Independent<string>("my input");

        public string Input { get { return InputValue.Value; } set { InputValue.Value = value; } }
    }
}
