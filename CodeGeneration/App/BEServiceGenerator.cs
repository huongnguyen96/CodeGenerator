using System;
using System.Collections.Generic;
using System.IO;

namespace CodeGeneration.App
{
    public class BEServiceGenerator : BEGenerator
    {
        private string Namespace { get; set; }
        private List<Type> Classes { get; set; }
        private const string Services = "Services";
        public BEServiceGenerator(string Namespace, List<Type> Classes)
        {
            if (!Directory.Exists(Services))
                Directory.CreateDirectory(Services);
            this.Namespace = Namespace;
            this.Classes = Classes;
        }

        public void Build()
        {
            foreach (Type type in Classes)
            {
                if (type.Name.Contains("_"))
                    continue;
                BuildService(type);
                BuildValidator(type);
            }
        }

        private void BuildService(Type type)
        {
            string ClassName = type.Name.Substring(0, type.Name.Length - 3);
            string path = Path.Combine(Services, "M" + ClassName, ClassName + "Service.cs");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            string content = $@"
using Common;
using {Namespace}.Entities;
using {Namespace}.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace {Namespace}.Services.M{ClassName}
{{
    public interface I{ClassName}Service : IServiceScoped
    {{
        Task<int> Count({ClassName}Filter {ClassName}Filter);
        Task<List<{ClassName}>> List({ClassName}Filter {ClassName}Filter);
        Task<{ClassName}> Get(long Id);
        Task<{ClassName}> Create({ClassName} {ClassName});
        Task<{ClassName}> Update({ClassName} {ClassName});
        Task<{ClassName}> Delete({ClassName} {ClassName});
    }}

    public class {ClassName}Service : I{ClassName}Service
    {{
        public IUOW UOW;
        public I{ClassName}Validator {ClassName}Validator;

        public {ClassName}Service(
            IUOW UOW, 
            I{ClassName}Validator {ClassName}Validator
        )
        {{
            this.UOW = UOW;
            this.{ClassName}Validator = {ClassName}Validator;
        }}
        public async Task<int> Count({ClassName}Filter {ClassName}Filter)
        {{
            int result = await UOW.{ClassName}Repository.Count({ClassName}Filter);
            return result;
        }}

        public async Task<List<{ClassName}>> List({ClassName}Filter {ClassName}Filter)
        {{
            List<{ClassName}> {ClassName}s = await UOW.{ClassName}Repository.List({ClassName}Filter);
            return {ClassName}s;
        }}

        public async Task<{ClassName}> Get(long Id)
        {{
            {ClassName} {ClassName} = await UOW.{ClassName}Repository.Get(Id);
            if ({ClassName} == null)
                return null;
            return {ClassName};
        }}

        public async Task<{ClassName}> Create({ClassName} {ClassName})
        {{
            if (!await {ClassName}Validator.Create({ClassName}))
                return {ClassName};

            try
            {{
               
                await UOW.Begin();
                await UOW.{ClassName}Repository.Create({ClassName});
                await UOW.Commit();

                await UOW.AuditLogRepository.Create({ClassName}, """", nameof({ClassName}Service));
                return await UOW.{ClassName}Repository.Get({ClassName}.Id);
            }}
            catch (Exception ex)
            {{
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof({ClassName}Service));
                throw new MessageException(ex);
            }}
        }}

        public async Task<{ClassName}> Update({ClassName} {ClassName})
        {{
            if (!await {ClassName}Validator.Update({ClassName}))
                return {ClassName};
            try
            {{
                var oldData = await UOW.{ClassName}Repository.Get({ClassName}.Id);

                await UOW.Begin();
                await UOW.{ClassName}Repository.Update({ClassName});
                await UOW.Commit();

                var newData = await UOW.{ClassName}Repository.Get({ClassName}.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof({ClassName}Service));
                return newData;
            }}
            catch (Exception ex)
            {{
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof({ClassName}Service));
                throw new MessageException(ex);
            }}
        }}

        public async Task<{ClassName}> Delete({ClassName} {ClassName})
        {{
            if (!await {ClassName}Validator.Delete({ClassName}))
                return {ClassName};

            try
            {{
                await UOW.Begin();
                await UOW.{ClassName}Repository.Delete({ClassName});
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("""", {ClassName}, nameof({ClassName}Service));
                return {ClassName};
            }}
            catch (Exception ex)
            {{
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof({ClassName}Service));
                throw new MessageException(ex);
            }}
        }}
    }}
}}
";

            System.IO.File.WriteAllText(path, content);
        }

        private void BuildValidator(Type type)
        {
            string ClassName = type.Name.Substring(0, type.Name.Length - 3);
            string path = Path.Combine(Services, "M" + ClassName, ClassName + "Validator.cs");
            string content = $@"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using {Namespace}.Entities;
using {Namespace}.Repositories;

namespace {Namespace}.Services.M{ClassName}
{{
    public interface I{ClassName}Validator : IServiceScoped
    {{
        Task<bool> Create({ClassName} {ClassName});
        Task<bool> Update({ClassName} {ClassName});
        Task<bool> Delete({ClassName} {ClassName});
    }}

    public class {ClassName}Validator : I{ClassName}Validator
    {{
        public enum ErrorCode
        {{
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }}

        private IUOW UOW;

        public {ClassName}Validator(IUOW UOW)
        {{
            this.UOW = UOW;
        }}

        public async Task<bool> ValidateId({ClassName} {ClassName})
        {{
            {ClassName}Filter {ClassName}Filter = new {ClassName}Filter
            {{
                Skip = 0,
                Take = 10,
                Id = new LongFilter {{ Equal = {ClassName}.Id }},
                Selects = {ClassName}Select.Id
            }};

            int count = await UOW.{ClassName}Repository.Count({ClassName}Filter);

            if (count == 0)
                {ClassName}.AddError(nameof({ClassName}Validator), nameof({ClassName}.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }}

        public async Task<bool> Create({ClassName} {ClassName})
        {{
            return {ClassName}.IsValidated;
        }}

        public async Task<bool> Update({ClassName} {ClassName})
        {{
            if (await ValidateId({ClassName}))
            {{
            }}
            return {ClassName}.IsValidated;
        }}

        public async Task<bool> Delete({ClassName} {ClassName})
        {{
            if (await ValidateId({ClassName}))
            {{
            }}
            return {ClassName}.IsValidated;
        }}
    }}
}}
";

            System.IO.File.WriteAllText(path, content);
        }
    }
}
