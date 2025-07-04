using Application.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
	public class ShippingService : IShippingService
	{
		public void Ship(List<IShippable> items)
		{
			Console.WriteLine("** Shipment notice **");
			double totalWeight = 0;
			foreach (var item in items)
			{
				Console.WriteLine($"{item.GetName()} {item.GetWeight()}kg");
				totalWeight += item.GetWeight();
			}
			Console.WriteLine($"Total package weight {totalWeight}kg");
		}
	}
}
