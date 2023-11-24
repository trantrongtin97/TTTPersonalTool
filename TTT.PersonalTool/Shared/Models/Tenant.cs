using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TTT.Framework.EfCore;

namespace TTT.PersonalTool.Shared.Models
{
    [Table("tblTenant")]
    public class Tenant : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(DefineFieldValue.String_Lenght_500)]
        public string Code { get; set; }
        [Required]
        public int UserID { get; set; }
    }
}
