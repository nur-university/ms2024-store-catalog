using Joseco.DDD.Core.Abstractions;

namespace Catalog.Domain.Categories;

public class Category : AggregateRoot
{
    public CategoryName Name { get; private set; }
    public Category(string name) : base(Guid.NewGuid())
    {
        Name = name;
    }

    public void Update(string name)
    {
        Name = name;
    }

    //This constructor is for EF
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Category() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}
