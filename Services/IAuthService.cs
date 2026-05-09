using MiniSIMRS.DTOs;

namespace MiniSIMRS.Services;
public interface IAuthService
{
    Task<string?> LoginAsync(LoginRequestDto dto);
    Task<bool> RegisterAsync(RegisterRequestDto dto);
}