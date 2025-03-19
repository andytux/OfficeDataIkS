using System.ComponentModel.DataAnnotations;

namespace SkiRentalApp.Data.Models
{
    public class Customer
    {
        [Key]
        [Required]
        public Guid CustomerId { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}
