using Catalog.Application.Products.CreateProduct;
using Catalog.WebApi.Extensions;
using Catalog.WebApi.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Joseco.DDD.Core.Results;

namespace Catalog.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : CustomController
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        return Ok();
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        Result<Guid> result = await _mediator.Send(command);
        
        return BuildResult(result);
    }
}
