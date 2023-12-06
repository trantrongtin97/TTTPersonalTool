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
    public int? TenantID { get; set; } = 2;

    [DataLookUp("TenantID")]
    public List<TenantLookUp>? TenantLookUp { get; set; } = new List<TenantLookUp>()
    {
        new TenantLookUp()
        {
            Id = 1,
            Code = "Code1"
        },
        new TenantLookUp()
        {
            Id = 2,
            Code = "Code2"
        }
    };

    [TTTDataType(TTTDataType.Dropdowlist)]
    [Display(Name = "TenantString")]
    public string TenantString { get; set; }

    [DataLookUp("TenantString")]
    public List<string>? TenantStringLookUp { get; set; } = new List<string>()
    {
        "Code1","Code2"
    };

    [Display(Name = "IsActive")]
    [TTTDataType(TTTDataType.CheckBox)]
    public bool IsActive { get; set; } = false;
}
