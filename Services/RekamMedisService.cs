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
    public async Task<IEnumerable<RekamMedisResponseDto?>> GetAllAsync(int pasienId)
    {
        var rekamMedis = await _context.RekamMediss
            .Include(p => p.Pasien)
            .Include(d => d.Dokter)
            .Where(r => r.Pasien.Id == pasienId)
            .Select(r => new RekamMedisResponseDto
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
        var pasienExist = await _context.Pasiens.AnyAsync(p => p.Id == dto.PasienId);
        if (!pasienExist) throw new Exception($"Pasien dengan id {dto.PasienId} tidak ditemukan");
        var dokterExist = await _context.Dokters.AnyAsync(d => d.Id == dto.DokterId);
        if (!dokterExist) throw new Exception($"Dokter dengan id {dto.DokterId} tidak ditemukan");
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
    public async Task<bool> UpdateAsync(int id, RekamMedisRequestDto dto)
    {
        var rekamMedis = await _context.RekamMediss.FirstOrDefaultAsync(r => r.Id == id);
        if (rekamMedis == null) throw new Exception($"Rekam medis dengan Id {id} tidak ditemukan");
        var pasienExist = await _context.Pasiens.AnyAsync(p => p.Id == dto.PasienId);
        if (!pasienExist) throw new Exception($"Pasien dengan id{dto.PasienId} tidak ditemukan");
        var dokterExist = await _context.Dokters.AnyAsync(d => d.Id == dto.DokterId);
        if (!dokterExist) throw new Exception($"Dokter dengan Id {dto.DokterId} tidak ditemukan");
        rekamMedis.PasienId = dto.PasienId;
        rekamMedis.DokterId = dto.DokterId;
        rekamMedis.KeluhanUtama = dto.KeluhanUtama;
        rekamMedis.Diagnosa = dto.Diagnosa;
        rekamMedis.Tindakan = dto.Tindakan;
        rekamMedis.ResepObat = dto.ResepObat;
        rekamMedis.UpdatedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var rekamMedis = await _context.RekamMediss.FirstOrDefaultAsync(r=> r.Id == id);
        if(rekamMedis == null) throw new Exception($"Rekam Medis dengan Id {id} tidak ditemukan");
        rekamMedis.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> RestoreAsync(int id)
    {
        var rekamMedis = await _context.RekamMediss.IgnoreQueryFilters().FirstOrDefaultAsync(r=> r.Id == id);
        if(rekamMedis == null) throw new Exception($"Rekam medis dengan Id {id} tidak ditemukan");
        rekamMedis.IsDeleted = false;
        await _context.SaveChangesAsync();
        return true;
    }
}