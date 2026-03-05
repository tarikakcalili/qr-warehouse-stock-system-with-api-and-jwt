using MegaQr.Web.Models.DTOs;
using MegaQr.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace MegaQr.Web.Controllers;

public class StoreController : BaseController
{
    private readonly ApiClientService _apiClient;

    public StoreController(ApiClientService apiClient)
    {
        _apiClient = apiClient;
    }
    [HttpGet]
    public async Task<IActionResult> ShowStoreList()
    {
        ApiResponseDto<IEnumerable<StoreDto>> apiResult;

        try
        {
            apiResult = await _apiClient.GetAsync<ApiResponseDto<IEnumerable<StoreDto>>>("api/store/GetStoreList");
        }
        catch (HttpRequestException ex)
        {
            TempData["Error"] = $"API hatası: {ex.Message}";
            return View(Enumerable.Empty<StoreDto>());
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Beklenmeyen bir hata oluştu: {ex.Message}";
            return View(Enumerable.Empty<StoreDto>());
        }

        if (apiResult == null || !apiResult.Success)
        {
            TempData["Error"] = apiResult?.Message ?? "Store verileri alınamadı!";
            return View(Enumerable.Empty<StoreDto>());
        }

        return View(apiResult.Data);
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromStore(int productId)
    {
        var dto = new RemoveFromStoreDto(productId);

        var response = await _apiClient.PostAsync("api/store/RemoveFromStore", dto);

        /*
        if (!response.IsSuccessStatusCode)
        {
            TempData["Error"] = "Ürün çıkarılamadı!";
            return RedirectToAction("ScanProcess", "Products", new { productId });
        }
        */

        var apiResult = await response.Content.ReadFromJsonAsync<ApiResponseDto<object>>();
        
        /*
        if (apiResult == null || !apiResult.Success)
        {
            TempData["Error"] = apiResult?.Message ?? "Ürün çıkarılamadı!";
            return RedirectToAction("ScanProcess", "Products", new { productId });
        }

        TempData["Success"] = apiResult.Message;
        return RedirectToAction("ScanProcess", "Products", new { productId });
        */

        return RedirectToAction("ShowStoreList", "Store");
    }

    [HttpPost]
    public async Task<IActionResult> AddToStore(int productId, int sectionId)
    {
        var dto = new AddToStoreDto(productId, sectionId);

        var response = await _apiClient.PostAsync("api/Store/AddToStore", dto);

        if (response.IsSuccessStatusCode)
        {
            TempData["Success"] = "Ürün başarıyla eklendi!";
        }
        else
        {
            TempData["Error"] = "Ürün eklenemedi!";
        }

        return RedirectToAction("ShowStoreList", "Store");
    }
}
