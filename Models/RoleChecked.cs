using System.ComponentModel.DataAnnotations.Schema;
namespace Hirezzz.Models;
public class RoleChecked
{
    [Column("RoleName")]
    public string Name { get; set; } = null!;
    [Column("RoleId")]
    public int Id { get; set; }
    public bool Checked { get; set; }
}