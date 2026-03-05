using MegaQr.Web.Models.DTOs;
using MegaQr.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using MegaQr.Web.Handlers;

namespace MegaQr.Web.Controllers;

public class AccountController : Controller
{
    private readonly ApiClientService _apiClient;

    public AccountController(ApiClientService apiClient)
    {
        _apiClient = apiClient;
    }

    [HttpGet]
    public IActionResult Register()
    {
        var token = HttpContext.Session.GetString("jwt");
        if (string.IsNullOrEmpty(token))
            return RedirectToAction("Login");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        try
        {
            var response = await _apiClient.PostAsync("api/Auth/register", dto);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToAction("Login");

            var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<string>>();

            if (result == null || !result.Success)
            {
                ModelState.AddModelError("", result?.Message ?? "Registering is failed.");
                return View(dto);
            }

            return RedirectToAction("Login");
        }
        catch
        {
            ModelState.AddModelError("", "Server Error!");
            return View(dto);
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login() => View();

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        try
        {
            var response = await _apiClient.PostAsync("api/Auth/login", dto);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                ModelState.AddModelError("", "Username or Password is wrong!");
                return View(dto);
            }

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Login failed!");
                return View(dto);
            }

            var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<LoginResponseDto>>();

            if (result == null || result.Data == null || string.IsNullOrEmpty(result.Data.Token))
            {
                ModelState.AddModelError("", "Could not get token!");
                return View(dto);
            }

            HttpContext.Session.SetString("jwt", result.Data.Token);

            return RedirectToAction("Index", "Home");
        }
        catch
        {
            ModelState.AddModelError("", "Server Error!");
            return View(dto);
        }
    }

    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("jwt");
        return RedirectToAction("Login");
    }
}
