using Assisticant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JungleControls
{
    public class FacadeInstance
    {
        readonly FacadeType FacadeType;
        readonly DependencyObject Facade;
        object Model;
        FacadeProperty[] ModelFields;

        public FacadeInstance(FacadeType facadeType, DependencyObject facade)
        {
            FacadeType = facadeType;
            Facade = facade;
        }

        public void ApplyModel(DependencyObject obj)
        {
            var element = obj as FrameworkElement;
            if (element != null)
                element.DataContext = ForView.Wrap(GetModel());
        }

        public object GetModel()
        {
            if (Model == null)
            {
                Model = Activator.CreateInstance(FacadeType.ModelType);
                ModelFields = new FacadeProperty[FacadeType.FieldMappings.Count];
                foreach (var mapping in FacadeType.FieldMappings)
                {
                    var computed = Activator.CreateInstance(typeof(FacadeProperty<>).MakeGenericType(mapping.FacadeProperty.PropertyType), Facade, mapping.FacadeProperty);
                    ModelFields[mapping.PropertyMetadata.Index] = (FacadeProperty)computed;
                    mapping.ModelField.SetValue(Model, computed);
                }
            }
            return Model;
        }

        internal void NotifyModel(object sender, DependencyPropertyChangedEventArgs args)
        {
            if (ModelFields != null)
            {
                if (sender != Facade)
                    throw new InvalidOperationException("Misrouted property change notification");
                var metadata = args.Property.GetMetadata(Facade) as FacadePropertyMetadata;
                if (metadata == null)
                    throw new InvalidOperationException("Received for notification that doesn't have FacadePropertyMetadata: " + args.Property);
                var computed = ModelFields[metadata.Index];
                computed.Invalidate();
            }
        }
    }
}
