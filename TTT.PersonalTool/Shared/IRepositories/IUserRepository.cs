using TTT.Framework.EfCore;
using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Contracts.IRepositories;

public interface IUserRepository : IBasicRepository<User>
{
    public Task<User?> GetByUsernameAsync(string username);
    public Task<User?> VerifyUsernamePasswordAsync(string username,string password);
}
