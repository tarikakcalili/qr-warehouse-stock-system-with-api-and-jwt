using System.ComponentModel.DataAnnotations;
namespace MegaQr.Web.Models.DTOs;
public class CreateProductDto
{
    [Required]
    public string ProductName { get; set; }

    [Required]
    public string Type { get; set; }
}