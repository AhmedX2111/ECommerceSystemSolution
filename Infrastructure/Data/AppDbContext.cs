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
			// TPH (Table-per-hierarchy) for Product inheritance
			modelBuilder.Entity<Product>()
				.HasDiscriminator<string>("ProductType")
				.HasValue<Cheese>("Cheese")
				.HasValue<TV>("TV")
				.HasValue<Biscuits>("Biscuits");

			// Cart - Customer (many-to-one)
			modelBuilder.Entity<Cart>()
				.HasOne(c => c.Customer)
				.WithMany() // If Customer has List<Cart> Carts, use .WithMany(cu => cu.Carts)
				.HasForeignKey(c => c.CustomerId)
				.OnDelete(DeleteBehavior.Cascade);

			// Cart - CartItems (one-to-many)
			modelBuilder.Entity<Cart>()
				.HasMany(c => c.Items)
				.WithOne(ci => ci.Cart)
				.HasForeignKey(ci => ci.CartId)
				.OnDelete(DeleteBehavior.Cascade);

			// CartItem - Product (many-to-one)
			modelBuilder.Entity<CartItem>()
				.HasOne(ci => ci.Product)
				.WithMany()
				.HasForeignKey(ci => ci.ProductId)
				.OnDelete(DeleteBehavior.Restrict);


			// Decimal precision for Price and Balance
			modelBuilder.Entity<Product>()
				.Property(p => p.Price)
				.HasColumnType("decimal(18,2)");
			modelBuilder.Entity<Customer>()
				.Property(c => c.Balance)
				.HasColumnType("decimal(18,2)");

			// Example seed:
			modelBuilder.Entity<Cheese>().HasData(
				new Cheese { Id = 1, Name = "Cheese", Price = 100, Quantity = 10, ExpiryDate = new DateTime(2025, 7, 10, 0, 0, 0, DateTimeKind.Utc), Weight = 0.2 }
			);
			modelBuilder.Entity<Biscuits>().HasData(
				new Biscuits { Id = 2, Name = "Biscuits", Price = 150, Quantity = 5, ExpiryDate = new DateTime(2025, 7, 14, 0, 0, 0, DateTimeKind.Utc), Weight = 0.7 }
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
