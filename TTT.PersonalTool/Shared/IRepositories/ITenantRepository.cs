using TTT.Framework.EfCore;
using TTT.PersonalTool.Shared.Dtos;
using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Shared.IRepositories
{
    public interface ITenantRepository : IBasicRepository<Tenant>
    {
        public Task<int> GetByCode(string tenantCode);
        public Task<List<TenantLookUp>> GetDataLookUp();
        public Task<List<string>> GetTenantCodeLookUp();
    }
}
