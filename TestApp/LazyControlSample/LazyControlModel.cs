// Part of JungleControls: https://blog.machinezoo.com/junglecontrols-free-wpf-controls-for
using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class LazyControlModel
    {
        public readonly Observable<int> MaterializedCount = new Observable<int>();
        public readonly LazyControlGroup[] Groups;
        public int TotalGroups { get { return Groups.Length; } }
        public int TotalRows { get { return Groups.Sum(g => g.Rows.Length); } }
        public int TotalBars { get { return TotalRows * Groups[0].Rows[0].Bars.Length; } }

        public LazyControlModel()
        {
            var random = new Random();
            Groups = Enumerable.Range(1, 100).Select(n => new LazyControlGroup(this, n, random)).ToArray();
        }
    }
}
