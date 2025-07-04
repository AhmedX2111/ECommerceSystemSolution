using Application.DTOs;
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

		public CheckoutResultDto Checkout(Customer customer, List<CartItem> cartItems)
		{
			if (cartItems == null || !cartItems.Any())
				throw new Exception("Cart is empty.");

			decimal subtotal = 0;
			double totalWeight = 0;
			var shippableItems = new List<IShippable>();
			var shipmentNotice = new List<string>();
			var receipt = new List<string>();

			foreach (var item in cartItems)
			{
				if (item.Product is IExpirable expirable && expirable.IsExpired())
					throw new Exception($"{item.Product.Name} is expired.");
				if (item.Quantity > item.Product.Quantity)
					throw new Exception($"{item.Product.Name} is out of stock.");
				subtotal += item.Product.Price * item.Quantity;
				receipt.Add($"{item.Quantity}x {item.Product.Name} {(int)(item.Product.Price * item.Quantity)}");
				if (item.Product is IShippable shippable)
				{
					shippableItems.Add(shippable);
					int grams = (int)(shippable.GetWeight() * item.Quantity * 1000);
					shipmentNotice.Add($"{item.Quantity}x {shippable.GetName()} {grams}g");
					totalWeight += shippable.GetWeight() * item.Quantity;
				}
			}

			decimal shippingFee = totalWeight > 0 ? 30 : 0;
			decimal total = subtotal + shippingFee;

			if (customer.Balance < total)
				throw new Exception("Insufficient balance.");

			foreach (var item in cartItems)
				item.Product.Quantity -= item.Quantity;
			customer.Balance -= total;

			if (shippableItems.Any())
				_shippingService.Ship(shippableItems, shipmentNotice);

			// Add total package weight as the last line in shipmentNotice
			if (shipmentNotice.Count > 0)
			{
				shipmentNotice.Add($"Total package weight {totalWeight:0.0}kg");
			}

			return new CheckoutResultDto
			{
				Subtotal = subtotal,
				Shipping = shippingFee,
				Total = total,
				CustomerBalance = customer.Balance,
				ShipmentNotice = shipmentNotice,
				Receipt = receipt
			};
		}

		// Optional: Print the result in the specified console format
		//public void PrintCheckoutResult(CheckoutResultDto result)
		//{
		//	if (result.ShipmentNotice.Any())
		//	{
		//		Console.WriteLine("** Shipment notice **");
		//		foreach (var line in result.ShipmentNotice)
		//		{
		//			Console.WriteLine(line);
		//		}
		//	}
		//	Console.WriteLine("** Checkout receipt **");
		//	foreach (var line in result.Receipt)
		//	{
		//		Console.WriteLine(line);
		//	}
		//	Console.WriteLine("----------------------");
		//	Console.WriteLine($"Subtotal {result.Subtotal:0}");
		//	Console.WriteLine($"Shipping {result.Shipping:0}");
		//	Console.WriteLine($"Amount {result.Total:0}");
		//}
	}
}
