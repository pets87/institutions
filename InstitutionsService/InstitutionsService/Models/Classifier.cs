using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstitutionsService.Models
{
    public class Classifier : BaseEntity
    {
        [Required]
        [Key]
        public long Id { get; set; }
        [Column("group")]
        public string Group { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("name_translation_code")]
        public string? NameTranslationCode { get; set; }
    }
}
