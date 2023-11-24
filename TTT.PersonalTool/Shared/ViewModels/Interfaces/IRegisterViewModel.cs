using TTT.PersonalTool.Shared.Enums;

namespace TTT.PersonalTool.Shared.ViewModels.Interfaces
{
    public interface IRegisterViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ReenterPassword { get; set; }
        public string TenantCode { get; set; }
        public Task<UserState> RegisterUser();
    }
}
