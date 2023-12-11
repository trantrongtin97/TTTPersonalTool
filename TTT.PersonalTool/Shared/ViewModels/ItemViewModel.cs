using System.Net;
using TTT.Framework.ServiceExtentions;
using TTT.PersonalTool.Shared.Dtos;
using TTT.PersonalTool.Shared.Services;
using TTT.PersonalTool.Shared.ViewModels.Interfaces;

namespace TTT.PersonalTool.Shared.ViewModels
{
    public class ItemViewModel : IItemViewModel
    {
        public IEnumerable<ItemDto> AllItems { get; private set; } = new List<ItemDto>();
        public Dictionary<string, object?> DicDataLookUp { get; private set; } = new Dictionary<string, object?>();
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

        public async Task<HttpStatusCode> UpdateItem(ItemDto itemDto)
        {
            var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
            return await _httpClient.PutAsync<HttpStatusCode>($"item/updateitem",itemDto, jwtToken);
        }

        public async Task<HttpStatusCode> CreateItem(ItemDto itemDto)
        {
            var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
            return await _httpClient.PostAsync<HttpStatusCode>($"item/createitem", itemDto, jwtToken);
        }

        public async Task GetDataLookUp(int id)
        {
            var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
            DicDataLookUp = await _httpClient.GetAsync<Dictionary<string, object?>>($"item/getdataloopup", jwtToken);
        }
    }
}
