namespace MegaQr.Web.Models;
public class Store
{
    public int StoreId { get; set; }
    public int ProductId { get; set; }
    public DateTime ToStoreRegister { get; set; } = DateTime.Now;
    public DateTime? FromStoreExit { get; set; }
    public int SectionId { get; set; }
}