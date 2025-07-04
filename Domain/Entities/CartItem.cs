using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class CartItem
	{
		public int Id { get; set; }
		public Product Product { get; set; } // The product being added
		public int Quantity { get; set; } // Quantity requested
	}
}
