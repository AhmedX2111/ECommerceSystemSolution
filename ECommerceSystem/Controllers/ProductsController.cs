using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly AppDbContext _db;

		public ProductsController(AppDbContext db)
		{
			_db = db;
		}

		// GET: api/products
		[HttpGet]
		public ActionResult<IEnumerable<Product>> GetProducts()
		{
			var products = _db.Products.ToList();
			return Ok(products);
		}
	}
}
