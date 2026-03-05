namespace MegaQr.Web.Models;
public class Product
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? Type { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
}