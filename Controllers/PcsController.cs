using Cwiczenie7.Data;
using Cwiczenie7.DTOs;
using Cwiczenie7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenie7.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PcsController : ControllerBase
{
    private readonly AppDbContext _context;
    public PcsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPcs()
    {
        var pcs = await _context.PCs.Select(pc => new PcWithComponentsResponseDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty =  pc.Warranty,
            CreatedAt =  pc.CreatedAt,
            Stock =  pc.Stock
        }).ToListAsync();
        return Ok(pcs);
    }

    [HttpGet("{id}/components")]
    public async Task<IActionResult> GetPcWithComponents(int id)
    {
        var pc = await _context.PCs
            .Include(p => p.PCComponents)
            .ThenInclude(pc => pc.Component)
            .ThenInclude(c => c.ComponentManufacturer)
            .Include(p => p.PCComponents)
            .ThenInclude(pc => pc.Component)
            .ThenInclude(c => c.ComponentType)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (pc == null)
        {
            return NotFound();
        }

        var response = new PcWithComponentsResponseDto
        {
            Id = pc.Id, Name = pc.Name, Weight = pc.Weight, Warranty = pc.Warranty, CreatedAt = pc.CreatedAt
            , Stock = pc.Stock, Components = pc.PCComponents.Select(c => new PCComponentDetailDto
            {
                Amount = c.Amount, Component = new ComponentDetailDto()
                {
                    Code = c.Component.Code, Name = c.Component.Name, Description = c.Component.Description
                    , Manufacturer = new ManufacturerDto
                    {
                        Id = c.Component.ComponentManufacturer.Id
                        , Abbrevation = c.Component.ComponentManufacturer.Abbreviation
                        , FullName = c.Component.ComponentManufacturer.FullName
                        , FoundationDate = c.Component.ComponentManufacturer.FoundationDate
                    }
                    , Type = new ComponentTypeDto
                    {
                        Id = c.Component.ComponentType.Id, Abbrevation = c.Component.ComponentType.Abbreviation
                        , Name = c.Component.ComponentType.Name
                    }
                }
            }).ToList()
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePc([FromBody] PcRequestDto request)
    {
        var newPc = new PC()
        {
            Name = request.Name, Weight = request.Weight, Warranty = request.Warranty, CreatedAt = request.CreatedAt
            , Stock = request.Stock
        };
        _context.PCs.Add(newPc);
        await _context.SaveChangesAsync();
        var responseDto = new PcResponseDto
        {
            Id = newPc.Id, Name = newPc.Name, Weight = newPc.Weight, Warranty = newPc.Warranty
            , CreatedAt = newPc.CreatedAt, Stock = newPc.Stock
        };
        return CreatedAtAction(nameof(GetAllPcs), new { id = newPc.Id }, responseDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePc(int id, [FromBody] PcRequestDto request)
    {
        var existing = await _context.PCs.FindAsync(id);
        if (existing == null)
        {
            return NotFound();
        }
        existing.Name = request.Name;
        existing.Weight = request.Weight;
        existing.Warranty = request.Warranty;
        existing.CreatedAt = request.CreatedAt;
        existing.Stock = request.Stock;

        await _context.SaveChangesAsync();

        var reposnseDto = new PcResponseDto
        {
            Id = existing.Id, Name = existing.Name, Weight = existing.Weight, Warranty = existing.Warranty
            , CreatedAt = existing.CreatedAt, Stock = existing.Stock
        };
        return Ok(reposnseDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePc(int id)
    {
        var pc = await _context.PCs.FindAsync(id);
        if (pc == null)
        {
            return NotFound();
        }
        _context.PCs.Remove(pc);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}