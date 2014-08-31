using Assisticant;
using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JungleControls
{
    public abstract class FacadeProperty
    {
        internal abstract void Invalidate();
    }

    public class FacadeProperty<T> : FacadeProperty
    {
        readonly Observable RefreshTrigger = new Observable();
        readonly Computed<T> Computed;

        public T Value { get { return Computed.Value; } }

        public FacadeProperty(DependencyObject obj, DependencyProperty property)
        {
            Computed = new Computed<T>(() =>
            {
                RefreshTrigger.OnGet();
                return (T)obj.GetValue(property);
            });
        }

        internal override void Invalidate()
        {
            RefreshTrigger.OnSet();
        }
    }
}
