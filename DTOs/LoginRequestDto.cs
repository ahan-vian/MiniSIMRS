using System.ComponentModel.DataAnnotations;

namespace MiniSIMRS.DTOs;
public class LoginRequestDto
{
    [Required]
    public string Username {get; set;} = string.Empty;
    [Required]
    [MinLength(6)]
    public string Password {get; set;} = string.Empty;
}