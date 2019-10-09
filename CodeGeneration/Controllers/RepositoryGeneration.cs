using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CodeGeneration.Controllers
{
    public class RepositoryGeneration
    {
        private string Namespace { get; set; }
        public string DbContext { get; set; }
        private List<Type> Classes { get; set; }
        private const string Repositories = "Repositories";

        public RepositoryGeneration(string Namespace, string DbContext, List<Type> Classes)
        {
            this.Namespace = Namespace;
            this.DbContext = DbContext;
            this.Classes = Classes;
        }

        public void Build()
        {
            foreach (Type type in Classes)
            {
                string ClassName = type.Name.Substring(0, type.Name.Length - 3);
                string path = Path.Combine(Repositories, ClassName + "Repository.cs");
                string FilterProperty = BuildFilterProperty(type);
                string OrderASCProperty = BuildOrderProperty(type, "ASC");
                string OrderDESCProperty = BuildOrderProperty(type, "DESC");
                string SelectProperty = BuildSelectProperty(type);
                string MappingDAOToEntity = BuildMappingDAOToEntity(type);
                string MappingEntityToDTO = BuildMappingEntityToDAO(type);
                string content = $@"
using Common;
using {Namespace}.Entities;
using CodeGeneration.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace {Namespace}.Repositories
{{
    public interface I{ClassName}Repository
    {{
        Task<int> Count({ClassName}Filter {ClassName}Filter);
        Task<List<{ClassName}>> List({ClassName}Filter {ClassName}Filter);
        Task<{ClassName}> Get(Guid Id);
        Task<bool> Create({ClassName} {ClassName});
        Task<bool> Update({ClassName} {ClassName});
        Task<bool> Delete(Guid Id);
        
    }}
    public class {ClassName}Repository : I{ClassName}Repository
    {{
        private {DbContext} {DbContext};
        private ICurrentContext CurrentContext;
        public {ClassName}Repository({DbContext} {DbContext}, ICurrentContext CurrentContext)
        {{
            this.{DbContext} = {DbContext};
            this.CurrentContext = CurrentContext;
        }}

        private IQueryable<{ClassName}DAO> DynamicFilter(IQueryable<{ClassName}DAO> query, {ClassName}Filter filter)
        {{
            if (filter == null) 
                return query.Where(q => false);
            {FilterProperty}
            return query;
        }}
        private IQueryable<{ClassName}DAO> DynamicOrder(IQueryable<{ClassName}DAO> query,  {ClassName}Filter filter)
        {{
            switch (filter.OrderType)
            {{
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {{
                        {OrderASCProperty}
                    }}
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {{
                        {OrderDESCProperty}
                    }}
                    break;
                default:
                    query = query.OrderBy(q => q.CX);
                    break;
            }}
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }}

        private async Task<List<{ClassName}>> DynamicSelect(IQueryable<{ClassName}DAO> query, {ClassName}Filter filter)
        {{
            List <{ClassName}> {ClassName}s = await query.Select(q => new {ClassName}()
            {{
                {SelectProperty}
            }}).ToListAsync();
            return {ClassName}s;
        }}

        public async Task<int> Count({ClassName}Filter filter)
        {{
            IQueryable <{ClassName}DAO> {ClassName}DAOs = {DbContext}.{ClassName};
            {ClassName}DAOs = DynamicFilter({ClassName}DAOs, filter);
            return await {ClassName}DAOs.CountAsync();
        }}

        public async Task<List<{ClassName}>> List({ClassName}Filter filter)
        {{
            if (filter == null) return new List<{ClassName}>();
            IQueryable<{ClassName}DAO> {ClassName}DAOs = {DbContext}.{ClassName};
            {ClassName}DAOs = DynamicFilter({ClassName}DAOs, filter);
            {ClassName}DAOs = DynamicOrder({ClassName}DAOs, filter);
            var {ClassName}s = await DynamicSelect({ClassName}DAOs, filter);
            return {ClassName}s;
        }}

        public async Task<{ClassName}> Get(Guid Id)
        {{
            {ClassName} {ClassName} = await {DbContext}.{ClassName}.Where(l => l.Id == Id).Select({ClassName}DAO => new {ClassName}()
            {{
                 {MappingDAOToEntity}
            }}).FirstOrDefaultAsync();
            return {ClassName};
        }}

        public async Task<bool> Create({ClassName} {ClassName})
        {{
            {ClassName}DAO {ClassName}DAO = new {ClassName}DAO();
            {MappingEntityToDTO}
            {ClassName}DAO.Disabled = false;
            
            await {DbContext}.{ClassName}.AddAsync({ClassName}DAO);
            await {DbContext}.SaveChangesAsync();
            return true;
        }}

        public async Task<bool> Update({ClassName} {ClassName})
        {{
            {ClassName}DAO {ClassName}DAO = {DbContext}.{ClassName}.Where(b => b.Id == {ClassName}.Id).FirstOrDefault();
            {MappingEntityToDTO}
            {ClassName}DAO.Disabled = false;
            {DbContext}.{ClassName}.Update({ClassName}DAO).Property(x => x.CX).IsModified = false;
            await {DbContext}.SaveChangesAsync();
            return true;
        }}
        public async Task<bool> Delete(Guid Id)
        {{
            {ClassName}DAO {ClassName}DAO = await {DbContext}.{ClassName}.Where(x => x.Id == Id).FirstOrDefaultAsync();
            {ClassName}DAO.Disabled = true;
            {DbContext}.{ClassName}.Update({ClassName}DAO);
            await {DbContext}.SaveChangesAsync();
            return true;
        }}
    }}
}}
";
                System.IO.File.WriteAllText(path, content);
            }
        }

        private string BuildSelectProperty(Type type)
        {
            string ClassName = type.Name.Substring(0, type.Name.Length - 3);
            string PropertyString = string.Empty;
            List<PropertyInfo> PropertyInfoes = type.GetProperties().ToList();
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                if (PropertyInfo.Name == "CX")
                    continue;
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(primitiveType))
                    continue;
                string SelectProperty = string.Empty;
                if (PropertyInfo.Name.EndsWith("Id") && PropertyInfo.Name.Length > 2)
                    SelectProperty = PropertyInfo.Name.Substring(0, PropertyInfo.Name.Length - 2);
                else
                    SelectProperty = PropertyInfo.Name;
                if (string.IsNullOrEmpty(primitiveType))
                    continue;
                PropertyString += $@"
                {PropertyInfo.Name} = filter.Selects.Contains({ClassName}Select.{SelectProperty}) ? q.{PropertyInfo.Name} : default({primitiveType}),";
            }
            return PropertyString;
        }

        private string BuildMappingDAOToEntity(Type type)
        {
            string ClassName = type.Name.Substring(0, type.Name.Length - 3);
            string PropertyString = string.Empty;
            List<PropertyInfo> PropertyInfoes = type.GetProperties().ToList();
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                if (PropertyInfo.Name == "CX" || PropertyInfo.Name == "Disabled")
                    continue;
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(primitiveType))
                    continue;
                PropertyString += $@"
                {PropertyInfo.Name} = {ClassName}DAO.{PropertyInfo.Name},";
            }
            return PropertyString;
        }
        private string BuildMappingEntityToDAO(Type type)
        {
            string ClassName = type.Name.Substring(0, type.Name.Length - 3);
            string PropertyString = string.Empty;
            List<PropertyInfo> PropertyInfoes = type.GetProperties().ToList();
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                if (PropertyInfo.Name == "CX" || PropertyInfo.Name == "Disabled")
                    continue;
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(primitiveType))
                    continue;
                PropertyString += $@"
            {ClassName}DAO.{PropertyInfo.Name} = {ClassName}.{PropertyInfo.Name};";
            }
            return PropertyString;
        }

        private string BuildOrderProperty(Type type, string OrderType)
        {
            string ClassName = type.Name.Substring(0, type.Name.Length - 3);
            string PropertyString = string.Empty;
            List<PropertyInfo> PropertyInfoes = type.GetProperties().ToList();
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                if (PropertyInfo.Name == "CX")
                    continue;
                string primitiveType = GetOrderType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(primitiveType))
                    continue;
                if (OrderType == "ASC")
                    PropertyString += $@"
                        case {ClassName}Order.{PropertyInfo.Name}:
                            query = query.OrderBy(q => q.{PropertyInfo.Name});
                            break;";
                else
                    PropertyString += $@"
                        case {ClassName}Order.{PropertyInfo.Name}:
                            query = query.OrderByDescending(q => q.{PropertyInfo.Name});
                            break;";
            }

            if (OrderType == "ASC")
                PropertyString += $@"
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;";
            else
                PropertyString += $@"
                        default:
                            query = query.OrderByDescending(q => q.CX);
                            break;";

            return PropertyString;
        }

        private string BuildFilterProperty(Type type)
        {
            string FilterString = string.Empty;
            List<PropertyInfo> PropertyInfoes = type.GetProperties().ToList();
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                if (PropertyInfo.Name == "CX")
                    continue;
                if (PropertyInfo.PropertyType.Name == typeof(bool).Name)
                {
                    FilterString += $@"
            if (filter.{PropertyInfo.Name}.HasValue)
                query = query.Where(q => q.{PropertyInfo.Name} == filter.{PropertyInfo.Name}.Value);";
                }
                if (PropertyInfo.PropertyType.Name == typeof(bool?).Name)
                {
                    FilterString += $@"
            if (filter.{PropertyInfo.Name}.HasValue)
                query = query.Where(q => q.{PropertyInfo.Name}.HasValue && q.{PropertyInfo.Name}.Value == filter.{PropertyInfo.Name}.Value);";
                }

                string primitiveType = GetFilterType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(primitiveType))
                    continue;

                FilterString += $@"
            if (filter.{PropertyInfo.Name} != null)
                query = query.Where(q => q.{PropertyInfo.Name}, filter.{PropertyInfo.Name});";

            }
            return FilterString;
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
            return null;
        }

        private string GetOrderType(Type type)
        {
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
    }
}
