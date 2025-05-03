using Catalog.Domain.Categories;
using MediatR;
using Joseco.DDD.Core.Abstractions;
using Joseco.DDD.Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Categories.CreateCategory;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Result<Guid>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category category = new(request.Name);

        await _categoryRepository.AddAsync(category);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success(category.Id);
    }
}
