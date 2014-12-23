using Assisticant.Facades;
using AutoDependencyPropertyMarker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JungleControls
{
    [AutoDependencyProperty]
    public class PrecisePopupPlacement : Freezable
    {
        internal readonly PrecisePopupPlacementModel Model;

        public object Tag { get; set; }
        public HorizontalAlignment HorizontalTargetAlignment { get; set; }
        public VerticalAlignment VerticalTargetAlignment { get; set; }
        public HorizontalAlignment HorizontalPopupAlignment { get; set; }
        public VerticalAlignment VerticalPopupAlignment { get; set; }
        public double HorizontalOffset { get; set; }
        public double VerticalOffset { get; set; }

        public PrecisePopupPlacement()
        {
            Model = new PrecisePopupPlacementModel(this);
            FacadeModel.UpdateAll(Model, this);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            FacadeModel.Update(Model, this, e);
            base.OnPropertyChanged(e);
        }

        protected override Freezable CreateInstanceCore() { return new PrecisePopupPlacement(); }
    }
}
