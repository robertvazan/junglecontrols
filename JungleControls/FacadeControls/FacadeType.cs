using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JungleControls
{
    class FacadeType
    {
        public readonly Type ComponentType;
        public readonly Type ModelType;
        public readonly Dictionary<FieldInfo, DependencyProperty> FieldMappings = new Dictionary<FieldInfo, DependencyProperty>();

        public FacadeType(Type componentType, Type modelType)
        {
            ComponentType = componentType;
            ModelType = modelType;
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
                FieldMappings[field] = property;
            }
        }
    }
}
