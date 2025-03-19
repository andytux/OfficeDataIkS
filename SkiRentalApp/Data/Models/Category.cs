using System.ComponentModel.DataAnnotations;

namespace SkiRentalApp.Data.Models
{
    public class Category
    {
        [Key]
        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
