namespace MegaQr.Api.Models.DTOs;
//Hangi Ürünün Veritabanına Kaydedildiği Bilgisini Client'a Sağlamak İçin
public class ProductDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string Type { get; set; }
    public DateTime Date { get; set; }
}