using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CodeGeneration.App
{
    public partial class ControllerGenerator
    {
        private void BuilListDTO(Type type, string NamespaceList, string path)
        {
            string ClassName = GetClassName(type);
            path = Path.Combine(path, $@"{ClassName}List_{ClassName}DTO.cs");
            string content = $@"
using {Namespace}.Entities;
using Common;
using System;
using System.Collections.Generic;

namespace {Namespace}.Controllers.{NamespaceList}
{{
    public class {ClassName}List_{ClassName}DTO : DataDTO
    {{
        {MainDeclareProperty(type)}
        public {ClassName}List_{ClassName}DTO() {{}}
        public {ClassName}List_{ClassName}DTO({ClassName} {ClassName})
        {{
            {MainConstructorMapping(type)}
        }}
    }}

    public class {ClassName}List_{ClassName}FilterDTO : FilterDTO
    {{
    }}
}}
";
            File.WriteAllText(path, content);
        }

        private string MainDeclareProperty(Type type)
        {
            string content = string.Empty;
            List<PropertyInfo> PropertyInfoes = type.GetProperties().ToList();
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(primitiveType))
                {
                    if (PropertyInfo.PropertyType.Name != typeof(ICollection<>).Name)
                    {
                        string typeName = PropertyInfo.PropertyType.Name.Substring(0, type.Name.Length - 3);
                        content += DeclareProperty(typeName, PropertyInfo.Name);
                    }
                }
                else
                {
                    content += DeclareProperty(primitiveType, PropertyInfo.Name);
                }
            }

            return content;
        }
        private string MainConstructorMapping(Type type)
        {
            string content = string.Empty;
            string ClassName = GetClassName(type);
            List<PropertyInfo> PropertyInfoes = type.GetProperties().ToList();
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(primitiveType))
                {
                    if (PropertyInfo.PropertyType.Name == typeof(ICollection<>).Name)
                    {
                        if (PropertyInfo.Name.Contains("_"))
                            continue;
                        string typeName = PropertyInfo.PropertyType.GetGenericArguments().Select(x => x.Name).FirstOrDefault();
                        content += $@"
            this.{PropertyInfo.Name} = {ClassName}.{PropertyInfo.Name}?.Select(x => new {ClassName}List_{typeName}DTO(x)).ToList();
";
                    }
                    else
                    {
                        string typeName = GetClassName(PropertyInfo.PropertyType);
                        content += $@"
            this.{PropertyInfo.Name} = new {ClassName}List_{typeName}DTO({ClassName}.{PropertyInfo.Name});
";
                    }
                }
                else
                {
                    content += MappingProperty("this", ClassName, PropertyInfo.Name);
                }
            }
            return content;
        }
    }
}
