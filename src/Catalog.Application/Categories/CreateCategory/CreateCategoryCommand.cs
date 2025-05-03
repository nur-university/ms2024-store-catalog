using MediatR;
using Joseco.DDD.Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Categories.CreateCategory;

public class CreateCategoryCommand : IRequest<Result<Guid>>
{
    public string Name { get; set; }
}
