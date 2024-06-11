namespace Hirezzz.Models;

public class RegisterModel
{
    public string Usr { get; set; } = null!;
    public string Pwd { get; set; } = null!;
    public string Eml { get; set; } = null!;
    public string Fullname { get; set; } = null!;
    public bool Gen { get; set; }
}