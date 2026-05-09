using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniSIMRS.DTOs;
using MiniSIMRS.Services;

namespace MiniSIMRS.Controllers;    
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DokterController : ControllerBase
{
    private readonly IDokterService _service;
    public DokterController(IDokterService service)
    {
        _service = service;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(int pageSize)
    {
        var dokters = await _service.GetAllAsync(pageSize);
        return Ok(dokters);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var dokter = await _service.GetByIdAsync(id);
        return Ok(dokter);
    }
    [HttpPost]
    public async Task<IActionResult> Create(DokterRequestDto dto)
    {
        await _service.CreateAsync(dto);
        return StatusCode(201, new{message = "Data Dokter berhasil diambahkan"});
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, DokterRequestDto dto)
    {
        var dokter = await _service.UpdateAsync(id, dto);
        if(!dokter) return NotFound();
        return Ok(new{message = $"Data dokter dengan ID {id} berhasil di update"});
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var dokter = await _service.DeleteAsync(id);
        if(!dokter) return NotFound();
        return NoContent();
    }
    [HttpPatch("{id}")]
    public async Task<IActionResult> Restore(int id)
    {
        var result = await _service.RestoreAsync(id);
        if(!result) return NotFound();
        return Ok(new{message = $"Dokter dengan ID {id} telah dipulihkan"});
    }
}