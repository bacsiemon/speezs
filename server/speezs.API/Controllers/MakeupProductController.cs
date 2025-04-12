using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using speezs.Services;
using speezs.Services.Interfaces;
using speezs.Services.Models.MakeupProduct;
using speezs.Services.Models.User;

namespace speezs.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MakeupProductController : ControllerBase
	{
		private IMakeupProductService _makeupProductService;

		public MakeupProductController(IMakeupProductService makeupProductService)
		{
			_makeupProductService = makeupProductService;
		}

		[HttpGet]
		//[Authorize(Roles = "1,2")]
		public async Task<IActionResult> Get()
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);
				var response = await _makeupProductService.GetAllAsync();
				return StatusCode(response.Status, response.Data ?? response.Message);

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}")]
		//[Authorize(Roles = "1,2")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var response = await _makeupProductService.GetByIdAsync(id);
				return StatusCode(response.Status, response.Data ?? response.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{page}/{size}")]
		//[Authorize(Roles = "1,2")]
		public async Task<IActionResult> Get(int page, int size)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var response = await _makeupProductService.GetPaginateAsync(page, size);
				return StatusCode(response.Status, response.Data ?? response.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost]
		//[Authorize(Roles = "1,2")]
		public async Task<IActionResult> Create(CreateMakeupProductRequest request)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var response = await _makeupProductService.CreateAsync(request);
				return StatusCode(response.Status, response.Data ?? response.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut]
		//[Authorize(Roles = "1,2")]
		public async Task<IActionResult> Update(UpdateMakeupProductRequest request)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var response = await _makeupProductService.UpdateAsync(request);
				return StatusCode(response.Status, response.Data ?? response.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete]
		//[Authorize(Roles = "1,2")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var response = await _makeupProductService.DeleteAsync(id);
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
