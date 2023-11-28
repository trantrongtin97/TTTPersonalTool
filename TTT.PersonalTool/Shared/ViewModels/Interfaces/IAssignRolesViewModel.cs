using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Shared.ViewModels.Interfaces
{
    public interface IAssignRolesViewModel
    {
        public IEnumerable<User> AllUsers { get; }
        public IEnumerable<string> AllRoles { get; }
        public Task LoadAllUsers();
        public Task AssignRole(int userId, string role);
        public Task DeleteUser(int userId);
        public Task LoadAllRole();
    }
}
