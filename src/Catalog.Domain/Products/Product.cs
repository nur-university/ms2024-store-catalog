using Catalog.Domain.Products.Events;
using Joseco.DDD.Core.Abstractions;

namespace Catalog.Domain.Products;

public class Product : AggregateRoot
{
    public ProductName Name { get; private set; }
    public PriceValue Price { get; private set; }

    public string Descripcion { get; private set; }
    public Guid CategoryId { get; private set; }

    public Product(string name, decimal price, string descripcion, Guid categoryId) : base(Guid.NewGuid())
    {
        Name = name;
        Price = price;
        Descripcion = descripcion;
        CategoryId = categoryId;

        AddDomainEvent(new ProductCreated(Id, name, descripcion, categoryId));
    }

    public void Update(string name, string description, decimal price, Guid categoryId)
    {
        Name = name;
        Descripcion = description;
        Price = price;
        CategoryId = categoryId;
    }

    //Constructor used by EF
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Product() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.



}
