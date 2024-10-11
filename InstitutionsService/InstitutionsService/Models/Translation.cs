using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstitutionsService.Models
{
    public class Translation : BaseEntity
    {
        [Required]
        [Key]
        public long Id { get; set; }
        [Column("type")]
        public string Type { get; set; }
        [Column("code")]
        public string Code { get; set; }
        [Column("language")]
        public string Language { get; set; }
        [Column("text")]
        public string Text { get; set; }
    }
}
