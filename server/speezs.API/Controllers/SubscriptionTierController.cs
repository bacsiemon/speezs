using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using speezs.Services.Interfaces;
using speezs.Services.Models.SubscriptionTier;

namespace speezs.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubscriptionTierController : ControllerBase
	{
		private ISubscriptionTierService _subscriptionTierService;

		public SubscriptionTierController(ISubscriptionTierService subscriptionTierService)
		{
			_subscriptionTierService = subscriptionTierService;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);
				var response = await _subscriptionTierService.GetAllAsync();
				return StatusCode(response.Status, response.Data ?? response.Message);

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var response = await _subscriptionTierService.GetByIdAsync(id);
				return StatusCode(response.Status, response.Data ?? response.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{page}/{size}")]
		public async Task<IActionResult> Get(int page, int size)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var response = await _subscriptionTierService.GetPaginateAsync(page, size);
				return StatusCode(response.Status, response.Data ?? response.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateSubscriptionTierRequest request)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var response = await _subscriptionTierService.CreateAsync(request);
				return StatusCode(response.Status, response.Data ?? response.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Update(UpdateSubscriptionTierRequest request)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var response = await _subscriptionTierService.UpdateAsync(request);
				return StatusCode(response.Status, response.Data ?? response.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var response = await _subscriptionTierService.DeleteAsync(id);
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
