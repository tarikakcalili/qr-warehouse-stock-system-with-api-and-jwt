using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MegaQr.Api.Models;
public class Product
{
    [Key]
    public int ProductId { get; set; }
    [Required]
    public string? ProductName { get; set; }
    [Required]
    public string? Type { get; set; }
    [Required]
    public DateTime Date { get; set; } = DateTime.Now;
    [JsonIgnore]
    public ICollection<Store>? Stores { get; set; }
}