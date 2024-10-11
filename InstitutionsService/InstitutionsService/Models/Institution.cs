using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstitutionsService.Models
{
    [Index(nameof(RegCode), IsUnique = true)]
    public class Institution : BaseEntity
    {
        [Required]
        [Key]
        public long Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("name_translation_code")]
        public string? NameTranslationCode { get; set; }
        [Column("reg_code")]
        public string RegCode { get; set; }
        [Column("kmkr")]
        public string? KMKR { get; set; }
        [Column("address_id")]
        public long AddressId { get; set; }
        [Column("type_classifier_id")]
        public long TypeClassifierId { get; set; }
        [Column("valid_from")]
        public DateTime ValidFrom { get; set; }
        [Column("valid_to")]
        public DateTime? ValidTo { get; set; }

        public Address? Address { get; set; }
        public List<Translation>? Translations { get; set; }
        public List<InstitutionReplication>? Replications { get; set; }
    }
}
