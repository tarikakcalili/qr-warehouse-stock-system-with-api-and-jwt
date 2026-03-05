namespace MegaQr.Api.Models.DTOs;
public class StoreDto
{
    public int StoreId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }   
    public int SectionId { get; set; }
    public string SectionName { get; set; }  
    public DateTime ToStoreRegister { get; set; }
    public DateTime? FromStoreExit { get; set; }
}
