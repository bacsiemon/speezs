using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using speezs.Services;
using speezs.Services.Interfaces;
using speezs.Services.Models.Image;

namespace speezs.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImageController : ControllerBase
	{
		private IImageService _imageService;

		public ImageController(IImageService imageService)
		{
			_imageService = imageService;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var response = await _imageService.GetByIdAsync(id);
				speezs.DataAccess.Models.Image? image = response.Data as speezs.DataAccess.Models.Image;
				if (image == null)
				{
					return StatusCode(response.Status, response.Message);
				}

				return File(image.Data, image.ContentType);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return StatusCode(500, ex.Message);
			}
		}


		[HttpPost]
		public async Task<IActionResult> Create(IFormFile image)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var response = await  _imageService.CreateAsync(image);
				return StatusCode(response.Status, response.Data ?? response.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return StatusCode(500, ex.Message);
			}
		}
	}
}
