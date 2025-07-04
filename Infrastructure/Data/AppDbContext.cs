using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<Product> Products { get; set; }
		public DbSet<Cheese> Cheeses { get; set; }
		public DbSet<TV> TVs { get; set; }
		public DbSet<Biscuits> Biscuits { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<CartItem> CartItems { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Configure entity relationships and seed data here
			// Example seed:
			modelBuilder.Entity<Cheese>().HasData(
				new Cheese { Id = 1, Name = "Cheese", Price = 100, Quantity = 10, ExpiryDate = DateTime.UtcNow.AddDays(5), Weight = 0.2 }
			);
			modelBuilder.Entity<Biscuits>().HasData(
				new Biscuits { Id = 2, Name = "Biscuits", Price = 150, Quantity = 5, ExpiryDate = DateTime.UtcNow.AddDays(10), Weight = 0.7 }
			);
			modelBuilder.Entity<TV>().HasData(
				new TV { Id = 3, Name = "TV", Price = 5000, Quantity = 3, Weight = 10 }
			);
			modelBuilder.Entity<Customer>().HasData(
				new Customer { Id = 1, Name = "Ahmed", Balance = 1000 }
			);
		}
	}
}
