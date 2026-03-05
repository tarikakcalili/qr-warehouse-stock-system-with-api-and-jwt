using System.ComponentModel.DataAnnotations;
namespace MegaQr.Api.Models.DTOs;
//Veritabanına Ürün Kaydetmek İçin
public class CreateProductDto
{
    [Required]
    public string ProductName { get; set; }

    [Required]
    public string Type { get; set; }
}

