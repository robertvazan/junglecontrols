using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using UpdateControls.Fields;

namespace JungleControls
{
    class StatCounterViewModel
    {
        readonly Independent<Brush> ForegroundIndependent = new Independent<Brush>();
        readonly Independent<double> FontSizeIndependent = new Independent<double>();
        readonly Independent<FontWeight> FontWeightIndependent = new Independent<FontWeight>();
        readonly Independent<StatCounterHeaderPosition> HeaderPositionIndependent = new Independent<StatCounterHeaderPosition>();
        readonly Independent<Brush> HeaderForegroundIndependent = new Independent<Brush>();
        readonly Independent<double?> HeaderFontSizeIndependent = new Independent<double?>();
        readonly Independent<FontWeight?> HeaderFontWeightIndependent = new Independent<FontWeight?>();
        readonly Independent<double> SpacingIndependent = new Independent<double>();
        readonly Independent<object> ContentIndependent = new Independent<object>();
        readonly Independent<string> ContentStringFormatIndependent = new Independent<string>();
        readonly Independent<Brush> ContentForegroundIndependent = new Independent<Brush>();
        readonly Independent<double?> ContentFontSizeIndependent = new Independent<double?>();
        readonly Independent<FontWeight?> ContentFontWeightIndependent = new Independent<FontWeight?>();
        readonly Independent<StatCounterMode> ModeIndependent = new Independent<StatCounterMode>();

        public StatCounter Control { get; private set; }
        public bool IsTop { get { return HeaderPositionIndependent.Value == StatCounterHeaderPosition.Top; } }
        public bool IsBottom { get { return HeaderPositionIndependent.Value == StatCounterHeaderPosition.Bottom; } }
        public int HeaderRow { get { return IsTop ? 0 : 1; } }
        public int ContentRow { get { return 1 - HeaderRow; } }
        public double HalfSpacing { get { return SpacingIndependent.Value / 2; } }
        public Thickness HeaderMargin { get { return new Thickness(0, IsBottom ? HalfSpacing : 0, 0, IsTop ? HalfSpacing : 0); } }
        public Thickness ContentMargin { get { return new Thickness(0, IsTop ? HalfSpacing : 0, 0, IsBottom ? HalfSpacing : 0); } }
        public Brush HeaderForeground { get { return HeaderForegroundIndependent.Value ?? ForegroundIndependent.Value; } }
        public Brush ContentForeground { get { return ContentForegroundIndependent.Value ?? ForegroundIndependent.Value; } }
        public double HeaderFontSize { get { return HeaderFontSizeIndependent.Value ?? FontSizeIndependent.Value; } }
        public double ContentFontSize { get { return ContentFontSizeIndependent.Value ?? 2 * FontSizeIndependent.Value; } }
        public FontWeight HeaderFontWeight { get { return HeaderFontWeightIndependent.Value ?? FontWeightIndependent.Value; } }
        public FontWeight ContentFontWeight { get { return ContentFontWeightIndependent.Value ?? FontWeightIndependent.Value; } }

        public string Content
        {
            get
            {
                var value = ContentIndependent.Value;
                if (ContentStringFormatIndependent.Value != null)
                    return String.Format("{0:" + ContentStringFormatIndependent.Value + "}", value);
                if (value == null)
                    return "";
                var mode = ModeIndependent.Value;
                if (mode == StatCounterMode.Auto)
                {
                    if (value is int || value is long || value is short || value is byte || value is sbyte || value is uint || value is ulong || value is ushort)
                        mode = StatCounterMode.Integer;
                    else if (value is double || value is float || value is decimal)
                    {
                        var fp = Convert.ToDouble(value);
                        if (fp > 0 && fp < 1)
                            mode = StatCounterMode.Percent;
                        else
                            mode = StatCounterMode.Float;
                    }
                    else if (value is TimeSpan)
                        mode = StatCounterMode.TimeSpan;
                    else
                        mode = StatCounterMode.String;
                }
                switch (mode)
                {
                    case StatCounterMode.Integer:
                        return String.Format("{0:N0}", value);
                    case StatCounterMode.Float:
                        return Convert.ToDouble(value).ToString("#,##0.##");
                    case StatCounterMode.Percent:
                        var fp = Convert.ToDouble(value);
                        if (fp >= 0.1 && fp <= 1 || fp == 0)
                            return fp.ToString("P0");
                        if (fp >= 0.01 && fp < 0.1)
                            return fp.ToString("P1");
                        return fp.ToString("P");
                    case StatCounterMode.TimeSpan:
                        var ts = (TimeSpan)value;
                        string sign = ts < TimeSpan.Zero ? "-" : "";
                        ts = ts < TimeSpan.Zero ? -ts : ts;
                        string positive;
                        if (ts >= TimeSpan.FromDays(10) || ts >= TimeSpan.FromDays(1) && ts.Hours == 0)
                            positive = String.Format("{0} d", ts.Days);
                        else if (ts >= TimeSpan.FromDays(1))
                            positive = String.Format("{0} d {1} hr", ts.Days, ts.Hours);
                        else if (ts >= TimeSpan.FromHours(1))
                            positive = ts.Minutes == 0 ? String.Format("{0} hr", ts.Hours) : String.Format("{0} hr {1} min", ts.Hours, ts.Minutes);
                        else if (ts >= TimeSpan.FromMinutes(1))
                            positive = ts.Seconds == 0 ? String.Format("{0} min", ts.Minutes) : String.Format("{0} min {1} s", ts.Minutes, ts.Seconds);
                        else if (ts >= TimeSpan.FromSeconds(10))
                            positive = String.Format("{0} s", ts.Seconds);
                        else if (ts >= TimeSpan.FromSeconds(1))
                            positive = String.Format("{0:0.#} s", ts.TotalSeconds);
                        else if (ts > TimeSpan.Zero)
                            positive = String.Format("{0} ms", ts.Milliseconds);
                        else
                            positive = "0";
                        return sign + positive;
                    default:
                        return value.ToString();
                }
            }
        }

        public StatCounterViewModel(StatCounter control)
        {
            Control = control;
            FacadeMapper.LiftAll(control, this);
        }
    }
}
