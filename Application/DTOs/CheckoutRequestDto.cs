using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
	public class CheckoutRequestDto
	{
		public int CustomerId { get; set; }
		public List<CartItemDto> Items { get; set; }
	}
}
