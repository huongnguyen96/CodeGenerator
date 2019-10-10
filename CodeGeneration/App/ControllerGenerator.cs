using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeGeneration.App
{
    public partial class ControllerGenerator : Generator
    {
        private string Namespace { get; set; }
        public string RootRoute { get; set; }
        private List<Type> Classes { get; set; }

        public const string Controllers = "Controllers";
        public ControllerGenerator(string Namespace, string RootRoute, List<Type> Classes)
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

                string ClassName = type.Name.Substring(0, type.Name.Length - 3);
                BuildListPage(type);
            }
        }
     
        private void BuildListPage(Type type)
        {
            string ClassName = type.Name.Substring(0, type.Name.Length - 3);
            string NamespaceList = BuildNamespace(ClassName) + "." + BuildNamespace(ClassName) + "List";
            string RouteList = BuildFolderPath(ClassName) + "/" + BuildFolderPath(ClassName) + "-list";
            string path = Path.Combine(Controllers, RouteList);
            string controllerPath = Path.Combine(path, ClassName + "ListController.cs");
            Directory.CreateDirectory(path);
            BuilListDTO(type, NamespaceList, path);

            string content = $@"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using {Namespace}.Services.M{ClassName}Service;
using Microsoft.AspNetCore.Mvc;
using {Namespace}.Entities;

namespace {Namespace}.Controllers.{NamespaceList}
{{
    public class {ClassName}ListRoute : Root
    {{
        public const string FE = ""{RouteList}"";
        private const string Default = Base + FE;
        public const string Count = Default + ""/count"";
        public const string List = Default + ""/list"";
        public const string Get = Default + ""/get"";
    }}

    public class {ClassName}ListController : ApiController
    {{
        private I{ClassName}Service {ClassName}Service;

        public {ClassName}ListController(
            I{ClassName}Service {ClassName}Service
        )
        {{
            this.{ClassName}Service = {ClassName}Service;
        }}

        [Route({ClassName}ListRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] {ClassName}List_{ClassName}FilterDTO {ClassName}List_{ClassName}FilterDTO)
        {{
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            {ClassName}Filter {ClassName}Filter = ConvertFilterDTOtoFilterBO({ClassName}List_{ClassName}FilterDTO);

            return await {ClassName}Service.Count({ClassName}Filter);
        }}

        [Route({ClassName}ListRoute.List), HttpPost]
        public async Task<List<{ClassName}List_{ClassName}DTO>> List([FromBody] {ClassName}List_{ClassName}FilterDTO {ClassName}List_{ClassName}FilterDTO)
        {{
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            {ClassName}Filter {ClassName}Filter = ConvertFilterDTOtoFilterEntity({ClassName}List_{ClassName}FilterDTO);

            List<{ClassName}> {ClassName}s = await {ClassName}Service.List(CustomerFilter);

            return {ClassName}s.Select(c => new {ClassName}List_{ClassName}DTO(c)).ToList();
        }}

        [Route({ClassName}ListRoute.Get), HttpPost]
        public async Task<{ClassName}List_{ClassName}DTO> Get([FromBody]{ClassName}List_{ClassName}DTO {ClassName}List_{ClassName}DTO)
        {{
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            {ClassName} {ClassName} = await {ClassName}Service.Get({ClassName}List_{ClassName}DTO.Id);
            return new {ClassName}List_{ClassName}DTO(Customer);
        }}


        public {ClassName}Filter ConvertFilterDTOtoFilterEntity({ClassName}List_{ClassName}FilterDTO {ClassName}Filter_{ClassName}DTO)
        {{
            {ClassName}Filter {ClassName}Filter = new {ClassName}Filter();
            return {ClassName}Filter;
        }}
    }}
}}
";
            System.IO.File.WriteAllText(controllerPath, content);
        }

        private string BuildFolderPath(string ClassName)
        {
            List<string> split = Regex.Split(ClassName, @"(?<!^)(?=[A-Z])").Select(s => s.ToLower().Trim()).ToList();
            string result = string.Join("-", split);
            return result;
        }
        private string BuildNamespace(string ClassName)
        {
            List<string> split = Regex.Split(ClassName, @"(?<!^)(?=[A-Z])").Select(s => s.ToLower().Trim()).ToList();
            string result = string.Join("_", split);
            return result;
        }
        private string ConvertDTOTOEntity(Type type)
        {
            string ClassName = type.Name.Substring(0, type.Name.Length - 3);
            string content = string.Empty;
            List<PropertyInfo> PropertyInfoes = type.GetProperties().ToList();
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                content += $@"
            {ClassName}.{PropertyInfo.Name} = 
";
            }
            return content;
        }

        private string ConvertFilterDTOToFilterEntity(Type type)
        {
            string ClassName = type.Name.Substring(0, type.Name.Length - 3);
            string content = string.Empty;
            List<PropertyInfo> PropertyInfoes = type.GetProperties().ToList();

            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {

            }
            return content;
        }
    }
}
