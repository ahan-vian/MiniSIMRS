namespace MiniSIMRS.Models;
public class RekamMedis : BaseEntity
{
    public int PasienId {get; set;}
    public int DokterId {get; set;}
    public DateTime TanggalPeriksa {get; set;}
    public string KeluhanUtama {get; set;} = string.Empty;
    public string Diagnosa {get; set;} = string.Empty;
    public string Tindakan {get; set;} = string.Empty;
    public string ResepObat {get; set;} = string.Empty;
    public Pasien Pasien {get; set;} = null!;
    public Dokter Dokter {get; set;} = null!;
}