using System.ComponentModel.DataAnnotations.Schema;

namespace Hirezzz.Models;

[Table("Country")]
public class Country
{
    [Column("CountryId")]
    public byte Id { get; set; }
    [Column("CountryName")]
    public string Name { get; set; } = null!;
}