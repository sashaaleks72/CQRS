
using Domain.Entities;

namespace Application.Products.DTOs
{
    public class AddProductRequestDto
    {
        public string Name { get; set; } = null!;

        public int Quantity { get; set; }

        public string Color { get; set; } = null!;

        public string BodyMaterial { get; set; } = null!;

        public int Power { get; set; }

        public double Price { get; set; }

        public string ImgName { get; set; } = null!;

        public double Volume { get; set; }

        public int WarrantyInMonths { get; set; }

        public string Functions { get; set; } = null!;

        public double Weight { get; set; }

        public string ManualUrl { get; set; } = string.Empty;

        public int CompanyId { get; set; }

        public string ManufacturerCountry { get; set; } = null!;
    }
}
