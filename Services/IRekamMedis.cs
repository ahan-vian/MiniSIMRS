using MiniSIMRS.DTOs;

namespace MiniSIMRS.Services;
public interface IRekamMedis
{
    Task<IEnumerable<RekamMedisResponseDto?>> GetAllAsync(int pasienId);
    Task CreateAsync(RekamMedisRequestDto dto);
    Task<bool> UpdateAsync(int Id, RekamMedisRequestDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> RestoreAsync(int id);
}