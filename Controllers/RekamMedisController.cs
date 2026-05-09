using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniSIMRS.DTOs;
using MiniSIMRS.Services;

namespace MiniSIMRS.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RekamMedisController : ControllerBase
{
    private readonly IRekamMedis _service;
    public RekamMedisController(IRekamMedis service)
    {
        _service = service;
    }
    [HttpGet("{pasienId}")]
    public async Task<IActionResult> GetAll(int pasienId)
    {
        var rekamMedis = await _service.GetAllAsync(pasienId);
        if(rekamMedis == null) return NotFound();
        return Ok(rekamMedis);
    }
    [HttpPost]
    public async Task<IActionResult> Create(RekamMedisRequestDto dto)
    {
        await _service.CreateAsync(dto);
        return StatusCode(201, new {message = "Data Rekam medis berhasil ditambahkan"});
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, RekamMedisRequestDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);
        if(!result) return NotFound();
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        if(!result) return NotFound();
        return Ok($"Data Rekam Medis dengan Id {id} berhasil dihapus");
    }
    [HttpPatch("{id}")]
    public async Task<IActionResult> Restore(int id)
    {
        var result = await _service.RestoreAsync(id);
        if(!result) return NotFound();
        return Ok($"Data Rekam medis dengan Id{id} berhasil dipulihkan");
    }
}