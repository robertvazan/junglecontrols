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
        protected readonly Observable RefreshTrigger = new Observable();

        internal void Invalidate() { RefreshTrigger.OnSet(); }
    }

    public class FacadeProperty<T> : FacadeProperty
    {
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
    }
}
