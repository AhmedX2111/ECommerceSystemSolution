using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly AppDbContext _db;

		public CustomerController(AppDbContext db)
		{
			_db = db;
		}

		// GET: api/customer
		[HttpGet]
		public ActionResult<Customer> GetCustomer()
		{
			// For demo, just return the first customer
			var customer = _db.Customers.FirstOrDefault();
			if (customer == null)
				return NotFound("No customer found.");
			return Ok(customer);
		}
	}
}
