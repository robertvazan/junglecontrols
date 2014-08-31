using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JungleControls
{
    class FacadeMapping
    {
        public readonly FieldInfo ModelField;
        public readonly DependencyProperty FacadeProperty;
        public readonly FacadePropertyMetadata PropertyMetadata;

        public FacadeMapping(FieldInfo field, DependencyProperty property, FacadePropertyMetadata metadata)
        {
            ModelField = field;
            FacadeProperty = property;
            PropertyMetadata = metadata;
        }
    }
}
