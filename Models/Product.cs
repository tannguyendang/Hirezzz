using System.ComponentModel.DataAnnotations.Schema;

namespace Hirezzz.Models;
[Table("Product")]
public class Product
{
    [Column("ProductId")]
    public int Id { get; set; }
    public byte CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    [Column("ProductName")]
    public string Name { get; set; } = null!;
    public byte ProductTypeId { get; set; }
    public string ProductUrl { get; set; } = null!;
    public string Singer { get; set; } = null!;
    public byte CountryId { get; set; }
    public string ImageUrl { get; set; } = null!;
}