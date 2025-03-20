using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SkiRentalApp.Data.Models.ViewModels
{
	public class ItemViewModel
	{
		public int ItemId { get; set; }

		public string ItemName { get; set; }

		public decimal RentalPricePerDay { get; set; }

		public int RentalCount { get; set; } = 0;
	}
}
