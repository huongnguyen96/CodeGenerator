using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CodeGeneration.App
{
    public partial class BEControllerGenerator_Detail : BEGenerator
    {
        private string Namespace { get; set; }
        public string RootRoute { get; set; }
        private List<Type> Classes { get; set; }

        public const string Controllers = "Controllers";
        public BEControllerGenerator_Detail(string Namespace, string RootRoute, List<Type> Classes)
        {
            if (!Directory.Exists(Controllers))
                Directory.CreateDirectory(Controllers);
            this.Namespace = Namespace;
            this.RootRoute = RootRoute;
            this.Classes = Classes;
        }

        public void Build()
        {
            foreach (Type type in Classes)
            {
                if (type.Name.Contains("_"))
                    continue;

                string ClassName = GetClassName(type);
                string NamespaceList = SnakeCase(ClassName) + "." + SnakeCase(ClassName) + "_detail";
                string RouteList = $"{KebabCase(ClassName)}/{KebabCase(ClassName)}-detail";
                string path = Path.Combine(Controllers, RouteList);
                string controllerPath = Path.Combine(path, ClassName + "DetailController.cs");
                Directory.CreateDirectory(path);
                BuildList_MainDTO(type, NamespaceList, path);

                string content = $@"
{BuildUsing(type)}

namespace {Namespace}.Controllers.{NamespaceList}
{{
    public class {ClassName}DetailRoute : Root
    {{
        public const string FE = ""/{RouteList}"";
        private const string Default = Base + FE;
        public const string Get = Default + ""/get"";
        public const string Create = Default + ""/create"";
        public const string Update = Default + ""/update"";
        public const string Delete = Default + ""/delete"";
        {BuildSingleListRoute(type)}
    }}

    public class {ClassName}DetailController : ApiController
    {{
        {ControllerConstructor(type)}

        [Route({ClassName}DetailRoute.Get), HttpPost]
        public async Task<{ClassName}Detail_{ClassName}DTO> Get([FromBody]{ClassName}Detail_{ClassName}DTO {ClassName}Detail_{ClassName}DTO)
        {{
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            {ClassName} {ClassName} = await {ClassName}Service.Get({ClassName}Detail_{ClassName}DTO.Id);
            return new {ClassName}Detail_{ClassName}DTO({ClassName});
        }}


        [Route({ClassName}DetailRoute.Create), HttpPost]
        public async Task<ActionResult<{ClassName}Detail_{ClassName}DTO>> Create([FromBody] {ClassName}Detail_{ClassName}DTO {ClassName}Detail_{ClassName}DTO)
        {{
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            {ClassName} {ClassName} = ConvertDTOToEntity({ClassName}Detail_{ClassName}DTO);

            {ClassName} = await {ClassName}Service.Create({ClassName});
            {ClassName}Detail_{ClassName}DTO = new {ClassName}Detail_{ClassName}DTO({ClassName});
            if ({ClassName}.IsValidated)
                return {ClassName}Detail_{ClassName}DTO;
            else
                return BadRequest({ClassName}Detail_{ClassName}DTO);        
        }}

        [Route({ClassName}DetailRoute.Update), HttpPost]
        public async Task<ActionResult<{ClassName}Detail_{ClassName}DTO>> Update([FromBody] {ClassName}Detail_{ClassName}DTO {ClassName}Detail_{ClassName}DTO)
        {{
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            {ClassName} {ClassName} = ConvertDTOToEntity({ClassName}Detail_{ClassName}DTO);

            {ClassName} = await {ClassName}Service.Update({ClassName});
            {ClassName}Detail_{ClassName}DTO = new {ClassName}Detail_{ClassName}DTO({ClassName});
            if ({ClassName}.IsValidated)
                return {ClassName}Detail_{ClassName}DTO;
            else
                return BadRequest({ClassName}Detail_{ClassName}DTO);        
        }}

        [Route({ClassName}DetailRoute.Delete), HttpPost]
        public async Task<ActionResult<{ClassName}Detail_{ClassName}DTO>> Delete([FromBody] {ClassName}Detail_{ClassName}DTO {ClassName}Detail_{ClassName}DTO)
        {{
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            {ClassName} {ClassName} = ConvertDTOToEntity({ClassName}Detail_{ClassName}DTO);

            {ClassName} = await {ClassName}Service.Delete({ClassName});
            {ClassName}Detail_{ClassName}DTO = new {ClassName}Detail_{ClassName}DTO({ClassName});
            if ({ClassName}.IsValidated)
                return {ClassName}Detail_{ClassName}DTO;
            else
                return BadRequest({ClassName}Detail_{ClassName}DTO);        
        }}

        public {ClassName} ConvertDTOToEntity({ClassName}Detail_{ClassName}DTO {ClassName}Detail_{ClassName}DTO)
        {{
            {ClassName} {ClassName} = new {ClassName}();
            {ConvertDTOToEntity(type)}
            return {ClassName};
        }}
        
        {BuildSingleListMethod(type)}
    }}
}}
";
                File.WriteAllText(controllerPath, content);
            }
        }

     
        private string ConvertDTOToEntity(Type type)
        {
            string ClassName = type.Name.Substring(0, type.Name.Length - 3);
            string content = string.Empty;
            List<PropertyInfo> PropertyInfoes = ListProperties(type);

            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(primitiveType))
                    continue;
                content += MappingProperty($"{ClassName}", $"{ClassName}Detail_{ClassName}DTO", PropertyInfo.Name);
            }
            return content;
        }
        private string ConvertFilterDTOToFilterEntity(string MainClassName, Type type)
        {
            string ClassName = type.Name.Substring(0, type.Name.Length - 3);
            string content = string.Empty;
            List<PropertyInfo> PropertyInfoes = ListProperties(type);

            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(primitiveType))
                    continue;
                content += MappingProperty($"{ClassName}Filter", $"{MainClassName}Detail_{ClassName}FilterDTO", PropertyInfo.Name);
            }
            return content;
        }

        private void BuildList_MainDTO(Type type, string NamespaceList, string path)
        {
            string ClassName = GetClassName(type);
            BuildDTO(ClassName, type, NamespaceList, path);
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
                        BuildDTO(ClassName, listType, NamespaceList, path);
                        List<PropertyInfo> Children = ListProperties(listType);
                        foreach (PropertyInfo Child in Children)
                        {
                            string childPrimitiveType = GetPrimitiveType(Child.PropertyType);
                            if (string.IsNullOrEmpty(childPrimitiveType) && type.Name != Child.PropertyType.Name && !PropertyInfoes.Any(p => p.Name == Child.PropertyType.Name))
                            {
                                BuildDTO(ClassName, Child.PropertyType, NamespaceList, path, false);
                            }
                        }
                    }
                    else
                    {
                        BuildDTO(ClassName, PropertyInfo.PropertyType, NamespaceList, path, false);
                    }
                }
            }
        }

        private void BuildDTO(string MainClassName, Type type, string NamespaceList, string path, bool IsMainClass = true)
        {
            string ClassName = GetClassName(type);
            path = Path.Combine(path, $@"{MainClassName}Detail_{ClassName}DTO.cs");
            string content = $@"
using {Namespace}.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace {Namespace}.Controllers.{NamespaceList}
{{
    public class {MainClassName}Detail_{ClassName}DTO : DataDTO
    {{
        {DTODeclareProperty(MainClassName, type, IsMainClass)}
        public {MainClassName}Detail_{ClassName}DTO() {{}}
        public {MainClassName}Detail_{ClassName}DTO({ClassName} {ClassName})
        {{
            {DTOConstructorMapping(MainClassName, type, IsMainClass)}
        }}
    }}

    public class {MainClassName}Detail_{ClassName}FilterDTO : FilterDTO
    {{
        {DTODeclareFilter(type, IsMainClass)}
    }}
}}
";
            File.WriteAllText(path, content);
        }

        private string DTODeclareProperty(string MainClassName, Type type, bool IsMainClass = true)
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
                if (!string.IsNullOrEmpty(referenceType) && IsMainClass)
                {
                    string typeName = GetClassName(PropertyInfo.PropertyType);
                    if (typeName == MainClassName)
                        continue;
                    typeName = $"{MainClassName}Detail_{typeName}DTO";
                    content += DeclareProperty(typeName, PropertyInfo.Name);
                }
                if (!string.IsNullOrEmpty(listType) && IsMainClass)
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
        private string DTOConstructorMapping(string MainClassName, Type type, bool IsMainClass = true)
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
                if (!string.IsNullOrEmpty(referenceType) && IsMainClass)
                {
                    string typeName = GetClassName(PropertyInfo.PropertyType);
                    if (typeName == MainClassName)
                        continue;
                    content += $@"
            this.{PropertyInfo.Name} = new {ClassName}Detail_{typeName}DTO({ClassName}.{PropertyInfo.Name});
";
                }
                if (!string.IsNullOrEmpty(listType) && IsMainClass)
                {
                    string typeName = GetClassName(PropertyInfo.PropertyType.GetGenericArguments().FirstOrDefault());
                    if (typeName == MainClassName)
                        continue;
                    content += $@"
            this.{PropertyInfo.Name} = {ClassName}.{PropertyInfo.Name}?.Select(x => new {ClassName}Detail_{typeName}DTO(x)).ToList();
";
                }
            }
            return content;
        }
        private string DTODeclareFilter(Type type, bool IsMainClass = true)
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

        public {ClassName}DetailController(
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
                string filterMapping = ConvertFilterDTOToFilterEntity(ClassName, PropertyInfo.PropertyType);
                content += $@"
        [Route({ClassName}DetailRoute.SingleList{referenceType}), HttpPost]
        public async Task<List<{ClassName}Detail_{referenceType}DTO>> SingleList{referenceType}([FromBody] {ClassName}Detail_{referenceType}FilterDTO {ClassName}Detail_{referenceType}FilterDTO)
        {{
            {referenceType}Filter {referenceType}Filter = new {referenceType}Filter();
            {referenceType}Filter.Skip = 0;
            {referenceType}Filter.Take = 10;
            {referenceType}Filter.OrderBy = {referenceType}Order.Id;
            {referenceType}Filter.OrderType = OrderType.ASC;
            {referenceType}Filter.Selects = {referenceType}Select.ALL;
            {filterMapping}

            List<{referenceType}> {referenceType}s = await {referenceType}Service.List({referenceType}Filter);
            List<{ClassName}Detail_{referenceType}DTO> {ClassName}Detail_{referenceType}DTOs = {referenceType}s
                .Select(x => new {ClassName}Detail_{referenceType}DTO(x)).ToList();
            return {ClassName}Detail_{referenceType}DTOs;
        }}
";
            }
            return content;
        }
    }
}
