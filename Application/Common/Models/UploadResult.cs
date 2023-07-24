using System.Net;

namespace Application.Common.Models
{
    public class UploadResult
    {
        public HttpStatusCode StatusCode { get; set; }

        public string FileName { get; set; } = string.Empty;
    }
}
