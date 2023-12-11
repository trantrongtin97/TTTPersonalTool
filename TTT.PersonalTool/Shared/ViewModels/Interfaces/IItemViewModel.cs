using System.Net;
using TTT.PersonalTool.Shared.Dtos;
using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Shared.ViewModels.Interfaces;

public interface IItemViewModel
{
    public IEnumerable<ItemDto> AllItems { get; }
    public Dictionary<string, object?> DicDataLookUp { get; }
    public Task GetListItem();
    public Task DeleteItem(int id);
    public Task<HttpStatusCode> UpdateItem(ItemDto itemDto);
    public Task<HttpStatusCode> CreateItem(ItemDto itemDto);
    public Task GetDataLookUp(int id);
}
