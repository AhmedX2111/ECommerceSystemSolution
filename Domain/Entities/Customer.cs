using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class Customer
	{
		public int Id { get; set; } // Unique identifier
		public string Name { get; set; } // Customer name
		public decimal Balance { get; set; } // Current account balance
	}
}
