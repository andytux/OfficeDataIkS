using System.ComponentModel.DataAnnotations;

namespace SkiRentalApp.Data.Models.ViewModels
{
	public class CustomerViewModel
	{
		public Guid CustomerId { get; set; }

		[Required]
		[MaxLength(100)]
		public string CustomerName { get; set; }

		[Required]
		[EmailAddress]
		[MaxLength(255)]
		public string Email { get; set; }

		[Required]
		[MaxLength(20)]
		public string Phone { get; set; }

		[Required]
		public DateTime BirthDate { get; set; }
	}
}
