using Assisticant.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JungleControls
{
    public class PrecisePopupPlacements : IList<PrecisePopupPlacement>, IList
    {
        readonly PrecisePopup Owner;
        internal readonly ObservableList<PrecisePopupPlacement> InnerList = new ObservableList<PrecisePopupPlacement>();

        public int Count { get { return InnerList.Count; } }
        public bool IsReadOnly { get { return false; } }
        bool ICollection.IsSynchronized { get { return false; } }
        object ICollection.SyncRoot { get { return this; } }
        bool IList.IsFixedSize { get { return false; } }
        public PrecisePopupPlacement this[int index] { get { return InnerList[index]; } set { Owner.RemovePlacement(InnerList[index]); InnerList[index] = value; Owner.AddPlacement(value); } }
        object IList.this[int index] { get { return this[index]; } set { this[index] = (PrecisePopupPlacement)value; } }

        public PrecisePopupPlacements(PrecisePopup owner) { Owner = owner; }

        public void Add(PrecisePopupPlacement item) { InnerList.Add(item); Owner.AddPlacement(item); }
        int IList.Add(object value) { Add((PrecisePopupPlacement)value); return Count - 1; }
        public void Clear() { foreach (var item in InnerList) Owner.RemovePlacement(item); InnerList.Clear(); }
        public bool Contains(PrecisePopupPlacement item) { return InnerList.Contains(item); }
        bool IList.Contains(object value) { return Contains((PrecisePopupPlacement)value); }
        public int IndexOf(PrecisePopupPlacement item) { return InnerList.IndexOf(item); }
        int IList.IndexOf(object value) { return IndexOf((PrecisePopupPlacement)value); }
        public void Insert(int index, PrecisePopupPlacement item) { InnerList.Insert(index, item); Owner.AddPlacement(item); }
        void IList.Insert(int index, object value) { Insert(index, (PrecisePopupPlacement)value); }
        public void RemoveAt(int index) { Owner.RemovePlacement(InnerList[index]); InnerList.RemoveAt(index); }
        public bool Remove(PrecisePopupPlacement item) { Owner.RemovePlacement(item); return InnerList.Remove(item); }
        void IList.Remove(object value) { Remove((PrecisePopupPlacement)value); }
        public void CopyTo(PrecisePopupPlacement[] array, int arrayIndex) { InnerList.CopyTo(array, arrayIndex); }
        void ICollection.CopyTo(Array array, int index) { CopyTo((PrecisePopupPlacement[])array, index); }
        public IEnumerator<PrecisePopupPlacement> GetEnumerator() { return InnerList.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
    }
}
