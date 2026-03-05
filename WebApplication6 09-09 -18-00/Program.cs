using MegaQr.Web.Handlers;
using MegaQr.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<AuthHeaderHandler>();
builder.Services.AddTransient<UnauthorizedRedirectHandler>();

builder.Services.AddScoped<ApiClientService>();

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7014/");
})
.AddHttpMessageHandler<AuthHeaderHandler>()           
.AddHttpMessageHandler<UnauthorizedRedirectHandler>(); 

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
