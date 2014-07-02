using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class StatCounterSampleViewModel
    {
        public TimeSpan Days { get { return new TimeSpan(15, 12, 0, 0); } }
        public TimeSpan FewDays { get { return new TimeSpan(5, 12, 0, 0); } }
        public TimeSpan Hours { get { return new TimeSpan(16, 25, 0); } }
        public TimeSpan WholeHours { get { return new TimeSpan(16, 0, 0); } }
        public TimeSpan Minutes { get { return new TimeSpan(0, 13, 5); } }
        public TimeSpan WholeMinutes { get { return new TimeSpan(0, 13, 0); } }
        public TimeSpan Seconds { get { return new TimeSpan(0, 0, 50); } }
        public TimeSpan FewSeconds { get { return new TimeSpan(0, 0, 5); } }
        public TimeSpan FractionalSeconds { get { return new TimeSpan(0, 0, 0, 5, 753); } }
        public TimeSpan Milliseconds { get { return new TimeSpan(0, 0, 0, 0, 258); } }
    }
}
