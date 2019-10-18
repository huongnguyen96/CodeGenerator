using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeGeneration.App
{
    public class FEGenerator 
    {
        protected string rootPath = "FE\\src";
        protected string GetPrimitiveType(Type type)
        {
            if (type.FullName == typeof(int).FullName)
                return "number";
            if (type.FullName == typeof(int?).FullName)
                return "number";
            if (type.FullName == typeof(decimal).FullName)
                return "number";
            if (type.FullName == typeof(decimal?).FullName)
                return "number";
            if (type.FullName == typeof(double).FullName)
                return "number";
            if (type.FullName == typeof(double?).FullName)
                return "number";
            if (type.FullName == typeof(long).FullName)
                return "number";
            if (type.FullName == typeof(long?).FullName)
                return "number";
            if (type.FullName == typeof(string).FullName)
                return "string";
            if (type.FullName == typeof(Guid).FullName)
                return "string";
            if (type.FullName == typeof(Guid?).FullName)
                return "string";
            if (type.FullName == typeof(DateTime).FullName)
                return "string | Date";
            if (type.FullName == typeof(DateTime?).FullName)
                return "string | Date";
            return null;
        }
        protected string GetReferenceType(Type type)
        {
            string primitiveType = GetPrimitiveType(type);
            if (string.IsNullOrEmpty(primitiveType) && type.Name != typeof(ICollection<>).Name)
                return GetClassName(type);
            return null;
        }
        protected string GetListType(Type type)
        {
            if (type.Name == typeof(ICollection<>).Name)
                return GetClassName(type.GetGenericArguments().FirstOrDefault());
            return null;
        }
        protected string GetFilterType(Type type)
        {
            if (type.FullName == typeof(Guid).FullName)
                return "TextFilter";
            if (type.FullName == typeof(Guid?).FullName)
                return "TextFilter";
            if (type.FullName == typeof(string).FullName)
                return "TextFilter";
            if (type.FullName == typeof(int).FullName)
                return "NumberFilter";
            if (type.FullName == typeof(int?).FullName)
                return "NumberFilter";
            if (type.FullName == typeof(decimal).FullName)
                return "NumberFilter";
            if (type.FullName == typeof(decimal?).FullName)
                return "NumberFilter";
            if (type.FullName == typeof(double).FullName)
                return "NumberFilter";
            if (type.FullName == typeof(double?).FullName)
                return "NumberFilter";
            if (type.FullName == typeof(long).FullName)
                return "NumberFilter";
            if (type.FullName == typeof(long?).FullName)
                return "NumberFilter";
            if (type.FullName == typeof(DateTime).FullName)
                return "DateFilter";
            if (type.FullName == typeof(DateTime?).FullName)
                return "DateFilter";
            return null;
        }

        protected string DeclareProperty(string type, string property)
        {
            return $@"
        public {type} {property} {{ get; set; }}";
        }

        protected string MappingProperty(string target, string source, string property)
        {
            return $@"
            {target}.{property} = {source}.{property};";
        }

        protected string GetClassName(Type type)
        {
            return type.Name.Substring(0, type.Name.Length - 3);
        }

        protected List<PropertyInfo> ListProperties(Type type)
        {
            return type.GetProperties().Where(p => !p.Name.Contains("_")).ToList();
        }
        protected string PascalCase(string str)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Char.ToUpper(str[0]));
            builder.Append(str.Substring(1, str.Length - 1));
            return builder.ToString();
        }

        protected string CamelCase(string str)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Char.ToLower(str[0]));
            builder.Append(str.Substring(1, str.Length - 1));
            return builder.ToString();
        }

        protected string SnakeCase(string str)
        {
            List<string> split = Regex.Split(str, @"(?<!^)(?=[A-Z])").Select(s => s.ToLower().Trim()).ToList();
            string result = string.Join("_", split);
            return result;
        }
        protected string KebabCase(string str)
        {
            List<string> split = Regex.Split(str, @"(?<!^)(?=[A-Z])").Select(s => s.ToLower().Trim()).ToList();
            string result = string.Join("-", split);
            return result;
        }

        public string UpperCase(string str)
        {
            return SnakeCase(str).ToUpper();
        }
    }
}
