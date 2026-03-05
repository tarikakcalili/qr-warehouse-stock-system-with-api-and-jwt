using MegaQr.Web.Models.DTOs;

namespace MegaQr.Web.Models.ViewModels;
public class ScanProcessViewModel
{
    public ProductInStoreDto Product { get; set; }
    public List<SectionDto> Sections { get; set; }
}