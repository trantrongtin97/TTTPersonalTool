using TTT.Framework.ServiceExtentions;
using TTT.PersonalTool.Shared.Dtos;
using TTT.PersonalTool.Shared.Services;
using TTT.PersonalTool.Shared.ViewModels.Interfaces;

namespace TTT.PersonalTool.Shared.ViewModels
{
    public class ItemViewModel : IItemViewModel
    {
        //public int Id { get; set; }
        //[TTTStringValidator(Requied = true, MaximumSize = DefineFieldValue.String_Lenght_200, Display = "Name Item")]
        //public string Name { get; set; }
        //[TTTNumberValidator(Requied = true, Display = "Price")]
        //public double? Price { get; set; }
        public IEnumerable<ItemDto> AllItems { get; private set; } = new List<ItemDto>();
        private readonly HttpClient _httpClient;
        private readonly IAccessTokenService _accessTokenService;

        public ItemViewModel()
        {
        }
        public ItemViewModel(HttpClient httpClient, IAccessTokenService accessTokenService)
        {
            _httpClient = httpClient;
            _accessTokenService = accessTokenService;
        }

        public async Task GetListItem()
        {
            var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
            AllItems = await _httpClient.GetAsync<List<ItemDto>>($"item/getallitem", jwtToken);
        }

        public async Task DeleteItem(int id)
        {
            var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
            await _httpClient.DeleteAsync($"item/deleteitem/{id}", jwtToken);
        }
    }
}
