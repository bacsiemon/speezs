﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using speezs.Services.Interfaces;
using speezs.Services.Models.SubscriptionTier;

namespace speezs.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubscriptionTierController : ControllerBase
	{
		private ISubscriptionTierService _service;

		public SubscriptionTierController(ISubscriptionTierService subscriptionTierService)
		{
			_service = subscriptionTierService;
		}

		[HttpGet]
		//[Authorize(Roles = "1")]
		public async Task<IActionResult> Get()
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);
				var response = await _service.GetAllAsync();
				return StatusCode(response.Status, response.Data ?? response.Message);

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}")]
		//[Authorize(Roles = "1")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var response = await _service.GetByIdAsync(id);
				return StatusCode(response.Status, response.Data ?? response.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{page}/{size}")]
		//[Authorize(Roles = "1")]
		public async Task<IActionResult> Get(int page = 1, int size = 10)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var response = await _service.GetPaginateAsync(page, size);
				return StatusCode(response.Status, response.Data ?? response.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost]
		//[Authorize(Roles = "1")]
		public async Task<IActionResult> Create(CreateSubscriptionTierRequest request)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var response = await _service.CreateAsync(request);
				return StatusCode(response.Status, response.Data ?? response.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut]
		//[Authorize(Roles = "1")]
		public async Task<IActionResult> Update(UpdateSubscriptionTierRequest request)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var response = await _service.UpdateAsync(request);
				return StatusCode(response.Status, response.Data ?? response.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete]
		//[Authorize(Roles = "1")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var response = await _service.DeleteAsync(id);
				return (response.Status >= 200 && response.Status <= 299) ?
					NoContent() : StatusCode(response.Status, response.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return StatusCode(500, ex.Message);
			}
		}
	}
}
