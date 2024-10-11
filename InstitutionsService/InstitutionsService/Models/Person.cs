using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstitutionsService.Models
{
    public class Person : BaseEntity
    {
        [Required]
        [Key]
        public long Id { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("person_code")]
        public string PersonCode { get; set; }
        [Column("birth_date")]
        public DateTime BirthDate { get; set; }
    }
}
