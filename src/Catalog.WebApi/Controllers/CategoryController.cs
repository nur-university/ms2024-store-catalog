using Catalog.Application.Categories.CreateCategory;
using Catalog.Application.Categories.GetCategories;
using Catalog.WebApi.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.WebApi.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class CategoryController : CustomController
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(Guid id)
    {
        GetCategoryByIdQuery query = new GetCategoryByIdQuery()
        {
            CategoryId = id
        };
        var result = await _mediator.Send(query);
        return BuildResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody]CreateCategoryCommand command)
    {
        var result = await _mediator.Send(command);

        return BuildResult(result);
    }
}
