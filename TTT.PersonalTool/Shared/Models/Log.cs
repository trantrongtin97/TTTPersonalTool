using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TTT.PersonalTool.Shared.Models.Interfaces;

namespace TTT.PersonalTool.Shared.Models;

[Table("tblLog")]
public partial class Log : IEntity<int>
{
    [Key]
    public int Id { get; set; }
    [MaxLength(DefineFieldValue.String_Lenght_200)]
    public string? Level { get; set; }
    [MaxLength(DefineFieldValue.String_Lenght_200)]
    public string? EventName { get; set; }
    [MaxLength(DefineFieldValue.String_Lenght_200)]
    public string? Source { get; set; }
    public string? ExceptionMessage { get; set; }
    public string? StackTrace { get; set; }
    public DateTime? CreateDate { get; set; }
    public int? UserId { get; set; }
}
