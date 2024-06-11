using System.ComponentModel.DataAnnotations.Schema;
namespace Hirezzz.Models;

[Table("MemberPassword")]
public class MemberPassword
{
    public string MemberId { get; set; } = null!;
    public byte[] Password { get; set; } = null!;
    public Member Member { get; set; } = null!;
}