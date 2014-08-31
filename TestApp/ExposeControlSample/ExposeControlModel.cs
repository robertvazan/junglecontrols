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
        public ICommand ClickMe { get { return MakeCommand.Do(DoClickMe); } }

        public void DoClickMe()
        {
            ScrollMeAway.BringIntoView();
        }
    }
}
