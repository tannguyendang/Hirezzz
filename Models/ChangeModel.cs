namespace Hirezzz.Models
{
    public class ChangeModel
    {
        public string? Id { get; set; }
        public string OldPwd { get; set; } = null!;
        public string NewPwd { get; set; } = null!;
    }
}
