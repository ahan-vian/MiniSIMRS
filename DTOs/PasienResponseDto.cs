namespace MiniSIMRS.DTOs;
public class PasienResponseDto
{
    public int Id {get; set;}
    public string NoRekamMedis {get; set;} = string.Empty;
    public string NamaLengkap {get; set;} = string.Empty;
    public string NIK {get; set;} = string.Empty;
    public string JenisKelamin {get; set;} = string.Empty;
    public string GolonganDarah {get; set;} = string.Empty;
    public DateOnly TanggalLahir {get; set;}
}