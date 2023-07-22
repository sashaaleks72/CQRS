
namespace Application.Common.DTOs
{
    public class PaginatedDataDto<T>
    {
        public List<T> Data { get; set; } = null!;

        public int TotalCount { get; set; }

        public int Page { get; set; }

        public int Limit { get; set; }
    }
}
