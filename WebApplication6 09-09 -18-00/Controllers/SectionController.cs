using MegaQr.Web.Models.DTOs;
using MegaQr.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace MegaQr.Web.Controllers;
public class SectionController : BaseController
{
    private readonly ApiClientService _apiClient;
    public SectionController(ApiClientService apiClient)
    {
        _apiClient = apiClient;
    }

    [HttpGet]
    public async Task<IActionResult> CreateSection()
    {
        var apiResult = await _apiClient.GetAsync<ApiResponseDto<IEnumerable<SectionDto>>>("api/sections/GetSectionList");
        var sections = apiResult?.Data ?? new List<SectionDto>();
        return View(sections);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSectionPost()
    {
        var apiResult = await _apiClient.PostAsync<object>("api/sections/CreateSection", new { });

        if (!apiResult.IsSuccessStatusCode)
        {
            TempData["Error"] = "Section üretilemedi!";
            return RedirectToAction("CreateSection");
        }

        return RedirectToAction("CreateSection");
    }
}
