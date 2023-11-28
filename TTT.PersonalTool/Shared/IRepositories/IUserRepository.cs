using TTT.Framework.EfCore;
using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Shared.IRepositories;

public interface IUserRepository : IBasicRepository<User>
{
    public Task<User?> GetByUsernameAsync(string username);
    public Task<User?> VerifyUsernamePasswordAsync(string username,string password);
    public Task<List<User>> GetListDependUserAsync(int userid);
}
