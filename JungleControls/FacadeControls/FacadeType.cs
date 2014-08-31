using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JungleControls
{
    public class FacadeType
    {
        internal readonly Type ComponentType;
        internal readonly Type ModelType;
        readonly Func<DependencyObject, FacadeInstance> InstanceExtractor;
        internal readonly List<FacadeMapping> FieldMappings = new List<FacadeMapping>();

        public FacadeType(Type componentType, Type modelType, Func<DependencyObject, FacadeInstance> instanceExtractor)
        {
            ComponentType = componentType;
            ModelType = modelType;
            InstanceExtractor = instanceExtractor;
            var properties = (from field in ComponentType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                              where field.FieldType == typeof(DependencyProperty) && field.Name.EndsWith("Property")
                              select (DependencyProperty)field.GetValue(null)).ToList();
            foreach (var field in ModelType.GetFields().Where(f => f.FieldType.IsGenericType && f.FieldType.GetGenericTypeDefinition() == typeof(FacadeProperty<>)))
            {
                var property = properties.FirstOrDefault(p => p.Name == field.Name);
                if (property == null)
                    throw new InvalidOperationException("No property corresponds to FacadeProperty: " + field.Name);
                if (field.FieldType.GenericTypeArguments[0] != property.PropertyType)
                    throw new InvalidOperationException("FacadeProperty associated to dependency property has different value type: " + field.Name);
                var metadata = property.GetMetadata(ComponentType) as FacadePropertyMetadata;
                if (metadata == null || metadata.FacadeType != null)
                {
                    if (property.OwnerType == ComponentType)
                        throw new InvalidOperationException("FacadePropertyMetadata must be used explicitly for dependency property linked to FacadeProperty: " + field.Name);
                    property.OverrideMetadata(ComponentType, metadata = new FacadePropertyMetadata());
                }
                metadata.FacadeType = this;
                metadata.Index = FieldMappings.Count;
                FieldMappings.Add(new FacadeMapping(field, property, metadata));
            }
        }

        internal void NotifyModel(object sender, DependencyPropertyChangedEventArgs args)
        {
            var instance = InstanceExtractor((DependencyObject)sender);
            if (instance == null)
                throw new NullReferenceException("FacadeInstance cannot be null");
            instance.NotifyModel(sender, args);
        }
    }
}
