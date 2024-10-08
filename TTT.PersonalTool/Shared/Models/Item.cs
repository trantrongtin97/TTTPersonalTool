﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TTT.Framework.EfCore;

namespace TTT.PersonalTool.Shared.Models;

[Table("tblItem")]
public partial class Item : IDataEntity<int>
{
    [Key]
    public int Id { get; set; }
    [MaxLength(DefineFieldValue.String_Lenght_100)]
    public string Name { get; set; }
    [MaxLength(DefineFieldValue.String_Lenght_600)]
    public string Description { get; set; }
    public double? Price { get; set; }
    public DateTime? CreateDate { get; set; }
    [MaxLength(DefineFieldValue.String_Lenght_500)]
    public string? TenantCode { get; set; }

}
