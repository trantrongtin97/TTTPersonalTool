using System.ComponentModel.DataAnnotations;
using TTT.Framework.Attributes;

namespace TTT.PersonalTool.Shared.Dtos;

public class ItemDto
{
    public int Id { get; set; }
    [Required]
    [DataType(DataType.Text)]
    [MaxLength(DefineFieldValue.String_Lenght_100)]
    public string Name { get; set; }
    [DataType(DataType.Currency)]
    [Range(1,10)]
    public double? Price { get; set; }
}
