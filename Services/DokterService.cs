using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MiniSIMRS.Data;
using MiniSIMRS.DTOs;
using MiniSIMRS.Models;

namespace MiniSIMRS.Services;

public class DokterService : IDokterService
{
    private readonly AppDbContext _context;
    public DokterService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<DokterResponseDto>> GetAllAsync(int pagenumber)
    {
        const int pageSize = 10;
        pagenumber = pagenumber < 1 ? 1 : pagenumber;
        var query = _context.Dokters.AsQueryable();
        var data = await query.OrderByDescending(d => d.CreatedAt).Skip((pagenumber - 1) * pageSize).Take(pageSize).Select(d => new DokterResponseDto
        {
            Id = d.Id,
            NoSIP = d.NoSIP,
            NamaDokter = d.NamaDokter,
            Spesialisasi = d.Spesialisasi,
        }).ToListAsync();
        return data;
    }
    public async Task<DokterResponseDto?> GetByIdAsync(int id)
    {
        var dokter = await _context.Dokters.Where(d => d.Id == id).Select(d => new DokterResponseDto
        {
            Id = d.Id,
            NoSIP = d.NoSIP,
            NamaDokter = d.NamaDokter,
            Spesialisasi = d.Spesialisasi,
        }).FirstOrDefaultAsync();
        return dokter;
    }
    public async Task CreateAsync(DokterRequestDto dto)
    {
        var exist = await _context.Dokters.AnyAsync(d => d.NoSIP == dto.NoSIP);
        if (exist) throw new Exception("Dokter dengan Nomor Sip tersebut sudah ada");
        var newDokter = new Dokter
        {
            NoSIP = dto.NoSIP,
            NamaDokter = dto.NamaDokter,
            Spesialisasi = dto.Spesialisasi,
            CreatedAt = DateTime.Now,
        };
        _context.Dokters.Add(newDokter);
        await _context.SaveChangesAsync();
    }
    public async Task<bool> UpdateAsync(int id, DokterRequestDto dto)
    {
        var dokter = await _context.Dokters.FirstOrDefaultAsync(d => d.Id == id);
        if (dokter == null) return false;
        dokter.NoSIP = dto.NoSIP;
        dokter.NamaDokter = dto.NamaDokter;
        dokter.Spesialisasi = dto.Spesialisasi;
        dokter.UpdatedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _context.Dokters.FirstOrDefaultAsync(d => d.Id == id);
        if (result == null) return false;
        result.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RestoreAsync(int id)
    {
        var dokter = await _context.Dokters
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(d => d.Id == id);
        if (dokter == null)
            return false;
        dokter.IsDeleted = false;
        await _context.SaveChangesAsync();
        return true;
    }

}