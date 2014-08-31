using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class LazyControlGroup
    {
        public readonly LazyControlModel Sample;
        public readonly LazyControlRow[] Rows;
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
                return String.Format("Group {0}", Id);
            }
        }
        public int HeightHint { get { return 20 * Rows.Length + 40; } }

        public LazyControlGroup(LazyControlModel sample, int id, Random random)
        {
            Sample = sample;
            Id = id;
            Rows = Enumerable.Range(1, random.Next(1, 100)).Select(n => new LazyControlRow(this, n, random)).ToArray();
        }
    }
}
