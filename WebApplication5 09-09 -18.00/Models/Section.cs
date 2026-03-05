using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MegaQr.Api.Models;
public class Section
{
    [Key]
    public int SectionId { get; set; }

    [Required]
    public string SectionName { get; set; }
    [JsonIgnore]
    public ICollection<Store>? Stores { get; set; }
}

