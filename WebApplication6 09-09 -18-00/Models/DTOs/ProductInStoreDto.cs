namespace MegaQr.Web.Models.DTOs
{
    public class ProductInStoreDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public int? SectionId { get; set; }
        public string? SectionName { get; set; }
        public DateTime? ToStoreRegister { get; set; }
        public DateTime? FromStoreExit { get; set; }
    }
}