using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstitutionsService.Models
{
    public class InstitutionContact :BaseEntity
    {
        [Required]
        [Key]
        public long Id { get; set; }
        [Column("institution_id")]
        public long InstitutionId { get; set; }
        [Column("person_id")]
        public long PersonId { get; set; }
        [Column("telephone")]
        public string Telephone { get; set; }
        [Column("email")]
        public string Email { get; set; }
    }
}
