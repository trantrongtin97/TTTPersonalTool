using System.ComponentModel.DataAnnotations;
using TTT.Framework.Shared.Attributes;

namespace TTT.PersonalTool.Shared.Dtos;

public class ItemDto
{
    public int Id { get; set; }

    [Display(Name = "Name")]
    [TTTDataType(TTTDataType.MultilineText)]
    [MaxLength(DefineFieldValue.String_Lenght_100)]
    public string Name { get; set; }

    [Display(Name = "Price")]
    [TTTDataType(TTTDataType.Double)]
    public double? Price { get; set; }

    [Display(Name = "CreateDate")]
    [TTTDataType(TTTDataType.Time)]
    public DateTime CreateDate { get; set; }

    [TTTDataType(TTTDataType.Combobox)]
    [Display(Name = "TenantID")]
    public int TenantID { get; set; }

    [DataLookUp("TenantID")]
    public List<TenantLookUp>? TenantLookUp { get; set; }

    [TTTDataType(TTTDataType.Dropdowlist)]
    [Display(Name = "TenantCode")]
    public string TenantCode { get; set; }

    [DataLookUp("TenantCode")]
    public List<string>? TenantCodeLookUp { get; set; }

    //[Display(Name = "IsActive")]
    //[TTTDataType(TTTDataType.CheckBox)]
    //public bool IsActive { get; set; } = false;
}
