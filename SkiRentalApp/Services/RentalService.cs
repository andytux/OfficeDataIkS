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

		/// <summary>
		/// Artikel vermieten
		/// </summary>
		/// <param name="customerId"></param>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public async Task<bool> RentItemAsync(Guid customerId, int itemId, Guid employeeId)
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
				EmployeeId = employeeId,
				RentalDate = DateTime.UtcNow
			};
			try
			{

			dbContext.Rentals.Add(rental);
			await dbContext.SaveChangesAsync();
			return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		/// <summary>
		/// Artikel zurückgeben
		/// </summary>
		/// <param name="rentalId"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Alle aktiven Mietvorgänge abrufen
		/// </summary>
		/// <returns></returns>
		public async Task<List<RentalViewModel>> GetActiveRentalsAsync()
		{
			return await dbContext.Rentals
				.Include(r => r.Employee)
				.Where(r => r.ReturnDate == null)
				.Select(r => new RentalViewModel
				{
					RentalId = r.RentalId,
					CustomerName = r.Customer.CustomerName,
					ItemName = r.Item.ItemName,
					PricePerDay = r.Item.RentalPricePerDay,
					RentalDate = r.RentalDate,
					ReturnDate = r.ReturnDate,
					EmployeeName = r.Employee.UserName
				}).ToListAsync();
		}

		/// <summary>
		/// Gibt den Artikel inklusive der Anzahl der Verleihungen für einen bestimmten Zeitraum zurück
		/// </summary>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
		public async Task<List<RentalViewModel>> GetRentalsByDateRangeAsync(DateTime startDate, DateTime endDate)
		{
			return await dbContext.Rentals
				.Include(r => r.Item)
				.Where(r => r.RentalDate >= startDate && r.RentalDate <= endDate)
				.Select(r => new RentalViewModel
				{
					ItemName = r.Item.ItemName,
				})
				.ToListAsync();
		}

		/// <summary>
		/// Gibt Artikel die ausgeliehen werden können zurück
		/// </summary>
		/// <returns></returns>
		public async Task<List<ItemViewModel>> GetAvailableItemsAsync()
		{
			return await dbContext.Items
				.Where(i => !dbContext.Rentals.Any(r => r.ItemId == i.ItemId && r.ReturnDate == null)).Select(r => new ItemViewModel
				{
					ItemId = r.ItemId,
					ItemName = r.ItemName,
					RentalPricePerDay = r.RentalPricePerDay
				})
				.ToListAsync();
		}

		/// <summary>
		/// Alle ausgeliehenen Artikel zurückgeben
		/// </summary>
		/// <returns></returns>
		public async Task<List<RentalViewModel>> GetCompletedRentalsAsync()
		{
			return await dbContext.Rentals
				.Include(r => r.Customer)
				.Include(r => r.Item)
				.Include(r=> r.Employee)
				.Where(r => r.ReturnDate != null)
				.Select(r => new RentalViewModel
				{
					CustomerName = r.Customer.CustomerName,
					ItemName = r.Item.ItemName,
					RentalId= r.RentalId,
					ReturnDate = r.ReturnDate,
					RentalDate= r.RentalDate,
					PricePerDay = r.Item.RentalPricePerDay,
					EmployeeName = r.Employee.UserName
				})
				.ToListAsync();
		}
	}
}