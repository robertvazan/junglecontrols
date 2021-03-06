// Part of JungleControls: https://blog.machinezoo.com/junglecontrols-free-wpf-controls-for
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class LazyControlRow
    {
        public LazyControlModel Sample { get { return Group.Sample; } }
        public readonly LazyControlGroup Group;
        public readonly int[] Bars;
        public readonly int Id;
        bool Seen;
        public string Name
        {
            get
            {
                if (!Seen)
                {
                    Seen = true;
                    ++Sample.MaterializedCount.Value;
                }
                return String.Format("Group {0}, Row {1}", Group.Id, Id);
            }
        }

        public LazyControlRow(LazyControlGroup group, int id, Random random)
        {
            Group = group;
            Id = id;
            Bars = Enumerable.Range(0, 50).Select(n => random.Next(1, 20)).ToArray();
        }
    }
}
