using System.ComponentModel.DataAnnotations;
using TTT.Framework.Shared.Attributes;

namespace TTT.PersonalTool.Shared.Dtos;

public class ItemDto
{
    public int Id { get; set; }
    [Required]
    [Display(Name = "Name")]
    [TTTDataType(TTTDataType.Text)]
    [MaxLength(DefineFieldValue.String_Lenght_100)]
    public string Name { get; set; }
    [Required]
    [Display(Name = "Price")]
    [TTTDataType(TTTDataType.Double)]
    [Range(1, 100)]
    public double? Price { get; set; }
    [Required]
    [Display(Name = "CreateDate")]
    [TTTDataType(TTTDataType.Date)]
    public DateTime? CreateDate { get; set; }

    [TTTDataType(TTTDataType.Combobox)]
    [Display(Name = "TenantID")]
    public int? TenantID { get; set; }
    public List<TenantLookUp>? TenantLookUp { get; set; }
}
