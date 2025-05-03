using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Persistence.StoredModel.Entities;

[Table("product", Schema = "catalog")]
internal class ProductModel
{
    [Key]
    [Column("productId")]
    public Guid Id { get; set; }

    [Column("name")]
    [StringLength(250)]
    [Required]
    public string Name { get; set; }

    [Column("description")]
    [MaxLength]
    public string Description { get; set; }

    [Column("price", TypeName = "decimal(18,2)")]
    [Required]
    public decimal Price { get; set; }

    [Column("categoryId")]
    public Guid CategoryId { get; set; }
    public CategoryModel Category { get; set; }
}
