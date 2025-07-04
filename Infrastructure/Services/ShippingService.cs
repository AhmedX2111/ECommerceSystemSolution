using Application.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class ShippingService : IShippingService
	{
		public void Ship(List<IShippable> items, List<string> shipmentNotice)
		{
			// This could be a call to an external shipping API, but for now just log/print
			Console.WriteLine("** Shipment notice **");
			foreach (var line in shipmentNotice)
				Console.WriteLine(line);
			double totalWeight = items.Sum(i => i.GetWeight());
			Console.WriteLine($"Total package weight {totalWeight}kg");
		}
	}
}
