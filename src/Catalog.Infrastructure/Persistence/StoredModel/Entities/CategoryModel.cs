using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Persistence.StoredModel.Entities;

[Table("category", Schema = "catalog")]
internal class CategoryModel
{
    [Key]
    [Column("categoryId")]
    public Guid Id { set; get; }

    [Column("name")]
    [Required]
    [MaxLength(250)]
    public string Name { get; set; }

}
