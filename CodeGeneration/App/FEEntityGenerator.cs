using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CodeGeneration.App
{
    public class FEEntityGenerator : FEGenerator
    {
        private List<Type> Classes;
        private string path;
        public FEEntityGenerator(List<Type> Classes)
        {
            this.Classes = Classes;

        }

        public void Build()
        {
            foreach (Type type in Classes)
            {
                BuildEntity(type);
                BuildSearch(type);
            }
        }

        #region Entity
        public void BuildEntity(Type type)
        {
            string ClassName = GetClassName(type);
            if (ClassName.Contains("_"))
                return;
            path = Path.Combine(rootPath, "models", ClassName + ".ts");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            string contents =
$@"import {{Model}} from 'core';
{BuildImportEntity(type)}

export class {ClassName} extends Model {{
  {BuildDeclareProperty(type)}
  public constructor({CamelCase(ClassName)}?: {ClassName}) {{
    super({CamelCase(ClassName)});
  }}
}}
";
            File.WriteAllText(path, contents);
        }
        public string BuildImportEntity(Type type)
        {
            string contents = string.Empty;
            List<PropertyInfo> PropertyInfoes = ListProperties(type);
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                if (PropertyInfo.Name.Contains("_"))
                    continue;
                string referenceType = GetReferenceType(PropertyInfo.PropertyType);
                if (!string.IsNullOrEmpty(referenceType))
                {
                    contents += $@"
import {{{referenceType}}} from 'models/{referenceType}';";
                }
                string listtype = GetListType(PropertyInfo.PropertyType);
                if (!string.IsNullOrEmpty(listtype))
                {
                    contents += $@"
import {{{listtype}}} from 'models/{listtype}';";
                }
            }
            return contents;
        }
        public string BuildDeclareProperty(Type type)
        {
            string contents = string.Empty;
            List<PropertyInfo> PropertyInfoes = ListProperties(type);
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                if (PropertyInfo.Name.Contains("_"))
                    continue;
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                if (!string.IsNullOrEmpty(primitiveType))
                {
                    contents +=
$@" 
  public {CamelCase(PropertyInfo.Name)}?: {primitiveType};
";
                }
                string referenceType = GetReferenceType(PropertyInfo.PropertyType);
                if (!string.IsNullOrEmpty(referenceType))
                {
                    contents +=
$@"
  public {CamelCase(PropertyInfo.Name)}?: {referenceType};
";
                }
                string listtype = GetListType(PropertyInfo.PropertyType);
                if (!string.IsNullOrEmpty(listtype))
                {
                    contents +=
$@"  
  public {CamelCase(PropertyInfo.Name)}?: {listtype}[];
";
                }
            }
            return contents;
        }

        #endregion

        #region Search
        public void BuildSearch(Type type)
        {
            string ClassName = GetClassName(type);
            if (ClassName.Contains("_"))
                return;
            path = Path.Combine(rootPath, "models", ClassName + "Search.ts");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            string contents =
$@"
import {{Search}} from 'core/entities/Search';

export class {ClassName}Search extends Search {{
  {BuildDeclareSearch(type)};
}}
";
            File.WriteAllText(path, contents);
        }


        public string BuildDeclareSearch(Type type)
        {
            string contents = string.Empty;
            List<PropertyInfo> PropertyInfoes = ListProperties(type);
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                if (PropertyInfo.Name.Contains("_"))
                    continue;
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                if (!string.IsNullOrEmpty(primitiveType))
                {
                    contents +=
                        $@"
  public {CamelCase(PropertyInfo.Name)}?: {primitiveType};
";
                }
            }
            return contents;
        }
        #endregion
    }
}
