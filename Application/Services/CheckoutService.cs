using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
	public class CheckoutService
	{
		private readonly IShippingService _shippingService;
		public CheckoutService(IShippingService shippingService)
		{
			_shippingService = shippingService;
		}

		public void Checkout(Customer customer, Cart cart)
		{
			if (!cart.Items.Any())
				throw new Exception("Cart is empty.");

			decimal subtotal = 0;
			double totalWeight = 0;
			var shippableItems = new List<IShippable>();

			foreach (var item in cart.Items)
			{
				if (item.Product is IExpirable expirable && expirable.IsExpired())
					throw new Exception($"{item.Product.Name} is expired.");
				if (item.Quantity > item.Product.Quantity)
					throw new Exception($"{item.Product.Name} is out of stock.");
				subtotal += item.Product.Price * item.Quantity;
				if (item.Product is IShippable shippable)
				{
					shippableItems.Add(shippable);
					totalWeight += shippable.GetWeight() * item.Quantity;
				}
			}

			decimal shippingFee = totalWeight > 0 ? 30 : 0;
			decimal total = subtotal + shippingFee;

			if (customer.Balance < total)
				throw new Exception("Insufficient balance.");

			// Deduct quantities and balance
			foreach (var item in cart.Items)
				item.Product.Quantity -= item.Quantity;
			customer.Balance -= total;

			// Ship items
			if (shippableItems.Any())
				_shippingService.Ship(shippableItems);

			// Print receipt
			Console.WriteLine("** Checkout receipt **");
			foreach (var item in cart.Items)
				Console.WriteLine($"{item.Quantity}x {item.Product.Name} {item.Product.Price * item.Quantity}");
			Console.WriteLine("----------------------");
			Console.WriteLine($"Subtotal {subtotal}");
			Console.WriteLine($"Shipping {shippingFee}");
			Console.WriteLine($"Amount {total}");
			Console.WriteLine($"Customer balance {customer.Balance}");
		}
	}
}
