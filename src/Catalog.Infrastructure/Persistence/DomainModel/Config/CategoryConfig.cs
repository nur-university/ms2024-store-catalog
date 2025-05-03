using Catalog.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Catalog.Infrastructure.Persistence.DomainModel.Config;

internal class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("category", "catalog");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("categoryId");

        var converter = new ValueConverter<CategoryName, string>(
                valueObject => valueObject.Name, // CategoryName to string
                stringValue => new CategoryName(stringValue) // string to CategoryName
            );

        builder.Property(x => x.Name)
            .HasConversion(converter)
            .HasColumnName("name");

        builder.Ignore("_domainEvents");
        builder.Ignore(x => x.DomainEvents);
    }
}
