using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UTMNStudentsExamAnalysis.Models;

namespace UTMNStudentsExamAnalysis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TownTypesController : ControllerBase
    {
        private readonly StudentExamResultsContext _context;

        public TownTypesController(StudentExamResultsContext context)
        {
            _context = context;
        }

        // GET: api/TownTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TownType>>> GetTownTypes()
        {
          if (_context.TownTypes == null)
          {
              return NotFound();
          }
            return await _context.TownTypes.ToListAsync();
        }

        // GET: api/TownTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TownType>> GetTownType(int id)
        {
          if (_context.TownTypes == null)
          {
              return NotFound();
          }
            var townType = await _context.TownTypes.FindAsync(id);

            if (townType == null)
            {
                return NotFound();
            }

            return townType;
        }

        // PUT: api/TownTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTownType(int id, TownType townType)
        {
            if (id != townType.TownTypeId)
            {
                return BadRequest();
            }

            _context.Entry(townType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TownTypeExists(id))
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

        // POST: api/TownTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TownType>> PostTownType(TownType townType)
        {
          if (_context.TownTypes == null)
          {
              return Problem("Entity set 'StudentExamResultsContext.TownTypes'  is null.");
          }
            _context.TownTypes.Add(townType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTownType", new { id = townType.TownTypeId }, townType);
        }

        // DELETE: api/TownTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTownType(int id)
        {
            if (_context.TownTypes == null)
            {
                return NotFound();
            }
            var townType = await _context.TownTypes.FindAsync(id);
            if (townType == null)
            {
                return NotFound();
            }

            _context.TownTypes.Remove(townType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TownTypeExists(int id)
        {
            return (_context.TownTypes?.Any(e => e.TownTypeId == id)).GetValueOrDefault();
        }
    }
}
