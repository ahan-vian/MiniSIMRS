using System.ComponentModel.DataAnnotations;

namespace MiniSIMRS.DTOs;
public class DokterRequestDto
{
    [Required]
    public string NoSIP {get; set;} = string.Empty;
    [Required]
    public string NamaDokter {get; set;} = string.Empty;
    [Required]
    public string Spesialisasi {get; set;} = string.Empty;
}