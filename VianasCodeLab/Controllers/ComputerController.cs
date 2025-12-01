using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VianasCodeLab.Data;
using VianasCodeLab.Model;

namespace VianasCodeLab.Controllers
{
    public class ComputerController : Controller
    {
        private readonly ApiDbContext _context;

        public ComputerController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<Computer>>> GetAll()
        {
            return await _context.Computer.ToListAsync();
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<Computer>> GetById(Guid id)
        {
            var computer = await _context.Computer.FindAsync(id);

            if (computer == null)
            {
                return NotFound();
            }

            return computer;
        }

        [HttpPost("Created")]
        public async Task<ActionResult<Computer>> Created(Computer computer)
        {
            _context.Computer.Add(computer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = computer.id }, computer);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(Guid id, Computer computer)
        {
            if (id != computer.id)
            {
                return BadRequest();
            }

            _context.Entry(computer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComputerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var computer = await _context.Computer.FindAsync(id);
            if (computer == null)
            {
                return NotFound();
            }

            _context.Computer.Remove(computer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComputerExists(Guid id)
        {
            return _context.Computer.Any(e => e.id == id);
        }
    }
}
