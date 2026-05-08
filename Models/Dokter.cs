namespace MiniSIMRS.Models;
public class Dokter : BaseEntity
{
    public string NoSIP {get; set;} = string.Empty;
    public string NamaDokter {get; set;} = string.Empty;
    public string Spesialisasi {get; set;} = string.Empty;
    public ICollection<RekamMedis> RekamMediss {get; set;} = new List<RekamMedis>();
}