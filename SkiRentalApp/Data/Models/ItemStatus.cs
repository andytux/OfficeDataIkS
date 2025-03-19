using System.ComponentModel.DataAnnotations;

namespace SkiRentalApp.Data.Models
{
    public class ItemStatus
    {
        [Key]
        [Required]
        public int StatusId { get; set; }

        [Required]
        [MaxLength(50)]
        public string StatusName { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
