using System.ComponentModel.DataAnnotations;

namespace MiniSIMRS.DTOs;
public class PasienRequestDto
{
    [Required]
    public string NamaLengkap {get; set;} = string.Empty;
    [Required]
    public string NIK {get; set;} = string.Empty;
    [Required]
    public string JenisKelamin {get; set;} = string.Empty;
    [Required]
    [MaxLength(2)]
    public string GolonganDarah {get; set;} = string.Empty;
    [Required]
    public DateOnly TanggalLahir {get; set;}
}