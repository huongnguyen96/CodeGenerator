using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CodeGeneration.App
{
    public partial class ControllerGenerator
    {
        private void BuilList_MainDTO(Type type, string NamespaceList, string path)
        {
            string ClassName = GetClassName(type);
            BuilList_DTO(ClassName, type, NamespaceList, path);
            List<PropertyInfo> PropertyInfoes = ListProperties(type);
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(primitiveType))
                {
                    if (PropertyInfo.PropertyType.Name == typeof(ICollection<>).Name)
                    {
                        Type listType = PropertyInfo.PropertyType.GetGenericArguments().FirstOrDefault();
                        if (PropertyInfoes.Any(p => p.PropertyType.Name == listType.Name))
                            continue;
                        BuilList_DTO(ClassName, listType, NamespaceList, path);
                        List<PropertyInfo> Children = ListProperties(listType);
                        foreach (PropertyInfo Child in Children)
                        {
                            string childPrimitiveType = GetPrimitiveType(Child.PropertyType);
                            if (string.IsNullOrEmpty(childPrimitiveType) && type.Name != Child.PropertyType.Name && !PropertyInfoes.Any(p => p.Name == Child.PropertyType.Name))
                            {
                                BuilList_DTO(ClassName, Child.PropertyType, NamespaceList, path, false);
                            }
                        }
                    }
                    else
                    {
                        BuilList_DTO(ClassName, PropertyInfo.PropertyType, NamespaceList, path, false);
                    }
                }
            }
        }

        private void BuilList_DTO(string MainClassName, Type type, string NamespaceList, string path, bool IsMainClass = true)
        {
            string ClassName = GetClassName(type);
            path = Path.Combine(path, $@"{MainClassName}List_{ClassName}DTO.cs");
            string content = $@"
using {Namespace}.Entities;
using Common;
using System;
using System.Collections.Generic;

namespace {Namespace}.Controllers.{NamespaceList}
{{
    public class {MainClassName}List_{ClassName}DTO : DataDTO
    {{
        {ListDeclareProperty(type, IsMainClass)}
        public {MainClassName}List_{ClassName}DTO() {{}}
        public {MainClassName}List_{ClassName}DTO({ClassName} {ClassName})
        {{
            {ListConstructorMapping(type, IsMainClass)}
        }}
    }}

    public class {MainClassName}List_{ClassName}FilterDTO : FilterDTO
    {{
        {ListDeclareFilter(type, IsMainClass)}
    }}
}}
";
            File.WriteAllText(path, content);
        }

        private string ListDeclareProperty(Type type, bool IsMainClass = true)
        {
            string content = string.Empty;
            List<PropertyInfo> PropertyInfoes = ListProperties(type);
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(primitiveType))
                {
                    if (IsMainClass)
                    {
                        if (PropertyInfo.PropertyType.Name == typeof(ICollection<>).Name)
                        {
                            string typeName = GetClassName(PropertyInfo.PropertyType.GetGenericArguments().FirstOrDefault());
                            typeName = $"List<{typeName}>";
                            content += DeclareProperty(typeName, PropertyInfo.Name);
                        }
                        else
                        {
                            string typeName = GetClassName(PropertyInfo.PropertyType);
                            content += DeclareProperty(typeName, PropertyInfo.Name);
                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    content += DeclareProperty(primitiveType, PropertyInfo.Name);
                }
            }

            return content;
        }
        private string ListConstructorMapping(Type type, bool IsMainClass = true)
        {
            string content = string.Empty;
            string ClassName = GetClassName(type);
            List<PropertyInfo> PropertyInfoes = ListProperties(type);
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(primitiveType))
                {
                    if (IsMainClass)
                    {
                        if (PropertyInfo.PropertyType.Name == typeof(ICollection<>).Name)
                        {
                            string typeName = GetClassName(PropertyInfo.PropertyType.GetGenericArguments().FirstOrDefault());
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
                }
                else
                {
                    content += MappingProperty("this", ClassName, PropertyInfo.Name);
                }
            }
            return content;
        }
        private string ListDeclareFilter(Type type, bool IsMainClass = true)
        {
            string content = string.Empty;
            List<PropertyInfo> PropertyInfoes = ListProperties(type);
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string filterType = GetFilterType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(filterType))
                {
                    continue;
                }
                else
                {
                    content += DeclareProperty(filterType, PropertyInfo.Name);
                }
            }
            return content;
        }
    }
}
