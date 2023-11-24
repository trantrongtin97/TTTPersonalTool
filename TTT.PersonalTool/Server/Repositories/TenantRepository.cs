using Microsoft.EntityFrameworkCore;
using Polly;
using System.Diagnostics.CodeAnalysis;
using TTT.Framework.EfCore;
using TTT.PersonalTool.Server.DbContexts;
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
    }
}
