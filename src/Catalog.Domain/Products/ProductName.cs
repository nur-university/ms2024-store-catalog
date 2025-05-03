using Joseco.DDD.Core.Results;

namespace Catalog.Domain.Products;

public record ProductName 
{
    public string Name { get; init; }

    public ProductName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException(ProductErrors.NameNullOrEmpty());
        }
        Name = name;
    }

    public static implicit operator ProductName(string name)
    {
        return new ProductName(name);
    }

    public static implicit operator string(ProductName productName)
    {
        return productName.Name;
    }
}
