using MegaQr.Api.Models;

namespace MegaQr.Api.Services;

public interface ISectionService
{
    Task<Section> CreateSectionAsync();
}

