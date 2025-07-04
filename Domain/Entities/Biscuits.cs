using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entities
{
	public class Biscuits : Product, IExpirable, IShippable
	{
		public DateTime ExpiryDate { get; set; }
		public double Weight { get; set; }
		public bool IsExpired() => DateTime.UtcNow > ExpiryDate;
		public string GetName() => Name;
		public double GetWeight() => Weight;
	}
}
