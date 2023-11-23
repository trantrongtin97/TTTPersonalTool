using Dapper;
using System.Data;
using TTT.Framework.DbExtensions;
using TTT.PersonalTool.Server.DbContexts;
using TTT.PersonalTool.Shared.IRepositories;
using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Server.Repositories
{
    /// <summary>
    /// Repository Example use dapper to access data
    /// this repository has't inject to server in severcollection
    /// </summary>
    public class TestDapperRepository : TTTDapperRepository<DbPersonalToolContext>, ITestDapperRepository
    {
        public TestDapperRepository(DbPersonalToolContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetById(int id)
        {
            var para = new DynamicParameters();
            para.Add("UserID", id, DbType.Int64, ParameterDirection.Input);
            return await QuerySingleOrDefault<User>($"select Id,Username,'xxx' AS Password,FirstName,LastName from tblUser where Id = @UserID", para, CommandType.Text);
        }
    }
}
