using System.ComponentModel.DataAnnotations;

namespace SkiRentalApp.Data.Models
{
    public class Employee
    {
        [Key]
        [Required]
        public Guid EmployeeId { get; set; } = Guid.NewGuid();

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

		public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
	}
}
