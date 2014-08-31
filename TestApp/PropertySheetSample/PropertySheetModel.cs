using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class PropertySheetModel
    {
        readonly Observable<string> InputValue = new Observable<string>("my input");

        public string Input { get { return InputValue.Value; } set { InputValue.Value = value; } }
    }
}
