using System.ComponentModel.DataAnnotations;

namespace MegaQr.Api.Models;
public class Store
{
    [Key]
    public int StoreId { get; set; }

    [Required]
    public int ProductId { get; set; }
    public Product Product { get; set; }

    [Required]
    public DateTime ToStoreRegister { get; set; } = DateTime.Now;

    public DateTime? FromStoreExit { get; set; }

    [Required]
    public int SectionId { get; set; }
    public Section Section { get; set; }
}