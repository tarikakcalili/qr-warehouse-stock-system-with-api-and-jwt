using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MegaQr.Web.Controllers;
public class BaseController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var token = HttpContext.Session.GetString("jwt");
        if (string.IsNullOrEmpty(token))
        {
            context.Result = RedirectToAction("Login", "Account");
        }

        base.OnActionExecuting(context);
    }
}
