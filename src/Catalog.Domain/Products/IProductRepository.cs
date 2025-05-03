using Joseco.DDD.Core.Abstractions;

namespace Catalog.Domain.Products;

public interface IProductRepository : IRepository<Product>
{
    Task UpdateAsync(Product product);
}
