using CleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Infra.Data.EntitiesConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> b)
        {
            b.HasKey(t => t.Id);
            b.Property(p => p.Name).HasMaxLength(100).IsRequired();

            b.HasData(
                new Category(1, "Material Escolar"),
                new Category(2, "Eletrônicos"),
                new Category(3, "Roupas"),
                new Category(4, "Alimentos")
                );
        }
    }
}
