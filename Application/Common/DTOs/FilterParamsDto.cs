
namespace Application.Common.DTOs
{
    public class FilterParamsDto
    {
        public int? Page { get; set; }

        public int? Limit { get; set; }

        public bool? IsAscending { get; set; }

        public string? SortField { get; set; }
    }
}
