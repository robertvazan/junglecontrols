using Assisticant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace TestApp
{
    class ExposeControlModel
    {
        public TextBlock ScrollMeAway { get; set; }

        public void ClickMe()
        {
            ScrollMeAway.BringIntoView();
        }
    }
}
