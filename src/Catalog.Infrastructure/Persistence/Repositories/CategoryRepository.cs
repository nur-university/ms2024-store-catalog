using Catalog.Domain.Categories;
using Catalog.Infrastructure.Persistence.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Persistence.Repositories;

internal class CategoryRepository(DomainDbContext dbContext) : ICategoryRepository
{
    private readonly DomainDbContext _dbContext = dbContext;

    public async Task AddAsync(Category entity)
    {
        await _dbContext.Category.AddAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var obj = await GetByIdAsync(id);
        if (obj != null)
        {
            _dbContext.Category.Remove(obj);
        }
    }

    public async Task<Category?> GetByIdAsync(Guid id, bool readOnly = false)
    {
        if (readOnly)
        {
            return await _dbContext.Category.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }
        else
        {
            return await _dbContext.Category.FindAsync(id);
        }
    }

    public Task UpdateAsync(Category category)
    {
        _dbContext.Category.Update(category);

        return Task.CompletedTask;
    }
}
