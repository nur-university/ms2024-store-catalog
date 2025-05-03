using Joseco.DDD.Core.Abstractions;

namespace Catalog.Domain.Categories;

public interface ICategoryRepository : IRepository<Category>
{
    Task UpdateAsync(Category category);

    Task DeleteAsync(Guid id);
}
