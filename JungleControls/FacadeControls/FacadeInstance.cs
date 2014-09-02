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
        static readonly Dictionary<Type, FacadeType> TypeMap = new Dictionary<Type, FacadeType>();
        readonly FacadeType FacadeType;
        readonly DependencyObject Facade;
        object Model;
        readonly Dictionary<DependencyProperty, FacadeProperty> ModelFields = new Dictionary<DependencyProperty, FacadeProperty>();

        public FacadeInstance(DependencyObject facade, Type modelType)
        {
            if (!TypeMap.TryGetValue(facade.GetType(), out FacadeType))
                TypeMap[facade.GetType()] = FacadeType = new FacadeType(facade.GetType(), modelType);
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
                foreach (var mapping in FacadeType.FieldMappings)
                {
                    var computed = Activator.CreateInstance(typeof(FacadeProperty<>).MakeGenericType(mapping.Value.PropertyType), Facade, mapping.Value);
                    ModelFields[mapping.Value] = (FacadeProperty)computed;
                    mapping.Key.SetValue(Model, computed);
                }
            }
            return Model;
        }

        public void UpdateModel(DependencyPropertyChangedEventArgs args)
        {
            FacadeProperty computed;
            if (ModelFields.TryGetValue(args.Property, out computed))
                computed.Invalidate();
        }
    }
}
