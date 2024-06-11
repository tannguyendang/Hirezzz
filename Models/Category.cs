using System.ComponentModel.DataAnnotations.Schema;

namespace Hirezzz.Models;

[Table("Category")]
public class Category
{
    [Column("CategoryId")]
    public byte Id { get; set; }
    [Column("CategoryName")]
    public string Name { get; set; } = null!;
    public byte? ParentId { get; set; }
    public Category Parent { get; set; } = null!;
    public List<Category> Children { get; set; } = null!;
    public List<Product> Products { get; set; } = null!;
}
