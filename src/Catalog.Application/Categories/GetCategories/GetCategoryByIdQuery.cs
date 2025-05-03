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

public record GetCategoryByIdQuery : IRequest<Result<CategoryDto>>
{
    public Guid CategoryId { get; set; }

}
