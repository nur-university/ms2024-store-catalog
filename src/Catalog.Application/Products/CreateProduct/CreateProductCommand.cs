using MediatR;
using Joseco.DDD.Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Products.CreateProduct;

public record CreateProductCommand(string Name, string Description, decimal Price, Guid CategoryId) : IRequest<Result<Guid>> { }