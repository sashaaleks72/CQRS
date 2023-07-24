using Amazon.S3.Model;
using Amazon.S3;
using Application.Interfaces.Services;
using Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using Application.Common.Models;

namespace Infrastructure.Services
{
    public class TeapotImageService : IImageService
    {
        private readonly IAmazonS3 _amazonS3;
        private readonly AmazonS3Configuration _s3Config;

        public TeapotImageService(IAmazonS3 amazonS3, IOptions<AmazonS3Configuration> options) 
        { 
            _amazonS3 = amazonS3;
            _s3Config = options.Value;
        }

        public async Task<UploadResult> UploadImage(Stream image, string contentType)
        {
            var fileName = $"{Guid.NewGuid()}.{contentType.Split("/")[1]}";

            var request = new PutObjectRequest
            {
                BucketName = _s3Config.Name,
                Key = $"{_s3Config.Folders["TeapotImages"]}{fileName}",
                InputStream = image
            };
            request.Metadata.Add("Content-Type", contentType);

            var response = await _amazonS3.PutObjectAsync(request);

            var result = new UploadResult
            {
                FileName = fileName,
                StatusCode = response.HttpStatusCode
            };

            return result;
        }
    }
}
