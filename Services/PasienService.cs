using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using MiniSIMRS.Data;
using MiniSIMRS.DTOs;
using MiniSIMRS.Models;

namespace MiniSIMRS.Services;
public class PasienService : IPasienService
{
    private readonly AppDbContext _context;
    public PasienService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<PasienResponseDto>> GetAllAsync(int pagenumber)
    {
        const int pageSize = 10;
        pagenumber = pagenumber < 1 ? 1:pagenumber;
        var query = _context.Pasiens.AsQueryable();
        
        var data = await query.OrderBy(p=>p.CreatedAt).Skip((pagenumber -1) * pageSize).Take(pageSize).Select(p=> new PasienResponseDto
        {
            Id = p.Id,
            NoRekamMedis = p.NoRekamMedis,
            NamaLengkap = p.NamaLengkap,
            NIK = p.NIK,
            JenisKelamin = p.JenisKelamin,
            GolonganDarah = p.GolonganDarah,
            TanggalLahir = p.TanggalLahir,
        }).ToListAsync();
        
        return data;
    }
    public async Task<PasienResponseDto?> GetByIdAsync(int id)
    {
        var pasien = await _context.Pasiens.Where(p=> p.Id == id).Select(p=> new PasienResponseDto
        {
            Id = p.Id,
            NoRekamMedis = p.NoRekamMedis,
            NamaLengkap = p.NamaLengkap,
            NIK = p.NIK,
            JenisKelamin = p.JenisKelamin,
            GolonganDarah = p.GolonganDarah,
            TanggalLahir = p.TanggalLahir,
        }).FirstOrDefaultAsync();
        return pasien;
    }
    public async Task CreateAsync(PasienRequestDto dto)
    {
        var nikExist = await _context.Pasiens.AnyAsync(n=> n.NIK == dto.NIK);
        if(nikExist) throw new Exception("NIK sudah terdaftar");
        var newPasien = new Pasien
        {
            NoRekamMedis = "RM" + DateTime.Now.ToString("yyyyMMddHHmmss"),
            NamaLengkap = dto.NamaLengkap,
            NIK = dto.NIK,
            JenisKelamin = dto.JenisKelamin,
            GolonganDarah = dto.GolonganDarah,
            TanggalLahir = dto.TanggalLahir,
            CreatedAt = DateTime.Now
        };
        _context.Pasiens.Add(newPasien);
        await _context.SaveChangesAsync();
    }
    public async Task<bool> UpdateAsync(int id, PasienRequestDto dto)
    {
        var pasien = await _context.Pasiens.FindAsync(id);
        if(pasien == null) return false;
        pasien.NIK = dto.NIK;
        pasien.NamaLengkap = dto.NamaLengkap;
        pasien.JenisKelamin = dto.JenisKelamin;
        pasien.GolonganDarah = dto.GolonganDarah;
        pasien.TanggalLahir = dto.TanggalLahir;
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var pasien = await _context.Pasiens.FindAsync(id);
        if(pasien == null) return false;
        pasien.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> RestoreAsync(int id)
    {
        var pasien = await _context.Pasiens
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(d => d.Id == id);
        if (pasien == null)
            return false;
        pasien.IsDeleted = false;
        await _context.SaveChangesAsync();
        return true;
    }
}