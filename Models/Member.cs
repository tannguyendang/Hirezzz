using System.ComponentModel.DataAnnotations.Schema;
namespace Hirezzz.Models;

[Table("Member")]
public class Member
{
    [Column("MemberId")]
    public string Id { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Fullname { get; set; } = null!;
    public bool Gender { get; set; }
    // public bool? IsDeleted { get; set; }
    // public DateTime? UpdatedDate { get; set; }
    public List<MemberPassword> MemberPasswords { get; set; } = null!;
    public List<MemberStringPassword> memberStringPasswords { get; set; } = null!;
}