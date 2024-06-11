using System.ComponentModel.DataAnnotations.Schema;

namespace Hirezzz.Models;
[Table("Role")]
public class Role
{
    [Column("RoleId")]
    public int Id { get; set; }
    [Column("RoleName")]
    public string Name { get; set; } = null!;
}