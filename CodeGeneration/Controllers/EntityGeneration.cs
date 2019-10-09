using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CodeGeneration.Controllers
{
    public class EntityGeneration
    {
        private string Namespace { get; set; }
        private List<Type> Classes { get; set; }
        private const string Entities = "Entities";

        public EntityGeneration(string Namespace, List<Type> Classes)
        {
            this.Namespace = Namespace;
            this.Classes = Classes;
        }

        public void Build()
        {
            foreach (Type type in Classes)
            {
                string ClassName = type.Name.Substring(0, type.Name.Length - 3);
                string path = Path.Combine(Entities, ClassName + ".cs");
                string DeclareProperty = BuildProperty(type);
                string FilterProperty = BuildFilterProperty(type);
                string OrderProperty = BuildOrderProperty(type);
                string SelectProperty = BuildSelectProperty(type);
                string content = $@"
using System;
using System.Collections.Generic;
using Common;

namespace {Namespace}.{Entities}
{{
    public class {ClassName} : DataEntity
    {{
        {DeclareProperty}
    }}

    public class {ClassName}Filter : FilterEntity
    {{
        {FilterProperty}
        public {ClassName}Order OrderBy {{get; set;}}
        public {ClassName}Select Selects {{get; set;}}
    }}

    public enum {ClassName}Order
    {{
        {OrderProperty}
    }}

    public enum {ClassName}Select:long
    {{
        ALL = E.ALL,
        {SelectProperty}
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
                if (PropertyInfo.Name == "CX")
                    continue;
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(primitiveType))
                    continue;

                PropertyString += $"public {primitiveType} {PropertyInfo.Name} {{ get; set; }}\n\t\t";

            }
            return PropertyString;
        }

        private string BuildFilterProperty(Type type)
        {
            string PropertyString = string.Empty;
            List<PropertyInfo> PropertyInfoes = type.GetProperties().ToList();
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                if (PropertyInfo.Name == "CX")
                    continue;
                string primitiveType = GetFilterType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(primitiveType))
                    continue;

                PropertyString += $"public {primitiveType} {PropertyInfo.Name} {{ get; set; }}\n\t\t";

            }
            return PropertyString;
        }

        private string GetPrimitiveType(Type type)
        {
            if (type.Name == typeof(Guid).Name)
                return "Guid";
            if (type.Name == typeof(Guid?).Name)
                return "Guid?";
            if (type.Name == typeof(int).Name)
                return "int";
            if (type.Name == typeof(int?).Name)
                return "int?";
            if (type.Name == typeof(decimal).Name)
                return "decimal";
            if (type.Name == typeof(decimal?).Name)
                return "decimal?";
            if (type.Name == typeof(double).Name)
                return "double";
            if (type.Name == typeof(double?).Name)
                return "double?";
            if (type.Name == typeof(bool).Name)
                return "bool";
            if (type.Name == typeof(bool?).Name)
                return "bool?";
            if (type.Name == typeof(string).Name)
                return "string";
            if (type.Name == typeof(DateTime).Name)
                return "DateTime";
            if (type.Name == typeof(DateTime?).Name)
                return "DateTime?";
            if (type.Name == typeof(long).Name)
                return "long";
            if (type.Name == typeof(long?).Name)
                return "long?";
            return null;
        }

        private string GetFilterType(Type type)
        {
            if (type.Name == typeof(Guid).Name)
                return "GuidFilter";
            if (type.Name == typeof(Guid?).Name)
                return "GuidFilter";
            if (type.Name == typeof(int).Name)
                return "IntFilter";
            if (type.Name == typeof(int?).Name)
                return "IntFilter";
            if (type.Name == typeof(decimal).Name)
                return "DecimalFilter";
            if (type.Name == typeof(decimal?).Name)
                return "DecimalFilter";
            if (type.Name == typeof(double).Name)
                return "DoubleFilter";
            if (type.Name == typeof(double?).Name)
                return "DoubleFilter";
            if (type.Name == typeof(string).Name)
                return "StringFilter";
            if (type.Name == typeof(DateTime).Name)
                return "DateTimeFilter";
            if (type.Name == typeof(DateTime?).Name)
                return "DateTimeFilter";
            if (type.Name == typeof(long).Name)
                return "LongFilter";
            if (type.Name == typeof(long?).Name)
                return "LongFilter";
            if (type.Name == typeof(bool).Name)
                return "bool?";
            return null;
        }

        private string BuildOrderProperty(Type type)
        {
            string ClassName = type.Name.Substring(0, type.Name.Length - 3);
            string PropertyString = string.Empty;
            List<PropertyInfo> PropertyInfoes = type.GetProperties().ToList();
            int count = 0;
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                if (PropertyInfo.Name == "CX")
                    continue;
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(primitiveType))
                    continue;
                count++;
                if (PropertyInfo.Name.EndsWith("Id"))
                    continue;
                if (string.IsNullOrEmpty(primitiveType))
                    continue;
                PropertyString += $@"
        {PropertyInfo.Name},";
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
                if (PropertyInfo.Name == "CX")
                    continue;
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
