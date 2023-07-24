
using Application.Common.Models;

namespace Application.Interfaces.Services
{
    public interface IImageService
    {
        Task<UploadResult> UploadImageAsync(Stream image, string contentType);
    }
}
