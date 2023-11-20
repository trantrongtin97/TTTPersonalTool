using System.Net;

namespace TTT.PersonalTool.Shared.ViewModels.Interfaces
{
    public interface ISettingsViewModel
    {
        public int Id { get; set; }
        public bool Theme { get; set; }

        public Task UpdateTheme();
        public Task GetProfile();
    }
}
