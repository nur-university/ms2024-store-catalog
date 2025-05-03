using Joseco.DDD.Core.Results;

namespace Catalog.Domain.Products;

public static class ProductErrors 
{
    public static Error NameNullOrEmpty() => new("Product.NameNullOrEmpty", "Product name cannot be null or empty", ErrorType.Validation);

    public static Error PriceInvalid() => new("Price.Negative", "Price cannot be negative", ErrorType.Validation);
}
