using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniSIMRS.DTOs;
using MiniSIMRS.Services;

namespace MiniSIMRS.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PasienController : ControllerBase
{
    private readonly IPasienService _service;
    public PasienController(IPasienService service)
    {
        _service = service;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery]int pageNumber)
    {
        var pasiens = await _service.GetAllAsync(pageNumber);
        return Ok(pasiens);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetbyId(int id)
    {
        var pasien = await _service.GetByIdAsync(id);
        if(pasien == null) return NotFound();
        return Ok(pasien);
    }
    [HttpPost]
    public async Task<IActionResult> Create(PasienRequestDto dto)
    {
        await _service.CreateAsync(dto);
        return StatusCode(201, new{message = "Data Pasien berhasil ditambahkan"});
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PasienRequestDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);
        if(!result) return NotFound("Pasien tidak ditemukan");
        return Ok(new{message = "Data Pasien berhasil di update"});
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        if(!result) return NotFound("Data Pasien tidak ditemukan");
        return NoContent();    
    }
    [HttpPatch("{id}")]
    public async Task<IActionResult> Restore(int id)
    {
        var result = await _service.RestoreAsync(id);
        if(!result) return NotFound($"Data Pasien dengan ID {id} tidak ditemukan");
        return Ok(new{message = $"Data Pasien dengan ID {id} telah dipulihkan"});
    }
}