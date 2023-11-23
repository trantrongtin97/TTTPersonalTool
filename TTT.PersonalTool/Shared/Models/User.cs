using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TTT.Framework.EfCore;

namespace TTT.PersonalTool.Shared.Models;

[Table("tblUser")]
public partial class User : IEntity<int>
{
    public User() { }
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(DefineFieldValue.String_Lenght_50)]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [MaxLength(DefineFieldValue.String_Lenght_200)]
    public string? FirstName { get; set; }
    [MaxLength(DefineFieldValue.String_Lenght_200)]
    public string? LastName { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public int? Theme { get; set; }
    public DateTime? CreatedDate { get; set; }
    [MaxLength(DefineFieldValue.String_Lenght_50)]
    public string? Role { get; set; }
}
