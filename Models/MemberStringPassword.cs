using System.ComponentModel.DataAnnotations.Schema;
namespace Hirezzz.Models;

[Table("MemberStringPassword")]
public class MemberStringPassword
{
    public string MemberId { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Member Member { get; set; } = null!;
}