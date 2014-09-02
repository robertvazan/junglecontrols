using Assisticant.Collections;
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
    class PropertySheetModel
    {
        public FacadeProperty<FontWeight> FontWeight;
        public FacadeProperty<FontWeight?> HeaderFontWeight;
        public FacadeProperty<Brush> Foreground;
        public FacadeProperty<Brush> HeaderForeground;
        public FacadeProperty<DataTemplate> HeaderTemplate;

        public ObservableList<Tuple<object, object>> Items = new ObservableList<Tuple<object, object>>();
        public IEnumerable<PropertySheetRow> Rows { get { return Items.Select(item => new PropertySheetRow(this, item.Item1, item.Item2)); } }
        public FontWeight EffectiveHeaderFontWeight { get { return HeaderFontWeight.Value ?? FontWeight.Value; } }
        public Brush EffectiveHeaderForeground { get { return HeaderForeground.Value ?? Foreground.Value; } }
    }
}
