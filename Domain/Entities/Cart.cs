using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class Cart
	{
		public List<CartItem> Items { get; set; } = new();

		// Adds a product to the cart with the specified quantity
		public void Add(Product product, int quantity)
		{
			if (quantity > product.Quantity)
				throw new Exception("Not enough stock."); // Prevent adding more than available

			var existing = Items.FirstOrDefault(i => i.Product.Id == product.Id);
			if (existing != null)
				existing.Quantity += quantity; // Increase quantity if already in cart
			else
				Items.Add(new CartItem { Product = product, Quantity = quantity }); // Add new item
		}
	}
}
