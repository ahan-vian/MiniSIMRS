using MiniSIMRS.Models;

namespace MiniSIMRS.DTOs;
public class RekamMedisResponseDto
{
    public int Id {get; set;}
    public DateTime TanggalPeriksa {get; set;}
    public string KeluhanUtama {get; set;} = string.Empty;
    public string Diagnosa {get; set;} = string.Empty;
    public string Tindakan {get; set;} = string.Empty;
    public string ResepObat {get; set;} = string.Empty;
    public string NamaPasien {get; set;} = string.Empty;
    public string NamaDokter {get; set;} = string.Empty;
}