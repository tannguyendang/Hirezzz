using System.ComponentModel.DataAnnotations.Schema;

namespace Hirezzz.Models;

[Table("ProductType")]
public class ProductType
{
    [Column("ProductTypeId")]
    public byte Id { get; set; }
    [Column("ProductTypeName")]
    public string Name { get; set; } = null!;
}