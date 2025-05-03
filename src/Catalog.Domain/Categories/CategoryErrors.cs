using Joseco.DDD.Core.Results;

namespace Catalog.Domain.Categories;

public static class CategoryErrors 
{
    public static Error NameNullOrEmpty() => new("Category.NameNullOrEmpty", "Category name cannot be null or empty", ErrorType.Validation);

    public static Error CategoryNotFound() => Error.NotFound("Category.NotFound", "Category requested does not exist");
}
