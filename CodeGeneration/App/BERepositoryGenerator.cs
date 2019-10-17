using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CodeGeneration.App
{
    public class BERepositoryGenerator : BEGenerator
    {
        private string Namespace { get; set; }
        public string DbContext { get; set; }
        private List<Type> Classes { get; set; }
        private const string Repositories = "Repositories";

        public BERepositoryGenerator(string Namespace, string DbContext, List<Type> Classes)
        {
            if (!Directory.Exists(Repositories))
                Directory.CreateDirectory(Repositories);
            this.Namespace = Namespace;
            this.DbContext = DbContext;
            this.Classes = Classes;
        }

        public void Build()
        {
            BuildUOW();
            BuildAuditLog();
            BuildSystemLog();
            foreach (Type type in Classes)
            {
                string ClassName = type.Name.Substring(0, type.Name.Length - 3);
                string path = Path.Combine(Repositories, ClassName + "Repository.cs");
                string getParameters = string.Empty;
                if (ClassName.Contains("_"))
                {
                    List<string> ClassNames = ClassName.Split("_").ToList();
                    getParameters = string.Join(", ", ClassNames.Select(cn => string.Format($"long {cn}Id")));
                }
                else
                {
                    getParameters = "long Id";
                }
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
        Task<{ClassName}> Get({getParameters});
        Task<bool> Create({ClassName} {ClassName});
        Task<bool> Update({ClassName} {ClassName});
        Task<bool> Delete({ClassName} {ClassName});
        
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
            {BuildFilterProperty(type)}
            return query;
        }}
        private IQueryable<{ClassName}DAO> DynamicOrder(IQueryable<{ClassName}DAO> query,  {ClassName}Filter filter)
        {{
            switch (filter.OrderType)
            {{
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {{
                        {BuildOrderProperty(type, "ASC")}
                    }}
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {{
                        {BuildOrderProperty(type, "DESC")}
                    }}
                    break;
            }}
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }}

        private async Task<List<{ClassName}>> DynamicSelect(IQueryable<{ClassName}DAO> query, {ClassName}Filter filter)
        {{
            List <{ClassName}> {ClassName}s = await query.Select(q => new {ClassName}()
            {{
                {BuildSelectProperty(type)}
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

        {BuildGetFunction(type)}

        public async Task<bool> Create({ClassName} {ClassName})
        {{
            {ClassName}DAO {ClassName}DAO = new {ClassName}DAO();
            {BuildMappingEntityToDAO(type)}
            
            await {DbContext}.{ClassName}.AddAsync({ClassName}DAO);
            await {DbContext}.SaveChangesAsync();
            return true;
        }}

        
        {BuildUpdateFunction(type)}
        {BuildDeleteFunction(type)}
    }}
}}
";
                System.IO.File.WriteAllText(path, content);
            }
        }

        private string BuildGetFunction(Type type)
        {
            string ClassName = type.Name.Substring(0, type.Name.Length - 3);
            string content = string.Empty;
            if (ClassName.Contains("_"))
            {
                List<string> ClassNames = ClassName.Split("_").ToList();
                string condition = string.Join(" && ", ClassNames.Select(cn => string.Format($"x.{cn}Id == {cn}Id")));
                string parameters = string.Join(", ", ClassNames.Select(cn => string.Format($"long {cn}Id")));
                content += $@"
        public async Task<{ClassName}> Get({parameters})
        {{
            {ClassName} {ClassName} = await {DbContext}.{ClassName}.Where(x => {condition}).Select({ClassName}DAO => new {ClassName}()
            {{
                 {BuildMappingDAOToEntity(type)}
            }}).FirstOrDefaultAsync();
            return {ClassName};
        }}";
            }
            else
            {
                content += $@"
        public async Task<{ClassName}> Get(long Id)
        {{
            {ClassName} {ClassName} = await {DbContext}.{ClassName}.Where(x => x.Id == Id).Select({ClassName}DAO => new {ClassName}()
            {{
                 {BuildMappingDAOToEntity(type)}
            }}).FirstOrDefaultAsync();
            return {ClassName};
        }}";
            }
            return content;
        }

        private string BuildUpdateFunction(Type type)
        {
            string ClassName = type.Name.Substring(0, type.Name.Length - 3);
            string content = string.Empty;
            if (ClassName.Contains("_"))
            {
                List<string> ClassNames = ClassName.Split("_").ToList();
                string condition = string.Join(" && ", ClassNames.Select(cn => string.Format($"x.{cn}Id == {ClassName}.{cn}Id")));
                content += $@"
        public async Task<bool> Update({ClassName} {ClassName})
        {{
            {ClassName}DAO {ClassName}DAO = {DbContext}.{ClassName}.Where(x => {condition}).FirstOrDefault();
            {BuildMappingEntityToDAO(type)}
            await {DbContext}.SaveChangesAsync();
            return true;
        }}
";
            }
            else
            {
                content += $@"
        public async Task<bool> Update({ClassName} {ClassName})
        {{
            {ClassName}DAO {ClassName}DAO = {DbContext}.{ClassName}.Where(x => x.Id == {ClassName}.Id).FirstOrDefault();
            {BuildMappingEntityToDAO(type)}
            await {DbContext}.SaveChangesAsync();
            return true;
        }}
";
            }
            return content;
        }
        private string BuildDeleteFunction(Type type)
        {
            string ClassName = type.Name.Substring(0, type.Name.Length - 3);
            string content = string.Empty;
            if (ClassName.Contains("_"))
            {
                List<string> ClassNames = ClassName.Split("_").ToList();
                string condition = string.Join(" && ", ClassNames.Select(cn => string.Format($"x.{cn}Id == {ClassName}.{cn}Id")));
                content += $@"
        public async Task<bool> Delete({ClassName} {ClassName})
        {{
            {ClassName}DAO {ClassName}DAO = await {DbContext}.{ClassName}.Where(x => {condition}).FirstOrDefaultAsync();
            {DbContext}.{ClassName}.Remove({ClassName}DAO);
            await {DbContext}.SaveChangesAsync();
            return true;
        }}
";
            }
            else
            {
                bool HasDisabled = type.GetProperties().Any(p => p.Name == "Disabled");
                if (HasDisabled)
                    content += $@"
        public async Task<bool> Delete({ClassName} {ClassName})
        {{
            {ClassName}DAO {ClassName}DAO = await {DbContext}.{ClassName}.Where(x => x.Id == {ClassName}.Id).FirstOrDefaultAsync();
            {ClassName}DAO.Disabled = true;
            {DbContext}.{ClassName}.Update({ClassName}DAO);
            await {DbContext}.SaveChangesAsync();
            return true;
        }}
";
                else
                    content += $@"
        public async Task<bool> Delete({ClassName} {ClassName})
        {{
            {ClassName}DAO {ClassName}DAO = await {DbContext}.{ClassName}.Where(x => x.Id == {ClassName}.Id).FirstOrDefaultAsync();
            {DbContext}.{ClassName}.Remove({ClassName}DAO);
            await {DbContext}.SaveChangesAsync();
            return true;
        }}
";
            }
            return content;
        }
 
        private string BuildMappingDAOToEntity(Type type)
        {
            string ClassName = type.Name.Substring(0, type.Name.Length - 3);
            string PropertyString = string.Empty;
            List<PropertyInfo> PropertyInfoes = type.GetProperties().ToList();
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                if (PropertyInfo.Name == "Disabled")
                    continue;
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                if (!string.IsNullOrEmpty(primitiveType))
                {
                    PropertyString += $@"
                {PropertyInfo.Name} = {ClassName}DAO.{PropertyInfo.Name},";
                }
                string referenceType = GetReferenceType(PropertyInfo.PropertyType);
                if (!string.IsNullOrEmpty(referenceType))
                {
                    List<PropertyInfo> Children = ListProperties(PropertyInfo.PropertyType);
                    string mapping = string.Empty;
                    foreach(PropertyInfo Child in Children)
                    {
                        string childPrimitiveType = GetPrimitiveType(Child.PropertyType);
                        if (string.IsNullOrEmpty(childPrimitiveType))
                            continue;
                        mapping += $@"
                    {Child.Name} = {ClassName}DAO.{PropertyInfo.Name}.{Child.Name},";
                    }
                    PropertyString += $@"
                {PropertyInfo.Name} = {ClassName}DAO.{PropertyInfo.Name} == null ? null : new {referenceType}
                {{
                    {mapping}
                }},";
                }
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
                if (PropertyInfo.Name == "Disabled")
                {
                    PropertyString += $@"
            {ClassName}DAO.Disabled = false;";
                    continue;
                }
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(primitiveType))
                    continue;
                PropertyString += MappingProperty(ClassName + "DAO", ClassName, PropertyInfo.Name);
            }
            return PropertyString;
        }

        private string BuildFilterProperty(Type type)
        {
            string FilterString = string.Empty;
            List<PropertyInfo> PropertyInfoes = type.GetProperties().ToList();
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                if (PropertyInfo.PropertyType.FullName == typeof(bool?).FullName)
                {
                    FilterString += $@"
            if (filter.{PropertyInfo.Name}.HasValue)
                query = query.Where(q => q.{PropertyInfo.Name} == filter.{PropertyInfo.Name}.Value);";
                }

                string primitiveType = GetFilterType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(primitiveType))
                    continue;

                FilterString += $@"
            if (filter.{PropertyInfo.Name} != null)
                query = query.Where(q => q.{PropertyInfo.Name}, filter.{PropertyInfo.Name});";

            }
            if (PropertyInfoes.Any(p => p.Name == "Id"))
                FilterString += $@"
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));";
            return FilterString;
        }

        private string BuildOrderProperty(Type type, string OrderType)
        {
            string ClassName = type.Name.Substring(0, type.Name.Length - 3);
            string PropertyString = string.Empty;
            List<PropertyInfo> PropertyInfoes = type.GetProperties().ToList();
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string primitiveType = GetOrderType(PropertyInfo.PropertyType);
                if (string.IsNullOrEmpty(primitiveType))
                    continue;
                if (OrderType == "ASC")
                {
                    if (PropertyInfo.Name.EndsWith("Id") && PropertyInfo.Name.Length > 2)
                    {
                        string propertyName = PropertyInfo.Name.Substring(0, PropertyInfo.Name.Length - 2);
                        PropertyString += $@"
                        case {ClassName}Order.{propertyName}:
                            query = query.OrderBy(q => q.{propertyName}.Id);
                            break;";
                    }
                    else
                    {
                        PropertyString += $@"
                        case {ClassName}Order.{PropertyInfo.Name}:
                            query = query.OrderBy(q => q.{PropertyInfo.Name});
                            break;";
                    }
                }
                else
                {
                    if (PropertyInfo.Name.EndsWith("Id") && PropertyInfo.Name.Length > 2)
                    {
                        string propertyName = PropertyInfo.Name.Substring(0, PropertyInfo.Name.Length - 2);
                        PropertyString += $@"
                        case {ClassName}Order.{propertyName}:
                            query = query.OrderByDescending(q => q.{propertyName}.Id);
                            break;";
                    }
                    else
                    {
                        PropertyString += $@"
                        case {ClassName}Order.{PropertyInfo.Name}:
                            query = query.OrderByDescending(q => q.{PropertyInfo.Name});
                            break;";
                    }
                }
            }

            return PropertyString;
        }

        private string BuildSelectProperty(Type type)
        {
            string ClassName = type.Name.Substring(0, type.Name.Length - 3);
            string PropertyString = string.Empty;
            List<PropertyInfo> PropertyInfoes = type.GetProperties().ToList();
            foreach (PropertyInfo PropertyInfo in PropertyInfoes)
            {
                string primitiveType = GetPrimitiveType(PropertyInfo.PropertyType);
                if (!string.IsNullOrEmpty(primitiveType))
                {
                    string SelectProperty = string.Empty;
                    if (PropertyInfo.Name.EndsWith("Id") && PropertyInfo.Name.Length > 2)
                        SelectProperty = PropertyInfo.Name.Substring(0, PropertyInfo.Name.Length - 2);
                    else
                        SelectProperty = PropertyInfo.Name;
                    PropertyString += $@"
                {PropertyInfo.Name} = filter.Selects.Contains({ClassName}Select.{SelectProperty}) ? q.{PropertyInfo.Name} : default({primitiveType}),";
                }

                string referenceType = GetReferenceType(PropertyInfo.PropertyType);
                if (!string.IsNullOrEmpty(referenceType))
                {
                    List<PropertyInfo> Children = ListProperties(PropertyInfo.PropertyType);
                    string mapping = string.Empty;
                    foreach (PropertyInfo Child in Children)
                    {
                        string childPrimitiveType = GetPrimitiveType(Child.PropertyType);
                        if (string.IsNullOrEmpty(childPrimitiveType))
                            continue;
                        mapping += $@"
                    {Child.Name} = q.{PropertyInfo.Name}.{Child.Name},";
                    }
                    PropertyString += $@"
                {PropertyInfo.Name} = filter.Selects.Contains({ClassName}Select.{PropertyInfo.Name}) && q.{PropertyInfo.Name} != null ? new {referenceType}
                {{
                    {mapping}
                }} : null,";
                }
            }
            return PropertyString;
        }

        private string GetOrderType(Type type)
        {
            if (type.FullName == typeof(int).FullName)
                return "int";
            if (type.FullName == typeof(int?).FullName)
                return "int?";
            if (type.FullName == typeof(decimal).FullName)
                return "decimal";
            if (type.FullName == typeof(decimal?).FullName)
                return "decimal?";
            if (type.FullName == typeof(double).FullName)
                return "double";
            if (type.FullName == typeof(double?).FullName)
                return "double?";
            if (type.FullName == typeof(string).FullName)
                return "string";
            if (type.FullName == typeof(DateTime).FullName)
                return "DateTime";
            if (type.FullName == typeof(DateTime?).FullName)
                return "DateTime?";
            if (type.FullName == typeof(long).FullName)
                return "long";
            if (type.FullName == typeof(long?).FullName)
                return "long?";
            return null;
        }

        private void BuildUOW()
        {
            string path = Path.Combine(Repositories, "UOW.cs");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            string interfaceMethod = string.Empty;
            string classMethod = string.Empty;
            string constructor = string.Empty;
            foreach (Type type in Classes)
            {
                string ClassName = type.Name.Substring(0, type.Name.Length - 3);
                interfaceMethod += $@"
        I{ClassName}Repository {ClassName}Repository {{ get; }}
";
                classMethod += $@"
        public I{ClassName}Repository {ClassName}Repository {{ get; private set; }}
";
                constructor += $@"
            {ClassName}Repository = new {ClassName}Repository({DbContext}, CurrentContext);
";
            }
            string content = $@"
using Common;
using System.Threading.Tasks;
using CodeGeneration.Repositories.Models;

namespace {Namespace}.Repositories
{{
    public interface IUOW : IServiceScoped
    {{
        Task Begin();
        Task Commit();
        Task Rollback();
        IAuditLogRepository AuditLogRepository {{ get; }}
        ISystemLogRepository SystemLogRepository {{ get; }}
        {interfaceMethod}
    }}
    public class UOW : IUOW
    {{
        private {DbContext} {DbContext};
        public IAuditLogRepository AuditLogRepository {{ get; private set; }}
        public ISystemLogRepository SystemLogRepository {{ get; private set; }}
        {classMethod}

        public UOW({DbContext} {DbContext}, ICurrentContext CurrentContext)
        {{
            this.{DbContext} = {DbContext};
            AuditLogRepository = new AuditLogRepository(CurrentContext);
            SystemLogRepository = new SystemLogRepository(CurrentContext);
            {constructor}
        }}
        public async Task Begin()
        {{
            await {DbContext}.Database.BeginTransactionAsync();
        }}

        public Task Commit()
        {{
            {DbContext}.Database.CommitTransaction();
            return Task.CompletedTask;
        }}

        public Task Rollback()
        {{
            {DbContext}.Database.RollbackTransaction();
            return Task.CompletedTask;
        }}
    }}
}}
";
            File.WriteAllText(path, content);
        }
        private void BuildAuditLog()
        {
            string path = Path.Combine(Repositories, "AuditLogRepository.cs");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            string content = $@"
using Common;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace {Namespace}.Repositories
{{
    public interface IAuditLogRepository
    {{
        Task<bool> Create(object newData, object oldData, string className, [CallerMemberName]string methodName = """");
    }}
    public class AuditLogRepository : IAuditLogRepository
    {{
        private ICurrentContext CurrentContext;
        public AuditLogRepository(ICurrentContext CurrentContext)
        {{
            this.CurrentContext = CurrentContext;
        }}
        public async Task<bool> Create(object newData, object oldData, string className, [CallerMemberName] string methodName = """")
        {{
            return true;
        }}
    }}
}}
";
            File.WriteAllText(path, content);
        }

        private void BuildSystemLog()
        {
            string path = Path.Combine(Repositories, "SystemLogRepository.cs");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            string content = $@"
using Common;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace {Namespace}.Repositories
{{
    public interface ISystemLogRepository
    {{
        Task<bool> Create(Exception ex, string className, [CallerMemberName]string methodName = """");
    }}
    public class SystemLogRepository : ISystemLogRepository
    {{
        private ICurrentContext CurrentContext;
        public SystemLogRepository(ICurrentContext CurrentContext)
        {{
            this.CurrentContext = CurrentContext;
        }}
        public async Task<bool> Create(Exception ex, string className, [CallerMemberName] string methodName = """")
        {{
            return true;
        }}
    }}
}}
";
            File.WriteAllText(path, content);
        }
    }
}
