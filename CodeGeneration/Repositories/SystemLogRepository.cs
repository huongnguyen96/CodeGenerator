
using Common;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace WG.Repositories
{
    public interface ISystemLogRepository
    {
        Task<bool> Create(Exception ex, string className, [CallerMemberName]string methodName = "");
    }
    public class SystemLogRepository : ISystemLogRepository
    {
        private ICurrentContext CurrentContext;
        public SystemLogRepository(ICurrentContext CurrentContext)
        {
            this.CurrentContext = CurrentContext;
        }
        public async Task<bool> Create(Exception ex, string className, [CallerMemberName] string methodName = "")
        {
            return true;
        }
    }
}
