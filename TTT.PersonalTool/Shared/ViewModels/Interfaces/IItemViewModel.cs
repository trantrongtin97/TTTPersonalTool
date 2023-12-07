using TTT.PersonalTool.Shared.Dtos;
using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Shared.ViewModels.Interfaces;

public interface IItemViewModel
{
    public IEnumerable<ItemDto> AllItems { get; }
    public Task GetListItem();
    public Task DeleteItem(int id);
}
