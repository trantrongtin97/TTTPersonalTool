using System.ComponentModel.DataAnnotations;

namespace TTT.PersonalTool.Shared.Dtos;

public class ItemDto
{
    public int Id { get; set; }
    [Required]
    [DataType(DataType.Text)]
    [MaxLength(DefineFieldValue.String_Lenght_100)]
    public string Name { get; set; }
    public double? Price { get; set; }
}
