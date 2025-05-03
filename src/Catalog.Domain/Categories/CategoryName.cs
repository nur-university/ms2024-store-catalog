using Joseco.DDD.Core.Results;

namespace Catalog.Domain.Categories;

public record CategoryName
{
    public string Name { get; init; }

    public CategoryName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException(CategoryErrors.NameNullOrEmpty());
        }
        Name = name;
    }

    public static implicit operator CategoryName(string name)
    {
        return new CategoryName(name);
    }

    public static implicit operator string(CategoryName categoryName)
    {
        return categoryName.Name;
    }
}
