using Microsoft.AspNetCore.Mvc;
using MiniSIMRS.DTOs;

namespace MiniSIMRS.Services;
public interface IPasienService
{
    Task<IEnumerable<PasienResponseDto>> GetAllAsync(int pagenumber);
    Task<PasienResponseDto?> GetByIdAsync(int id);
    Task CreateAsync(PasienRequestDto dto);
    Task<bool> UpdateAsync(int id, PasienRequestDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> RestoreAsync(int id);
}