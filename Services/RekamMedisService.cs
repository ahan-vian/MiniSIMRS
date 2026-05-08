using Microsoft.EntityFrameworkCore;
using MiniSIMRS.Data;
using MiniSIMRS.DTOs;
using MiniSIMRS.Models;

namespace MiniSIMRS.Services;
public class RekamMedisService : IRekamMedis
{
    private readonly AppDbContext _context;
    public RekamMedisService(AppDbContext context)
    {
        _context = context;
    }
    public async Task <IEnumerable<RekamMedisResponseDto?>> GetAllAsync(int pasienId)
    {
        var rekamMedis = await _context.RekamMediss
            .Include(p=> p.Pasien) 
            .Include(d=> d.Dokter)
            .Where(r=> r.Pasien.Id == pasienId)
            .Select(r=> new RekamMedisResponseDto
            {
                Id = r.Id,
                TanggalPeriksa = r.TanggalPeriksa,
                KeluhanUtama = r.KeluhanUtama,
                Diagnosa = r.Diagnosa,
                Tindakan = r.Tindakan,
                ResepObat = r.ResepObat,
                NamaPasien = r.Pasien.NamaLengkap,
                NamaDokter = r.Dokter.NamaDokter,
            }).ToListAsync();
        return rekamMedis;
    }
    public async Task CreateAsync(RekamMedisRequestDto dto)
    {
        var pasienExist = await _context.Pasiens.AnyAsync(p=> p.Id == dto.PasienId);
        if(!pasienExist) throw new Exception($"Pasien dengan id {dto.PasienId} tidak ditemukan");
        var dokterExist = await _context.Dokters.AnyAsync(d=>d.Id == dto.DokterId);
        if(!dokterExist) throw new Exception($"Dokter dengan id {dto.DokterId} tidak ditemukan");
        var newRekamMedis = new RekamMedis
        {
            PasienId = dto.PasienId,
            DokterId = dto.DokterId,
            KeluhanUtama = dto.KeluhanUtama,
            Diagnosa = dto.Diagnosa,
            Tindakan = dto.Tindakan,
            ResepObat = dto.ResepObat,
            TanggalPeriksa = DateTime.Now,
            CreatedAt = DateTime.Now,
        };
        _context.RekamMediss.Add(newRekamMedis);
        await _context.SaveChangesAsync();
    }
    
}