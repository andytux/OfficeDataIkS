using SkiRentalApp.Data.Models;
using SkiRentalApp.Data;
using Microsoft.EntityFrameworkCore;
using SkiRentalApp.Data.Models.ViewModels;

namespace SkiRentalApp.Services
{
	public class CustomerService
	{
		private readonly AppDbContext dbContext;

		public CustomerService(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task AddCustomerAsync(Customer customer)
		{
			dbContext.Customers.Add(customer);
			await dbContext.SaveChangesAsync();
		}

		public async Task DeleteCustomerAsync(Guid customerId)
		{
			var customer = await dbContext.Customers.FindAsync(customerId);
			
			if (customer != null)
			{
				dbContext.Customers.Remove(customer);
				await dbContext.SaveChangesAsync();
			}
		}

		public async Task<List<CustomerViewModel>> GetCustomersAsync()
		{
			return await dbContext.Customers
				.Select(c => new CustomerViewModel
				{
					CustomerId = c.CustomerId,
					CustomerName = c.CustomerName,
					Email = c.Email,
					Phone = c.Phone,
					BirthDate = c.BirthDate
				}).ToListAsync();
		}

		public async Task AddCustomerAsync(CustomerViewModel customerViewModel)
		{
			var customer = new Customer
			{
				CustomerId = Guid.NewGuid(),
				CustomerName = customerViewModel.CustomerName,
				Email = customerViewModel.Email,
				Phone = customerViewModel.Phone,
				BirthDate = customerViewModel.BirthDate
			};

			dbContext.Customers.Add(customer);
			await dbContext.SaveChangesAsync();
		}

	}
}