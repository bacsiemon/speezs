using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using speezs.Services;
using speezs.Services.Interfaces;
using speezs.Services.Models.Look;
using speezs.Services.Models.Transfer;

namespace speezs.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TransferController : ControllerBase
	{
		private ITransferService _transferService;

		public TransferController(ITransferService transferService)
		{
			_transferService = transferService;
		}

		[HttpGet]
		//[Authorize(Roles = "1")]
		public async Task<IActionResult> Get()
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);
				var response = await _transferService.GetAllAsync();
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

				var response = await _transferService.GetByIdAsync(id);
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

				var response = await _transferService.GetPaginateAsync(page, size);
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
		public async Task<IActionResult> Create(CreateTransferRequest request)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var response = await _transferService.CreateAsync(request);
				return StatusCode(response.Status, response.Data ?? response.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return StatusCode(500, ex.Message);
			}
		}

		//[HttpPut]
		//public async Task<IActionResult> Update(UpdateLookRequest request)
		//{
		//	try
		//	{
		//		if (!ModelState.IsValid)
		//			return BadRequest(ModelState);

		//		var response = await _transferService.UpdateAsync(request);
		//		return HttpStatus(response.Status, response.Data ?? response.Message);
		//	}
		//	catch (Exception ex)
		//	{
		//		Console.WriteLine(ex.ToString());
		//		return HttpStatus(500, ex.Message);
		//	}
		//}

		[HttpDelete]
		//[Authorize(Roles = "1")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var response = await _transferService.DeleteAsync(id);
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
