using InstitutionsService.Models;

namespace InstitutionsService.RabbitMQ.Dto
{
    /// <summary>
    /// Dto for replications. Institution model have more data that is needed for replication, so create smaller dto for replication.
    /// </summary>
    public class InstitutionDto
    {
        public long Id { get; set; }     
        public string Name { get; set; }
        public string RegCode { get; set; }
        public string? KMKR { get; set; }
        public Classifier? Type { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public Address? Address { get; set; }
        public List<Translation>? Translations { get; set; }
    }
}
