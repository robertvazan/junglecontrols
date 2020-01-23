// Part of JungleControls: https://blog.machinezoo.com/junglecontrols-free-wpf-controls-for
using Assisticant.Collections;
using Assisticant.Facades;
using AutoDependencyPropertyMarker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace JungleControls
{
    [AutoDependencyProperty]
    public class PrecisePopup : ContentControl
    {
        readonly PrecisePopupModel Model;

        public FrameworkElement PlacementTarget { get; set; }
        public FrameworkElement SuppressTarget { get; set; }
        [AutoDependencyProperty(Options = FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)]
        public bool IsOpen { get; set; }
        public bool AllowsTransparency { get; set; }
        public PrecisePopupPlacementCollection Placements { get; set; }

        static readonly DependencyPropertyKey SelectedPlacementKey = DependencyProperty.RegisterReadOnly("SelectedPlacement", typeof(PrecisePopupPlacement), typeof(PrecisePopup), new FrameworkPropertyMetadata());
        public static readonly DependencyProperty SelectedPlacementProperty = SelectedPlacementKey.DependencyProperty;
        public PrecisePopupPlacement SelectedPlacement { get { return (PrecisePopupPlacement)GetValue(SelectedPlacementProperty); } }

        static PrecisePopup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PrecisePopup), new FrameworkPropertyMetadata(typeof(PrecisePopup)));
            VisibilityProperty.OverrideMetadata(typeof(PrecisePopup), new FrameworkPropertyMetadata(Visibility.Collapsed));
        }

        public PrecisePopup()
        {
            Model = new PrecisePopupModel(this);
            Placements = new PrecisePopupPlacementCollection();
            FacadeModel.UpdateAll(Model, this);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            FacadeModel.Update(Model, this, e);
            base.OnPropertyChanged(e);
        }

        internal void UpdateSelectedPlacement() { SetValue(SelectedPlacementKey, Model.IsOpen ? Model.SelectedCandidate.Placement.PlacementControl : null); }
    }
}
