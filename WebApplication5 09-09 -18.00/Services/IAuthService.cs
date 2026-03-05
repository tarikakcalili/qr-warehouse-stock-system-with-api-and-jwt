using MegaQr.Api.Models;

namespace MegaQr.Api.Services;

public interface IAuthService
{
    string GenerateJwt(User user);
    string HashPassword(string password);
}
