using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entities
{
	public class TV : Product, IShippable
	{
		public double Weight { get; set; }
		public string GetName() => Name;
		public double GetWeight() => Weight;
	}
}
