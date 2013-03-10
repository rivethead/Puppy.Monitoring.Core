using System;
using System.Reflection;

namespace Puppy.Monitoring
{
    internal class ReflectionHelper
    {
        public static void SetId<T>(T instance, int id)
        {
            var type = typeof(T);
            var idProperty = type.GetProperty("Id");

            idProperty.SetMethod.Invoke(instance, new object[] { id });
        }

        public static object GetPrivateField<T>(T instance, string fieldName)
        {
            var type = typeof(T);

            var fieldInfo = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (fieldInfo == null)
                throw new Exception(string.Format("Failed to find field '{0}' on instance of type {1}",
                                                  fieldName, type));

            return fieldInfo.GetValue(instance);
        }

        public static object GetPrivateProperty<T>(T instance, string propertyName)
        {
            var type = typeof(T);

            var propertyInfo = type.GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (propertyInfo == null)
                throw new Exception(string.Format("Failed to find property '{0}' on instance of type {1}",
                                                  propertyName, type));

            return propertyInfo.GetValue(instance);
        }
    }
}