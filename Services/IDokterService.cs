using MiniSIMRS.DTOs;

namespace MiniSIMRS.Services;
public interface IDokterService
{
    Task<IEnumerable<DokterResponseDto>> GetAllAsync(int pagenumber);
    Task<DokterResponseDto?> GetByIdAsync(int id);
    Task CreateAsync(DokterRequestDto dto);
    Task<bool> UpdateAsync(int id, DokterRequestDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> RestoreAsync(int id);
}