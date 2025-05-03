using Catalog.Application.Categories.Dto;
using Catalog.Domain.Categories;
using MediatR;
using Joseco.DDD.Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Categories.GetCategories;

internal class GetCategoryByIdHandler(ICategoryRepository categoryRepository) 
    : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<Result<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var obj = await _categoryRepository.GetByIdAsync(request.CategoryId, true);

        if(obj == null)
        {
            return Result.Failure<CategoryDto>(CategoryErrors.CategoryNotFound());
        }

        return Result.Success(new CategoryDto()
        {
            CategoryId = obj.Id,
            Name = obj.Name
        });

    }
}
