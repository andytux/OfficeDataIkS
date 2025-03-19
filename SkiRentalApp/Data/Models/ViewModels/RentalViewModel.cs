namespace SkiRentalApp.Data.Models.ViewModels
{
	public class RentalViewModel
	{
		public Guid RentalId { get; set; }
		public string CustomerName { get; set; }
		public string ItemName { get; set; }
		public decimal PricePerDay { get; set; }
		public DateTime RentalDate { get; set; }
		public DateTime? ReturnDate { get; set; }
	}
}
