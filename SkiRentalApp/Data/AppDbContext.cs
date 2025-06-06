﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SkiRentalApp.Data.Models;
using SkiRentalApp.Services;

namespace SkiRentalApp.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		//Mitarbeiter
		public DbSet<Employee> Employees { get; set; }
		
		// Kunden
		public DbSet<Customer> Customers { get; set; }
		
		// Artikel
		public DbSet<Item> Items { get; set; }
		
		// Kategorien (Ski, Snowboard, Boots...)
		public DbSet<Category> Categories { get; set; }

		// Status (Verfügbar, Verliehen)
		public DbSet<ItemStatus> ItemStatuses { get; set; }

		// Verleih-Transaktionen
		public DbSet<Rental> Rentals { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Unique Constraint für Mitarbeiter-Benutzernamen
			modelBuilder.Entity<Employee>()
				.HasIndex(e => e.UserName)
				.IsUnique();

			// Unique Constraint für Kategorienamen
			modelBuilder.Entity<Category>()
				.HasIndex(c => c.CategoryName)
				.IsUnique();

			// Unique Constraint für Item-Status-Namen
			modelBuilder.Entity<ItemStatus>()
				.HasIndex(s => s.StatusName)
				.IsUnique();

			// Standardmäßiger Artikelstatus: "Available"
			modelBuilder.Entity<Item>()
				.Property(i => i.StatusId)
				.HasDefaultValue(1);

			// 1:n Beziehung 
			modelBuilder.Entity<Rental>()
				.HasOne(r => r.Employee) // ein Rental kann nur einen Mitarbeiter haben
				.WithMany(e => e.Rentals) //Ein Mitarbeiter kann mehrere Rentals haben
				.HasForeignKey(r => r.EmployeeId)
				.OnDelete(DeleteBehavior.Restrict);

			base.OnModelCreating(modelBuilder);
		}

		/// <summary>
		/// Fügt Testdaten in die Datenbank ein
		/// </summary>
		/// <param name="dbContext"></param>
		/// <param name="authService"></param>
		public static void SeedDatabase(AppDbContext dbContext)
		{
			//für die käufer
			if (!dbContext.Customers.Any())
			{
				var customers = new List<Customer>
				{
					new Customer { CustomerName = "Peter Maier", Email = "peter@example.com", Phone = "1234567890", BirthDate = new DateTime(1990, 5, 10) },
					new Customer { CustomerName = "Lara Schmidt", Email = "Lara@example.com", Phone = "0987654321", BirthDate = new DateTime(1985, 7, 20) }
				};
				dbContext.Customers.AddRange(customers);
			}

			//für die Kategorien
			if (!dbContext.Categories.Any())
			{
				var categories = new List<Category>
				{
					new Category { CategoryName = "Ski" },
					new Category { CategoryName = "Snowboard" },
					new Category {CategoryName = "Boots"}
				};
				dbContext.Categories.AddRange(categories);
				dbContext.SaveChanges();
			}

			//für Artikelstatuses
			if (!dbContext.ItemStatuses.Any())
			{
				var availableStatus = new ItemStatus { StatusName = "Available" };
				var rentedStatus = new ItemStatus { StatusName = "Rented" };

				dbContext.ItemStatuses.AddRange(availableStatus, rentedStatus);
				dbContext.SaveChanges();
			}

			//für Artikel
			if (!dbContext.Items.Any())
			{
				var skiCategory = dbContext.Categories.FirstOrDefault(c => c.CategoryName == "Ski");
				var snowboardCategory = dbContext.Categories.FirstOrDefault(c => c.CategoryName == "Snowboard");
				var bootsCategory = dbContext.Categories.FirstOrDefault(c => c.CategoryName == "Boots");
				
				var availableStatus = dbContext.ItemStatuses.FirstOrDefault(s => s.StatusName == "Available");

				if (skiCategory != null && snowboardCategory != null && availableStatus != null && bootsCategory != null)
				{
					var items = new List<Item>
					{
						new Item { ItemName = "Atomic Ski", RentalPricePerDay = 25.00m, CategoryId = skiCategory.CategoryId, StatusId = availableStatus.StatusId },
						new Item { ItemName = "Burton Snowboard", RentalPricePerDay = 30.00m, CategoryId = snowboardCategory.CategoryId, StatusId = availableStatus.StatusId },
						new Item { ItemName = "Head Ski", RentalPricePerDay = 45.00m, CategoryId = skiCategory.CategoryId, StatusId = availableStatus.StatusId },
						new Item { ItemName = "Head Boots", RentalPricePerDay = 15.00m, CategoryId = bootsCategory.CategoryId, StatusId = availableStatus.StatusId },
						new Item { ItemName = "Volcom Snowboard", RentalPricePerDay = 45.00m, CategoryId = snowboardCategory.CategoryId, StatusId = availableStatus.StatusId },
					};
					dbContext.Items.AddRange(items);
				}
			}

			dbContext.SaveChanges();
		}
	}
}
