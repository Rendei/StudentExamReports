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
    public class ReportsDataController : ControllerBase
    {
        private readonly StudentExamResultsContext _context;

        public ReportsDataController(StudentExamResultsContext context)
        {
            _context = context;
        }

        // GET: api/ReportsData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportsData>>> GetReportsData()
        {
          if (_context.ReportsData == null)
          {
              return NotFound();
          }
            return await _context.ReportsData.ToListAsync();
        }

        // GET: api/ReportsData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportsData>> GetReportsData(int id)
        {
          if (_context.ReportsData == null)
          {
              return NotFound();
          }
            var reportsData = await _context.ReportsData.FindAsync(id);

            if (reportsData == null)
            {
                return NotFound();
            }

            return reportsData;
        }

        // PUT: api/ReportsData/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReportsData(int id, ReportsData reportsData)
        {
            if (id != reportsData.ReportsDataId)
            {
                return BadRequest();
            }

            _context.Entry(reportsData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportsDataExists(id))
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

        // POST: api/ReportsData
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReportsData>> PostReportsData(ReportsData reportsData)
        {
          if (_context.ReportsData == null)
          {
              return Problem("Entity set 'StudentExamResultsContext.ReportsData'  is null.");
          }
            _context.ReportsData.Add(reportsData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReportsData", new { id = reportsData.ReportsDataId }, reportsData);
        }

        // DELETE: api/ReportsData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportsData(int id)
        {
            if (_context.ReportsData == null)
            {
                return NotFound();
            }
            var reportsData = await _context.ReportsData.FindAsync(id);
            if (reportsData == null)
            {
                return NotFound();
            }

            _context.ReportsData.Remove(reportsData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReportsDataExists(int id)
        {
            return (_context.ReportsData?.Any(e => e.ReportsDataId == id)).GetValueOrDefault();
        }
    }
}
