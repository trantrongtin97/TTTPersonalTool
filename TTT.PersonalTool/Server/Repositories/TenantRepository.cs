using Microsoft.EntityFrameworkCore;
using TTT.Framework.EfCore;
using TTT.PersonalTool.Server.DbContexts;
using TTT.PersonalTool.Shared.Dtos;
using TTT.PersonalTool.Shared.IRepositories;
using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Server.Repositories
{
    public class TenantRepository : BasicRepositoryBase<Tenant>, ITenantRepository
    {
        private readonly DbPersonalToolContext _context;
        public TenantRepository(DbPersonalToolContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> GetByCode(string tenantCode)
        {
            var tenant = await _context.Tenant.FirstOrDefaultAsync(t => t.Code == tenantCode);
            if (tenant != null)
            {
                return tenant.Id;
            }
            return 0;
        }

        public async Task<List<TenantLookUp>> GetDataLookUp()
        {
            return await _context.Tenant.Select(t => new TenantLookUp()
            {
                Id = t.Id,
                Code = t.Code
            }).ToListAsync();
        }

        public async Task<List<string>> GetTenantCodeLookUp()
        {
            return await _context.Tenant.Select(t => t.Code).ToListAsync();
        }
    }
}
