using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CodeGeneration.App
{
    public partial class BEControllerGenerator_Master : BEGenerator
    {
        private string Namespace { get; set; }
        public string RootRoute { get; set; }
        private List<Type> Classes { get; set; }

        public const string Controllers = "Controllers";
        public BEControllerGenerator_Master(string Namespace, string RootRoute, List<Type> Classes)
        {
            if (!Directory.Exists(Controllers))
                Directory.CreateDirectory(Controllers);
            this.Namespace = Namespace;
            this.RootRoute = RootRoute;
            this.Classes = Classes;
        }

        public void Build()
        {
            string content = $@"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace {Namespace}.Controllers
{{
    public class Root
    {{
        public const string Base = ""api"";
    }}

    [Authorize]
    public class ApiController : ControllerBase
    {{
    }}
}}  
";
            File.WriteAllText(Controllers + "/ApiController.cs", content);
            foreach (Type type in Classes)
            {
                if (type.Name.Contains("_"))
                    continue;

                string ClassName = GetClassName(type);
                string NamespaceList = SnakeCase(ClassName) + "." + SnakeCase(ClassName) + "_master";
                string RouteList = $"{KebabCase(ClassName)}/{KebabCase(ClassName)}-master";
                string path = Path.Combine(Controllers, RouteList);
                string controllerPath = Path.Combine(path, ClassName + "MasterController.cs");
                Directory.CreateDirectory(path);
                BuildList_MainDTO(type, NamespaceList, path);

                content = $@"
{BuildUsing(type)}

namespace {Namespace}.Controllers.{NamespaceList}
{{
    public class {ClassName}MasterRoute : Root
    {{
        public const string FE = ""/{RouteList}"";
        private const string Default = Base + FE;
        public const string Count = Default + ""/count"";
        public const string List = Default + ""/list"";
        public const string Get = Default + ""/get"";
        {BuildSingleListRoute(type)}
    }}

    public class {ClassName}MasterController : ApiController
    {{
        {ControllerConstructor(type)}

        [Route({ClassName}MasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] {ClassName}Master_{ClassName}FilterDTO {ClassName}Master_{ClassName}FilterDTO)
        {{
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            {ClassName}Filter {ClassName}Filter = ConvertFilterDTOToFilterEntity({ClassName}Master_{ClassName}FilterDTO);

            return await {ClassName}Service.Count({ClassName}Filter);
        }}

        [Route({ClassName}MasterRoute.List), HttpPost]
        public async Task<List<{ClassName}Master_{ClassName}DTO>> List([FromBody] {ClassName}Master_{ClassName}FilterDTO {ClassName}Master_{ClassName}FilterDTO)
        {{
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            {ClassName}Filter {ClassName}Filter = ConvertFilterDTOToFilterEntity({ClassName}Master_{ClassName}FilterDTO);

            List<{ClassName}> {ClassName}s = await {ClassName}Service.List({ClassName}Filter);

            return {ClassName}s.Select(c => new {ClassName}Master_{ClassName}DTO(c)).ToList();
        }}

        [Route({ClassName}MasterRoute.Get), HttpPost]
        public async Task<{ClassName}Master_{ClassName}DTO> Get([FromBody]{ClassName}Master_{ClassName}DTO {ClassName}Master_{ClassName}DTO)
        {{
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            {ClassName} {ClassName} = await {ClassName}Service.Get({ClassName}Master_{ClassName}DTO.Id);
            return new {ClassName}Master_{ClassName}DTO({ClassName});
        }}


        public {ClassName}Filter ConvertFilterDTOToFilterEntity({ClassName}Master_{ClassName}FilterDTO {ClassName}Master_{ClassName}FilterDTO)
        {{
            {ClassName}Filter {ClassName}Filter = new {ClassName}Filter();
            {ConvertFilterDTOToFilterEntity(type)}
            return {ClassName}Filter;
        }}
        
        {BuildSingleListMethod(type)}
    }}
}}
";
                File.WriteAllText(controllerPath, content);
            }
        }

        private string ConvertFilterDTOToFilterEntity(Type type)
        {
            string ClassName = type.Name.Substring(0, type.Name.Length - 3);
            string content = string.Empty;
            List<PropertyInfo> PropertyInfoes = ListProperties(type);

            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string filterType = GetDTOFilterType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(filterType))
                    continue;
                content += MappingDTOFilter($"{ClassName}Filter", $"{ClassName}Master_{ClassName}FilterDTO", PropertyInfo.Name, filterType);
            }
            return content;
        }

        private void BuildList_MainDTO(Type type, string NamespaceList, string path)
        {
            string ClassName = GetClassName(type);
            BuildDTO(ClassName, type, NamespaceList, path, 1);
            List<PropertyInfo> PropertyInfoes = ListProperties(type);
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string referenceType = GetReferenceType(PropertyInfo.PropertyType);
                if (!string.IsNullOrEmpty(referenceType))
                {
                    BuildDTO(ClassName, PropertyInfo.PropertyType, NamespaceList, path, 3);
                }
                string listType = GetListType(PropertyInfo.PropertyType);
                if (!string.IsNullOrEmpty(listType))
                {
                    Type list = PropertyInfo.PropertyType.GetGenericArguments().FirstOrDefault();
                    BuildDTO(ClassName, list, NamespaceList, path, 2);
                    List<PropertyInfo> Children = ListProperties(list);
                    foreach (PropertyInfo Child in Children)
                    {
                        string childReferenceType = GetReferenceType(Child.PropertyType);
                        if (!string.IsNullOrEmpty(childReferenceType))
                        {
                            BuildDTO(ClassName, Child.PropertyType, NamespaceList, path, 3);
                        }
                    }
                }
            }
        }

        private void BuildDTO(string MainClassName, Type type, string NamespaceList, string path, int level)
        {
            string ClassName = GetClassName(type);
            path = Path.Combine(path, $@"{MainClassName}Master_{ClassName}DTO.cs");
            string content = $@"
using {Namespace}.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace {Namespace}.Controllers.{NamespaceList}
{{
    public class {MainClassName}Master_{ClassName}DTO : DataDTO
    {{
        {DeclareProperty(MainClassName, type, level)}
        public {MainClassName}Master_{ClassName}DTO() {{}}
        public {MainClassName}Master_{ClassName}DTO({ClassName} {ClassName})
        {{
            {ConstructorMapping(MainClassName, type, level)}
        }}
    }}

    public class {MainClassName}Master_{ClassName}FilterDTO : FilterDTO
    {{
        {DeclareFilter(type, level)}
    }}
}}
";
            File.WriteAllText(path, content);
        }

        private string DeclareProperty(string MainClassName, Type type,int level)
        {
            string content = string.Empty;
            List<PropertyInfo> PropertyInfoes = ListProperties(type);
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                string referenceType = GetReferenceType(PropertyInfo.PropertyType);
                string listType = GetListType(PropertyInfo.PropertyType);
                if (!string.IsNullOrEmpty(primitiveType))
                {
                    content += DeclareProperty(primitiveType, PropertyInfo.Name);
                }
                if (!string.IsNullOrEmpty(referenceType) && (level == 1 || level == 2))
                {
                    string typeName = GetClassName(PropertyInfo.PropertyType);
                    if (typeName == MainClassName)
                        continue;
                    typeName = $"{MainClassName}Master_{typeName}DTO";
                    content += DeclareProperty(typeName, PropertyInfo.Name);
                }
                if (!string.IsNullOrEmpty(listType) && level == 1)
                {
                    string typeName = GetClassName(PropertyInfo.PropertyType.GetGenericArguments().FirstOrDefault());
                    if (typeName == MainClassName)
                        continue;
                    typeName = $"List<{MainClassName}Detail_{typeName}DTO>";
                    content += DeclareProperty(typeName, PropertyInfo.Name);
                }
            }

            return content;
        }
        private string ConstructorMapping(string MainClassName, Type type, int level)
        {
            string content = string.Empty;
            string ClassName = GetClassName(type);
            List<PropertyInfo> PropertyInfoes = ListProperties(type);
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                string referenceType = GetReferenceType(PropertyInfo.PropertyType);
                string listType = GetListType(PropertyInfo.PropertyType);

                if (!string.IsNullOrEmpty(primitiveType))
                {
                    content += MappingProperty("this", ClassName, PropertyInfo.Name);
                }
                if (!string.IsNullOrEmpty(referenceType) && (level == 1 || level == 2))
                {
                    string typeName = GetClassName(PropertyInfo.PropertyType);
                    if (typeName == MainClassName)
                        continue;
                    content += $@"
            this.{PropertyInfo.Name} = new {MainClassName}Master_{typeName}DTO({ClassName}.{PropertyInfo.Name});
";
                }
                if (!string.IsNullOrEmpty(listType) && level == 1)
                {
                    string typeName = GetClassName(PropertyInfo.PropertyType.GetGenericArguments().FirstOrDefault());
                    if (typeName == MainClassName)
                        continue;
                    content += $@"
            this.{PropertyInfo.Name} = {ClassName}.{PropertyInfo.Name}?.Select(x => new {MainClassName}Master_{typeName}DTO(x)).ToList();
";
                }
            }
            return content;
        }
        private string DeclareFilter(Type type, int level)
        {
            string content = string.Empty;
            List<PropertyInfo> PropertyInfoes = ListProperties(type);
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string filterType = GetDTOFilterType(PropertyInfo.PropertyType);
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

        private string ControllerConstructor(Type type)
        {
            string ClassName = GetClassName(type);
            List<PropertyInfo> PropertyInfoes = ListProperties(type);
            string declareService = string.Empty;
            string declareParameter = string.Empty;
            string mappingService = string.Empty;
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(primitiveType) && PropertyInfo.PropertyType.Name != typeof(ICollection<>).Name)
                {
                    string typeName = GetClassName(PropertyInfo.PropertyType);
                    declareService += $@"
        private I{typeName}Service {typeName}Service;";
                    declareParameter += $@"
            I{typeName}Service {typeName}Service,";
                    mappingService += $@"
            this.{typeName}Service = {typeName}Service;";
                }
            }
            string content = $@"
        {declareService}
        private I{ClassName}Service {ClassName}Service;

        public {ClassName}MasterController(
            {declareParameter}
            I{ClassName}Service {ClassName}Service
        )
        {{
            {mappingService}
            this.{ClassName}Service = {ClassName}Service;
        }}
";
            return content;
        }
        private string BuildUsing(Type type)
        {
            string ClassName = GetClassName(type);
            List<PropertyInfo> PropertyInfoes = ListProperties(type);
            string declareUsing = string.Empty;
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string referenceType = GetReferenceType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(referenceType))
                    continue;
                declareUsing += $@"
using {Namespace}.Services.M{referenceType};";
            }

            string content = $@"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using {Namespace}.Services.M{ClassName};
using Microsoft.AspNetCore.Mvc;
using {Namespace}.Entities;
{declareUsing}
";
            return content;
        }
        private string BuildSingleListRoute(Type type)
        {
            string content = string.Empty;
            List<PropertyInfo> PropertyInfoes = ListProperties(type);
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string referenceType = GetReferenceType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(referenceType))
                    continue;
                content += $@"
        public const string SingleList{referenceType}=""/single-list-{KebabCase(referenceType)}"";";
            }
            return content;
        }

        private string BuildSingleListMethod(Type type)
        {
            string content = string.Empty;
            string ClassName = GetClassName(type);
            List<PropertyInfo> PropertyInfoes = ListProperties(type);
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string referenceType = GetReferenceType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(referenceType))
                    continue;
                string filterMapping = string.Empty;
                List<PropertyInfo> Children = ListProperties(PropertyInfo.PropertyType);
                foreach (PropertyInfo Child in Children)
                {
                    string filterType = GetDTOFilterType(Child.PropertyType);
                    if (string.IsNullOrEmpty(filterType))
                        continue;
                    filterMapping += MappingDTOFilter($"{referenceType}Filter", $"{ClassName}Master_{referenceType}FilterDTO", Child.Name, filterType);
                }
                content += $@"
        [Route({ClassName}MasterRoute.SingleList{referenceType}), HttpPost]
        public async Task<List<{ClassName}Master_{referenceType}DTO>> SingleList{referenceType}([FromBody] {ClassName}Master_{referenceType}FilterDTO {ClassName}Master_{referenceType}FilterDTO)
        {{
            {referenceType}Filter {referenceType}Filter = new {referenceType}Filter();
            {referenceType}Filter.Skip = 0;
            {referenceType}Filter.Take = 20;
            {referenceType}Filter.OrderBy = {referenceType}Order.Id;
            {referenceType}Filter.OrderType = OrderType.ASC;
            {referenceType}Filter.Selects = {referenceType}Select.ALL;
            {filterMapping}

            List<{referenceType}> {referenceType}s = await {referenceType}Service.List({referenceType}Filter);
            List<{ClassName}Master_{referenceType}DTO> {ClassName}Master_{referenceType}DTOs = {referenceType}s
                .Select(x => new {ClassName}Master_{referenceType}DTO(x)).ToList();
            return {ClassName}Master_{referenceType}DTOs;
        }}
";
            }
            return content;
        }
    }
}
