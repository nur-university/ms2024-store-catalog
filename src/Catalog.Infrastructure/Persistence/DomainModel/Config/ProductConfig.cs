using Catalog.Domain.Categories;
using Catalog.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Persistence.DomainModel.Config;

internal class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("product", "catalog");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("productId");

        var nameConverter = new ValueConverter<ProductName, string>(
                valueObject => valueObject.Name, // ProductName to string
                stringValue => new ProductName(stringValue) // string to ProductName
            );

        builder.Property(x => x.Name)
            .HasConversion(nameConverter)
            .HasColumnName("name");

        builder.Property(x => x.Descripcion)
            .HasColumnName("description");

        var priceConverter = new ValueConverter<PriceValue, decimal>(
                valueObject => valueObject.Value, // PriceValue to decimal
                decimalValue => new PriceValue(decimalValue) // decimal to PriceValue
            );

        builder.Property(x => x.Price)
            .HasConversion(priceConverter)
            .HasColumnName("price");

        builder.Property(x => x.CategoryId)
            .HasColumnName("categoryId");

        builder.Ignore("_domainEvents");
        builder.Ignore(x => x.DomainEvents);
    }
}
