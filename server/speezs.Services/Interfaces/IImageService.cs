using Microsoft.AspNetCore.Http;
using speezs.Services.Base;
using speezs.Services.Models.Image;

namespace speezs.Services.Interfaces
{
	public interface IImageService
	{
		Task<IServiceResult> CreateAsync(IFormFile request);
		Task<IServiceResult> GetByIdAsync(int id);
	}
}