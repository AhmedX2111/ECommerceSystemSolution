﻿using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceSystemAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CheckoutController : ControllerBase
	{
		private readonly AppDbContext _db;
		private readonly CheckoutService _checkoutService;

		public CheckoutController(AppDbContext db, CheckoutService checkoutService)
		{
			_db = db;
			_checkoutService = checkoutService;
		}

		// POST: api/checkout
		[HttpPost]
		public IActionResult Checkout([FromBody] CheckoutRequestDto request)
		{
			// Validate customer
			var customer = _db.Customers.Find(request.CustomerId);
			if (customer == null)
				return NotFound("Customer not found.");

			// Validate products and build cart items
			var cartItems = new List<CartItem>();
			foreach (var item in request.Items)
			{
				var product = _db.Products.Find(item.ProductId);
				if (product == null)
					return NotFound($"Product with ID {item.ProductId} not found.");
				cartItems.Add(new CartItem { Product = product, Quantity = item.Quantity });
			}

			try
			{
				// Call your checkout service to process the order
				var result = _checkoutService.Checkout(customer, cartItems);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
