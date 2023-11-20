namespace TTT.PersonalTool.Shared.ViewModels.Interfaces
{
    public interface IProfileViewModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ProfilePicDataUrl { get; set; }

        public Task UpdateProfile();
        public Task GetProfile();
    }
}
