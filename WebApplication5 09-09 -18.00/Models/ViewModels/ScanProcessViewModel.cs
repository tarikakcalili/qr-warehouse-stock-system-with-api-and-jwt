using MegaQr.Api.Models.DTOs;

namespace MegaQr.Api.Models.ViewModels;
public class ScanProcessViewModel
{
    public ProductInStoreDto Product { get; set; }
    public List<SectionDto> Sections { get; set; } = new List<SectionDto>();
}