using TTT.PersonalTool.Shared.Dtos;
using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Shared.ViewModels.Interfaces;

public interface IItemViewModel
{
    //public int Id { get; set; }
    //public string Name { get; set; }
    //public double? Price { get; set; }
    public IEnumerable<ItemDto> AllItems { get; }
    public Task GetListItem();
    public Task DeleteItem(int id);
}
