﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateControls.Fields;

namespace TestApp
{
    class LazyControlSampleViewModel
    {
        readonly Independent<int> MaterializedCounter = new Independent<int>();
        public int MaterializedCount { get { return MaterializedCounter.Value; } set { MaterializedCounter.Value = value; } }
        public LazyControlSampleGroup[] Groups { get; set; }
        public int TotalGroups { get { return Groups.Length; } }
        public int TotalRows { get { return Groups.Sum(g => g.Rows.Length); } }
        public int TotalBars { get { return TotalRows * Groups[0].Rows[0].Bars.Length; } }

        public LazyControlSampleViewModel()
        {
            var random = new Random();
            Groups = Enumerable.Range(1, 100).Select(n => new LazyControlSampleGroup(this, n, random)).ToArray();
        }
    }
}
