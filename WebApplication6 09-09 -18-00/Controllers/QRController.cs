using MegaQr.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace MegaQr.Web.Controllers;

public class QrController : BaseController
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(string userInput)
    {
        if (!string.IsNullOrEmpty(userInput))
        {
            string svgQr = QrCodeGeneratorService.GenerateQrCodeSvg(userInput);
            ViewBag.QrCodeSvg = svgQr;
        }

        return View();
    }
}
