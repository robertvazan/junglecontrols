using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UpdateControls.Fields;

namespace JungleControls
{
    class StatCounterViewModel
    {
        readonly Independent<StatCounterHeaderPosition> HeaderPositionIndependent = new Independent<StatCounterHeaderPosition>();
        readonly Independent<double> SpacingIndependent = new Independent<double>();
        readonly Independent<object> ContentIndependent = new Independent<object>();
        readonly Independent<string> ContentStringFormatIndependent = new Independent<string>();

        public StatCounter Control { get; private set; }
        public bool IsTop { get { return HeaderPositionIndependent.Value == StatCounterHeaderPosition.Top; } }
        public bool IsBottom { get { return HeaderPositionIndependent.Value == StatCounterHeaderPosition.Bottom; } }
        public int HeaderRow { get { return IsTop ? 0 : 1; } }
        public int ContentRow { get { return 1 - HeaderRow; } }
        public double HalfSpacing { get { return SpacingIndependent.Value / 2; } }
        public Thickness HeaderMargin { get { return new Thickness(0, IsBottom ? HalfSpacing : 0, 0, IsTop ? HalfSpacing : 0); } }
        public Thickness ContentMargin { get { return new Thickness(0, IsTop ? HalfSpacing : 0, 0, IsBottom ? HalfSpacing : 0); } }

        public string Content
        {
            get
            {
                var value = ContentIndependent.Value;
                if (value == null)
                    return "";
                if (ContentStringFormatIndependent.Value != null)
                    return String.Format("{0:" + ContentStringFormatIndependent.Value + "}", value);
                if (value is int || value is long || value is short || value is byte || value is sbyte || value is uint || value is ulong || value is ushort)
                    return String.Format("{0:N0}", value);
                if (value is double || value is float || value is decimal)
                {
                    var fp = Convert.ToDouble(value);
                    if (fp >= 0.1 && fp <= 1)
                        return fp.ToString("P0");
                    if (fp >= 0.01 && fp < 0.1)
                        return fp.ToString("P1");
                    if (fp > 0 && fp < 0.01)
                        return fp.ToString("P");
                    if (fp == 0)
                        return "0";
                    return fp.ToString("#,##0.##");
                }
                if (value is TimeSpan)
                {
                    var ts = (TimeSpan)value;
                    string sign = ts < TimeSpan.Zero ? "-" : "";
                    ts = ts < TimeSpan.Zero ? -ts : ts;
                    string positive;
                    if (ts >= TimeSpan.FromDays(10) || ts >= TimeSpan.FromDays(1) && ts.Hours == 0)
                        positive = String.Format("{0}d", ts.Days);
                    else if (ts >= TimeSpan.FromDays(1))
                        positive = String.Format("{0}d {1}h", ts.Days, ts.Hours);
                    else if (ts >= TimeSpan.FromHours(1))
                        positive = ts.Minutes == 0 ? String.Format("{0}h", ts.Hours) : String.Format("{0}h {1}m", ts.Hours, ts.Minutes);
                    else if (ts >= TimeSpan.FromMinutes(1))
                        positive = ts.Seconds == 0 ? String.Format("{0}m", ts.Minutes) : String.Format("{0}m {1}s", ts.Minutes, ts.Seconds);
                    else if (ts >= TimeSpan.FromSeconds(10))
                        positive = String.Format("{0}s", ts.Seconds);
                    else if (ts >= TimeSpan.FromSeconds(1))
                        positive = String.Format("{0:0.#}s", ts.TotalSeconds);
                    else if (ts > TimeSpan.Zero)
                        positive = String.Format("{0}ms", ts.Milliseconds);
                    else
                        positive = "0";
                    return sign + positive;
                }
                return value.ToString();
            }
        }

        public StatCounterViewModel(StatCounter control)
        {
            Control = control;
            FacadeMapper.LiftAll(control, this);
        }
    }
}
