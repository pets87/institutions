using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InstitutionsService.Models
{
    public class BaseEntity
    {

        [JsonIgnore]
        [Column("created_datetime")]
        public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        [Column("created_by")]
        public string? CreatedBy { get; set; }

        [JsonIgnore]
        [Column("updated_datetime")]
        public DateTime? UpdatedDateTime { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        [Column("updated_by")]
        public string? UpdatedBy { get; set; }

        [JsonIgnore]
        [Column("deleted_datetime")]
        public DateTime? DeletedDateTime { get; set; }
        [JsonIgnore]
        [Column("deleted_by")]
        public string? DeletedBy { get; set; }
    }
}
