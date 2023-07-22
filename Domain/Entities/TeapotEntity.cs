namespace Domain.Entities;
public class TeapotEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

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

    public DateTime CreationDate { get; set; } = DateTime.Now;

    public string ManualUrl { get; set; } = string.Empty;

    public int CompanyId { get; set; }
    public virtual CompanyEntity Company { get; set; } = null!;

    public string ManufacturerCountry { get; set; } = null!;

    public virtual ICollection<CommentEntity> Comments { get; set; } = null!;
}