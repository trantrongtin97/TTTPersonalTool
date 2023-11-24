using System.ComponentModel.DataAnnotations;

namespace TTT.PersonalTool.Shared.Dtos
{
    public class RegisterDto
    {
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
        [MaxLength(DefineFieldValue.String_Lenght_500)]
        public string? TenantCode { get; set; }
    }
}
