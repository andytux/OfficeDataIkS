using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SkiRentalApp.Data.Models
{
    public class Rental
    {
        [Key]
        [Required]
        public Guid RentalId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [Required]
        public int ItemId { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        [Required]
        public DateTime RentalDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}
