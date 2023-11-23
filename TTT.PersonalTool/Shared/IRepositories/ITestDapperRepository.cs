using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Shared.IRepositories
{
    public interface ITestDapperRepository 
    {
        Task<User> GetById(int id);
    }
}
