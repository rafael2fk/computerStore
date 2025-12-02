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

        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<Computer>>> GetAll()
        {
            return await _context.Computer.ToListAsync();
        }

        [HttpGet("getbyid/{id:guid}")]
        public async Task<ActionResult<Computer>> GetById(Guid id)
        {
            var computer = await _context.Computer.FindAsync(id);

            if (computer == null) return NotFound();

            return computer;
        }

        [HttpPost("created")]
        public async Task<ActionResult<Computer>> Created([FromBody]Computer computer)
        {
            if (!ModelState.IsValid) return BadRequest("error");

            _context.Computer.Add(computer);
            await _context.SaveChangesAsync();

            return Ok(computer);
        }

        [HttpPut("edit/{id:guid}")]
        public async Task<ActionResult<Computer>> Edit(Guid id, [FromBody] Computer computer)
        {
            if (id != computer.id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            _context.Entry(computer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Computer.Any(c => c.id == id)) return NotFound();

                throw;
            }

            return Ok(computer);
        }

        [HttpDelete("delete/{id:guid}")]
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
    }
}
