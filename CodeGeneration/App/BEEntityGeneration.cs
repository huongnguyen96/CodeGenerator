using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CodeGeneration.App
{
    public class BEEntityGeneration : BEGenerator
    {
        private string Namespace { get; set; }
        private List<Type> Classes { get; set; }
        private const string Entities = "Entities";

        public BEEntityGeneration(string Namespace, List<Type> Classes)
        {
            if (!Directory.Exists(Entities))
                Directory.CreateDirectory(Entities);
            this.Namespace = Namespace;
            this.Classes = Classes;
        }

        public void Build()
        {
            foreach (Type type in Classes)
            {
                string ClassName = type.Name.Substring(0, type.Name.Length - 3);
                string path = Path.Combine(Entities, ClassName + ".cs");
                string content = $@"
using System;
using System.Collections.Generic;
using Common;

namespace {Namespace}.{Entities}
{{
    public class {ClassName} : DataEntity
    {{
        {BuildProperty(type)}
    }}

    public class {ClassName}Filter : FilterEntity
    {{
        {BuildFilterProperty(type)}
        public {ClassName}Order OrderBy {{get; set;}}
        public {ClassName}Select Selects {{get; set;}}
    }}

    public enum {ClassName}Order
    {{
        {BuildOrderProperty(type)}
    }}

    public enum {ClassName}Select:long
    {{
        ALL = E.ALL,
        {BuildSelectProperty(type)}
    }}
}}
";
                System.IO.File.WriteAllText(path, content);
            }
        }
        private string BuildProperty(Type type)
        {
            string PropertyString = string.Empty;
            List<PropertyInfo> PropertyInfoes = type.GetProperties().ToList();
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(primitiveType))
                    continue;

                PropertyString += $@"
        public {primitiveType} {PropertyInfo.Name} {{ get; set; }}";

            }
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                if (!string.IsNullOrEmpty(primitiveType))
                    continue;

                string referenceType = string.Empty;
                if (PropertyInfo.PropertyType.Name == typeof(ICollection<>).Name)
                {
                    Type rType = PropertyInfo.PropertyType.GetGenericArguments().SingleOrDefault();
                    if (rType == null)
                        continue;
                    if (!string.IsNullOrEmpty(GetPrimitiveType(rType)))
                        continue;

                    referenceType = rType.Name.Substring(0, rType.Name.Length - 3);
                    PropertyString += $@"
        public List<{referenceType}> {PropertyInfo.Name} {{ get; set; }}";
                }
                else
                {
                    referenceType = PropertyInfo.PropertyType.Name.Substring(0, PropertyInfo.PropertyType.Name.Length - 3);
                    PropertyString += $@"
        public {referenceType} {PropertyInfo.Name} {{ get; set; }}";
                }
            }
            return PropertyString;
        }

        private string BuildFilterProperty(Type type)
        {
            string PropertyString = string.Empty;
            List<PropertyInfo> PropertyInfoes = type.GetProperties().ToList();
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string primitiveType = GetFilterType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(primitiveType))
                    continue;

                PropertyString += $@"
        public {primitiveType} {PropertyInfo.Name} {{ get; set; }}";

            }
            if (PropertyInfoes.Any(p => p.Name == "Id"))
            {
                PropertyString += $@"
        public List<long> Ids {{ get; set; }}
        public List<long> ExceptIds {{ get; set; }}
";
            }
            return PropertyString;
        }

        private string BuildOrderProperty(Type type)
        {
            string ClassName = type.Name.Substring(0, type.Name.Length - 3);
            string PropertyString = string.Empty;
            List<PropertyInfo> PropertyInfoes = type.GetProperties().ToList();
            int count = 0;
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                if (PropertyInfo.Name == "Disabled")
                    continue;
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(primitiveType))
                    continue;
                count++;
                string propertyName = (PropertyInfo.Name.EndsWith("Id") && PropertyInfo.Name.Length > 2)
                    ? PropertyInfo.Name.Substring(0, PropertyInfo.Name.Length - 2)
                    : PropertyInfo.Name;

                PropertyString += $@"
        {propertyName} = {count},";
            }
            return PropertyString;
        }

        private string BuildSelectProperty(Type type)
        {
            string ClassName = type.Name.Substring(0, type.Name.Length - 3);
            string PropertyString = string.Empty;
            List<PropertyInfo> PropertyInfoes = type.GetProperties().ToList();
            int count = 0;
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(primitiveType))
                    continue;
                count++;
                string SelectProperty = string.Empty;
                if (PropertyInfo.Name.EndsWith("Id") && PropertyInfo.Name.Length > 2)
                    SelectProperty = PropertyInfo.Name.Substring(0, PropertyInfo.Name.Length - 2);
                else
                    SelectProperty = PropertyInfo.Name;
                if (string.IsNullOrEmpty(primitiveType))
                    continue;
                PropertyString += $@"
        {SelectProperty} = E._{count},";
            }
            return PropertyString;
        }
    }
}
