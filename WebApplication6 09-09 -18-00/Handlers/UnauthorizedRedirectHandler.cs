namespace MegaQr.Web.Handlers;
public class UnauthorizedRedirectHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            response.Headers.Add("X-Redirect", "/Account/Login");
        }

        return response;
    }
}