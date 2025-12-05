using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VianasCodeLab.Data;
using VianasCodeLab.Model;

namespace VianasCodeLab.Controllers;

public class ComponentController : Controller
{
    private readonly ApiDbContext _context;

    public ComponentController(ApiDbContext context)
    {
        _context = context;
    }

    [HttpGet("component")]
    public async Task<ActionResult<IEnumerable<Component>>> GetAll()
    {
        return await _context.Component.ToListAsync();
    }

    [HttpGet("component/{id:guid}")]
    public async Task<ActionResult<Component>> GetById(Guid id)
    {
        var component = await _context.Component.FindAsync(id);

        if (component == null) return NotFound();

        return component;
    }

    [HttpPost("component")]
    public async Task<ActionResult<Component>> Create([FromBody] Component component)
    {
        if (!ModelState.IsValid) return BadRequest("error");

        _context.Component.Add(component);
        await _context.SaveChangesAsync();

        return Ok(component);
    }

    [HttpPut("component/{id:guid}")]
    public async Task<ActionResult<Component>> Edit(Guid id, [FromBody] Component component)
    {
        if (id != component.id) return BadRequest();

        if (!ModelState.IsValid) return BadRequest();

        _context.Entry(component).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Component.Any(c => c.id == id)) return NotFound();

            throw;
        }

        return Ok();
    }

    [HttpDelete("component/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var component = await _context.Component.FindAsync(id);
        if (component == null)
        {
            return NotFound();
        }

        _context.Component.Remove(component);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
