using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstitutionsService.Models
{
    public class Address : BaseEntity
    {
        [Required]
        [Key]
        public long Id { get; set; }
        [Column("country")]
        public string? Country { get; set; }
        [Column("county")]
        public string? County { get; set; }
        [Column("city")]
        public string? City { get; set; }
        [Column("street")]
        public string? Street { get; set; }
        [Column("house")]
        public string? House { get; set; }
        [Column("apartment")]
        public string? Apartment { get; set; }
        [Column("postal_code")]
        public string? PostalCode { get; set; }

        [Column("address_text")]
        public string AddressText { get; set; }
    }
}
