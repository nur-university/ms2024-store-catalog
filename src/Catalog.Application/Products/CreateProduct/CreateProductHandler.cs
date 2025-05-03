using Catalog.Domain.Products;
using MediatR;
using Joseco.DDD.Core.Abstractions;
using Joseco.DDD.Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Products.CreateProduct;

class CreateProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand, Result<Guid>>
{

    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = new(request.Name,
            request.Price,
            request.Description,
            request.CategoryId);

        await productRepository.AddAsync(product);

        await unitOfWork.CommitAsync(cancellationToken);

        return product.Id;
    }
}
