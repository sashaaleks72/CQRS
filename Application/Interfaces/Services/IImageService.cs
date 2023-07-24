
using Application.Common.Models;

namespace Application.Interfaces.Services
{
    public interface IImageService
    {
        Task<UploadResult> UploadImage(Stream image, string contentType);
    }
}
