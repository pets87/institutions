using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InstitutionsService.Models
{
    public class InstitutionReplication :BaseEntity
    {
        [Required]
        [Key]
        public long Id { get; set; }
        [Column("institution_id")]
        public long InstitutionId { get; set; }
        [Column("environment_classifier_id")]
        public long EnvironmentClassifierId { get; set; }
        [Column("system_classifier_id")]
        public long SystemClassifierId { get; set; }
        [Column("planned_publish_datetime")]
        public DateTime? PlannedPublishDateTime { get; set; }
        [Column("published_datetime")]
        public DateTime? PublishedDateTime { get; set; }
        [JsonIgnore]
        public Institution? Institution { get; set; }
    }
}
