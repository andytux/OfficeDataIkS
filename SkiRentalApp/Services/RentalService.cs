using SkiRentalApp.Data.Models;
using SkiRentalApp.Data;
using Microsoft.EntityFrameworkCore;
using SkiRentalApp.Data.Models.ViewModels;

namespace SkiRentalApp.Services
{
	public class RentalService
	{
		private readonly AppDbContext dbContext;

		public RentalService(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		// Artikel vermieten
		public async Task<bool> RentItemAsync(Guid customerId, int itemId)
		{
			bool isAlreadyRented = await dbContext.Rentals.AnyAsync(r => r.ItemId == itemId && r.ReturnDate == null);

			if (isAlreadyRented)
			{
				return false;
			}

			var item = await dbContext.Items.FirstOrDefaultAsync(i => i.ItemId == itemId);

			if (item == null)
				return false;

			var rental = new Rental
			{
				CustomerId = customerId,
				ItemId = itemId,
				RentalDate = DateTime.UtcNow
			};

			dbContext.Rentals.Add(rental);
			await dbContext.SaveChangesAsync();
			return true;
		}

		// Artikel zurückgeben
		public async Task<bool> ReturnItemAsync(Guid rentalId)
		{
			var rental = await dbContext.Rentals.Include(r => r.Item).FirstOrDefaultAsync(r => r.RentalId == rentalId);

			if (rental == null || rental.ReturnDate != null)
			{
				return false;
			}

			rental.ReturnDate = DateTime.UtcNow;

			var availableStatus = await dbContext.ItemStatuses.FirstOrDefaultAsync(s => s.StatusName == "Available");

			if (availableStatus != null)
			{
				rental.Item.StatusId = availableStatus.StatusId;
			}

			await dbContext.SaveChangesAsync();

			return true;
		}

		// Alle aktiven Mietvorgänge abrufen
		public async Task<List<RentalViewModel>> GetActiveRentalsAsync()
		{
			return await dbContext.Rentals
				.Where(r => r.ReturnDate == null)
				.Select(r => new RentalViewModel
				{
					RentalId = r.RentalId,
					CustomerName = r.Customer.CustomerName,
					ItemName = r.Item.ItemName,
					PricePerDay = r.Item.RentalPricePerDay,
					RentalDate = r.RentalDate,
					ReturnDate = r.ReturnDate
				}).ToListAsync();
		}

		public async Task<List<Rental>> GetRentalsByDateRangeAsync(DateTime startDate, DateTime endDate)
		{
			return await dbContext.Rentals
				.Include(r => r.Item)
				.Include(r => r.Customer)
				.Where(r => r.RentalDate >= startDate && r.RentalDate <= endDate)
				.ToListAsync();
		}

		public async Task<List<Item>> GetAvailableItemsAsync()
		{
			return await dbContext.Items
				.Where(i => !dbContext.Rentals.Any(r => r.ItemId == i.ItemId && r.ReturnDate == null))
				.ToListAsync();
		}

		public async Task<List<Rental>> GetCompletedRentalsAsync()
		{
			return await dbContext.Rentals
				.Include(r => r.Customer)
				.Include(r => r.Item)
				.Where(r => r.ReturnDate != null)
				.ToListAsync();
		}
	}
}