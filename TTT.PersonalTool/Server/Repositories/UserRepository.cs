using Microsoft.EntityFrameworkCore;
using TTT.Framework.EfCore;
using TTT.PersonalTool.Contracts.IRepositories;
using TTT.PersonalTool.Server.DbContexts;
using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Server.Repositories;

public class UserRepository : BasicRepositoryBase<User>, IUserRepository
{
    private readonly DbPersonalToolContext _context;
    public UserRepository(DbPersonalToolContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<List<User>> GetListAsync()
    {
        return await _context.Users.Select(t => new User
        {
            Id = t.Id,
            Username = t.Username,
            Password = "xxx",
            Role = t.Role,
            Theme = t.Theme
        }).ToListAsync();
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users.Where(x => x.Username == username).Select(t => new User
        {
            Id = t.Id,
            Username = t.Username,
            Role = t.Role,
            Theme = t.Theme
        }).FirstOrDefaultAsync();
    }

    public async Task<User?> VerifyUsernamePasswordAsync(string username, string password)
    {
        var a = _context.Users.ToList();
        return await _context.Users.Where(x => x.Username == username && x.Password == password).Select(t => new User
        {
            Id = t.Id,
            Username = t.Username,
            Role = t.Role,
            Theme = t.Theme,
            TenantCode = t.TenantCode
        }).FirstOrDefaultAsync();
    }
}
