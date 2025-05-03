using Catalog.Domain.Categories;
using Catalog.Domain.Products;
using Catalog.Infrastructure.Persistence.DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Persistence.Repositories;

internal class ProductRepository(DomainDbContext context) : IProductRepository
{
    public async Task AddAsync(Product entity)
    {
        await context.Product.AddAsync(entity);
    }

    public async Task<Product?> GetByIdAsync(Guid id, bool readOnly = false)
    {
        if (readOnly)
        {
            return await context.Product.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }
        else
        {
            return await context.Product.FindAsync(id);
        }
    }

    public Task UpdateAsync(Product product)
    {
        context.Product.Update(product);

        return Task.CompletedTask;
    }
}
