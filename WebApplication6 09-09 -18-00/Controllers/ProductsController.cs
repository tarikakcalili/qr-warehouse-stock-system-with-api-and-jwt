using MegaQr.Web.Models.DTOs;
using MegaQr.Web.Models.ViewModels;
using MegaQr.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace MegaQr.Web.Controllers;

public class ProductsController : BaseController
{
    private readonly ApiClientService _apiClient;

    public ProductsController(ApiClientService apiClient)
    {
        _apiClient = apiClient;
    }

    [HttpGet]
    public IActionResult CreateProduct()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductDto dto)
    {
        var response = await _apiClient.PostAsync("api/products", dto);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError("", "Product couldn't registered!");
            return View(dto);
        }

        var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<ProductDto>>();

        if (result == null || !result.Success)
        {
            ModelState.AddModelError("", result?.Message ?? "Unknown Error!");
            return View(dto);
        }

        return RedirectToAction("ShowProductList");
    }

    [HttpGet]
    public async Task<IActionResult> ShowProductList()
    {
        var response = await _apiClient.GetAsync<ApiResponseDto<IEnumerable<ProductDto>>>("api/products");

        if (response == null || !response.Success || response.Data == null)
        {
            ModelState.AddModelError("", response?.Message ?? "Could not get ProductList!");
            return View(new List<ProductDto>());
        }

        return View(response.Data.ToList());
    }
    [HttpGet]
    public IActionResult Scan()
    {
        return View();
    }
    [HttpGet]
    public IActionResult ScanManuel()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> ScanProcess(int productId)
    {
        var dto = new ProductIdDto(productId);

        var response = await _apiClient.PostAsync("api/products/inStore", dto);

        if (!response.IsSuccessStatusCode)
            return View("Scan");

        var apiResult = await response.Content.ReadFromJsonAsync<ApiResponseDto<ScanProcessViewModel>>();

        if (apiResult == null || !apiResult.Success || apiResult.Data == null)
            return View("Scan");

        return View(apiResult.Data);
    }
}