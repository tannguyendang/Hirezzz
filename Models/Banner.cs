using System.ComponentModel.DataAnnotations.Schema;

namespace Hirezzz.Models;
[Table("Banner")]
public class Banner
{
    [Column("BannerId")]
    public byte Id { get; set; }
    [Column("BannerName")]
    public string Name { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    [Column("BannerType")]
    public byte Type { get; set; }
}