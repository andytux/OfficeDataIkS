using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SkiRentalApp.Data.Models
{
    public class Item
    {
        [Key]
        [Required]
        public int ItemId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ItemName { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal RentalPricePerDay { get; set; }

        [Required]
        public int RentalCount { get; set; } = 0;

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Required]
        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public ItemStatus Status { get; set; }
    }
}

