namespace MiniSIMRS.Models;
public class Pasien : BaseEntity
{
    public string NoRekamMedis {get; set;} = string.Empty;
    public string NamaLengkap {get; set;} = string.Empty;
    public string NIK {get; set;} = string.Empty;
    public string JenisKelamin {get; set;} = string.Empty;
    public string GolonganDarah {get; set;} = string.Empty;
    public DateOnly TanggalLahir {get; set;}
    public ICollection<RekamMedis> RekamMediss {get; set;} = new List<RekamMedis>();
}