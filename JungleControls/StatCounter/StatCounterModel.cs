// Part of JungleControls: https://blog.machinezoo.com/junglecontrols-free-wpf-controls-for
﻿﻿using Assisticant;
using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace JungleControls
{
    class StatCounterModel
    {
        public readonly Observable<StatCounterHeaderPosition> HeaderPosition = new Observable<StatCounterHeaderPosition>();
        public readonly Observable<double> Spacing = new Observable<double>();
        public readonly Observable<Brush> Foreground = new Observable<Brush>();
        public readonly Observable<Brush> HeaderForeground = new Observable<Brush>();
        public readonly Observable<Brush> ContentForeground = new Observable<Brush>();
        public readonly Observable<double> FontSize = new Observable<double>();
        public readonly Observable<double?> HeaderFontSize = new Observable<double?>();
        public readonly Observable<double?> ContentFontSize = new Observable<double?>();
        public readonly Observable<FontWeight> FontWeight = new Observable<FontWeight>();
        public readonly Observable<FontWeight?> HeaderFontWeight = new Observable<FontWeight?>();
        public readonly Observable<FontWeight?> ContentFontWeight = new Observable<FontWeight?>();
        public readonly Observable<object> Content = new Observable<object>();
        public readonly Observable<string> ContentStringFormat = new Observable<string>();
        public readonly Observable<StatCounterMode> Mode = new Observable<StatCounterMode>();

        bool IsTop { get { return HeaderPosition.Value == StatCounterHeaderPosition.Top; } }
        bool IsBottom { get { return HeaderPosition.Value == StatCounterHeaderPosition.Bottom; } }
        public int HeaderRow { get { return IsTop ? 0 : 1; } }
        public int ContentRow { get { return 1 - HeaderRow; } }
        public double HalfSpacing { get { return Spacing.Value / 2; } }
        public Thickness HeaderMargin { get { return new Thickness(0, IsBottom ? HalfSpacing : 0, 0, IsTop ? HalfSpacing : 0); } }
        public Thickness ContentMargin { get { return new Thickness(0, IsTop ? HalfSpacing : 0, 0, IsBottom ? HalfSpacing : 0); } }
        public Brush EffectiveHeaderForeground { get { return HeaderForeground.Value ?? Foreground.Value; } }
        public Brush EffectiveContentForeground { get { return ContentForeground.Value ?? Foreground.Value; } }
        public double EffectiveHeaderFontSize { get { return HeaderFontSize.Value ?? FontSize.Value; } }
        public double EffectiveContentFontSize { get { return ContentFontSize.Value ?? 2 * FontSize.Value; } }
        public FontWeight EffectiveHeaderFontWeight { get { return HeaderFontWeight.Value ?? FontWeight.Value; } }
        public FontWeight EffectiveContentFontWeight { get { return ContentFontWeight.Value ?? FontWeight.Value; } }

        public string FormattedContent
        {
            get
            {
                var value = Content.Value;
                if (ContentStringFormat.Value != null)
                    return String.Format(ContentStringFormat.Value, value);
                if (value == null)
                    return "";
                var mode = Mode.Value;
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
    }
}
