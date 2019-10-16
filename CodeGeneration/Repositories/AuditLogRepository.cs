
using Common;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WG.Repositories
{
    public interface IAuditLogRepository
    {
        Task<bool> Create(object newData, object oldData, string className, [CallerMemberName]string methodName = "");
    }
    public class AuditLogRepository : IAuditLogRepository
    {
        private ICurrentContext CurrentContext;
        public AuditLogRepository(ICurrentContext CurrentContext)
        {
            this.CurrentContext = CurrentContext;
        }
        public async Task<bool> Create(object newData, object oldData, string className, [CallerMemberName] string methodName = "")
        {
            return true;
        }
    }
}
