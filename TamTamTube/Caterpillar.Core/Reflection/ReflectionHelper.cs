using Caterpillar.Core.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Caterpillar.Core.Reflection
{
    /// <summary>
    /// Helper class for reflection operations.
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// Gets BindingFlags which having value of public and instance flags
        /// </summary>
        public const BindingFlags PublicInstanceFlags = BindingFlags.Public | BindingFlags.Instance;

        /// <summary>
        /// Gets properties which are marked with attribute with type of TAttribute.
        /// </summary>
        /// <typeparam name="TAttribute">Type of marker atrribute</typeparam>
        /// <param name="obj">Object whose properties are marked with attribute</param>
        /// <returns>Returns object properties System.Reflection.PropertyInfo[].</returns>
        public static PropertyInfo[] GetObjectPropertiesMarkedWithAttribute<TAttribute>(object obj) 
        {
            return GetObjectPropertiesMarkedWithAttribute(obj, typeof(TAttribute));
        }

        /// <summary>
        /// Gets properties which are marked with attribute with type of TAttribute.
        /// </summary>
        /// <param name="attributeType">Type of marker atrribute.</param>
        /// <param name="obj">Object whose properties are marked with attribute.</param>
        /// <returns>Returns object properties System.Reflection.PropertyInfo[].</returns>
        public static PropertyInfo[] GetObjectPropertiesMarkedWithAttribute(object obj, Type attributeType)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(string.Format(LanguageStrings.ArgumentNullExceptionMessageFormat, "obj"));
            }

            if (attributeType == null)
            {
                throw new ArgumentNullException(string.Format(LanguageStrings.ArgumentNullExceptionMessageFormat, "attributeType"));
            }

            List<PropertyInfo> foundProperties = new List<PropertyInfo>();
            Type objectType = obj.GetType();
            PropertyInfo[] properties = objectType.GetProperties(PublicInstanceFlags);
            IEnumerable<Attribute> tempAttributes = null;
            foreach (PropertyInfo eachPropertyInfo in properties)
            {
                tempAttributes = eachPropertyInfo.GetCustomAttributes(attributeType);
                if (tempAttributes != null && tempAttributes.Count() > 0)
                {
                    foundProperties.Add(eachPropertyInfo);
                }
            }

            if (foundProperties.Count > 0)
            {
                return foundProperties.ToArray();
            }

            return null;
        }

        /// <summary>
        /// Gets all properties marked with specified flags.
        /// </summary>
        /// <param name="obj">Object to get properties.</param>
        /// <param name="flags">Flags for properties to get.</param>
        /// <returns>Returns all properties having specified flags</returns>
        public static PropertyInfo[] GetProperties(object obj, BindingFlags flags) 
        {
            if (obj == null)
            {
                throw new ArgumentNullException(string.Format(LanguageStrings.ArgumentNullExceptionMessageFormat, "obj"));
            }

            return obj.GetType().GetProperties(flags);
        }

        /// <summary>
        /// Creates instance of specified type with custom data. Type argument can be in fully qualified format 'Namespace.Class, Assambly.dll' or local type name  format 'Namespace.Class'
        /// </summary>
        /// <param name="type">Type to create instance from</param>
        /// <param name="activationData">Data to create type. If no data is need, provide null.</param>
        /// <returns>Returns created instance in object type.</returns>
        public static object CreateInstance(string type, params object[] activationData) 
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                ObjectNullException.Throw("type");
            }

            string[] typeParts = type.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (typeParts == null)
            {
                ObjectNullException.Throw("typeParts");
            }

            Type activationType = null;
            if (typeParts.Length == 1)
            {
                activationType = Type.GetType(typeParts[0]);
            }
            else
            {
                activationType = Type.GetType(type);
            }

            if (activationData == null || activationData.Length == 0)
            {
                return Activator.CreateInstance(activationType);
            }
            else
            {
                return Activator.CreateInstance(activationType, activationData);
            }            
        }
    }
}
