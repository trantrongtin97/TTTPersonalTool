using Microsoft.EntityFrameworkCore;
using TTT.Framework.EfCore;
using TTT.PersonalTool.Server.DbContexts;
using TTT.PersonalTool.Shared.IRepositories;
using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Server.Repositories
{
    public class ItemRepository : BasicRepositoryBase<Item>, IItemRepository
    {
        public ItemRepository(DbPersonalToolContext context) : base(context)
        {
        }
    }
}
