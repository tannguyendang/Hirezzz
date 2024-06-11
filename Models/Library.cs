using System.ComponentModel.DataAnnotations.Schema;

namespace Hirezzz.Models;
public class Library
{
    public string LibId { get; set; } = null!;
    public string MemberId { get; set; } = null!;
    public int ProductId { get; set; }
    public string ProductUrl { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public string Singer { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
}