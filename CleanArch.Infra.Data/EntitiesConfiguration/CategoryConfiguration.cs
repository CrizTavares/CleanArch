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
                new Category("Material Escolar"),
                new Category("Eletrônicos"),
                new Category("Roupas"),
                new Category("Alimentos")
                );
        }
    }
}
