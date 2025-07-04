using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
	public class CheckoutResultDto
	{
		public decimal Subtotal { get; set; }
		public decimal Shipping { get; set; }
		public decimal Total { get; set; }
		public decimal CustomerBalance { get; set; }
		public List<string> ShipmentNotice { get; set; }
		public List<string> Receipt { get; set; }
	}
}
