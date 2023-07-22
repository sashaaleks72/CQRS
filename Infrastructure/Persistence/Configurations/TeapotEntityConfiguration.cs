using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    public class TeapotEntityConfiguration : IEntityTypeConfiguration<TeapotEntity>
    {
        public void Configure(EntityTypeBuilder<TeapotEntity> builder)
        {
            builder.HasKey(o => o.Id);
            builder.HasMany(o => o.Comments)
                .WithOne(c => c.Teapot)
                .HasForeignKey(c => c.TeapotId);
        }
    }
}