using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
//using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UTMNStudentsExamAnalysis.Models;

namespace UTMNStudentsExamAnalysis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly StudentExamResultsContext _context;

        public ResultsController(StudentExamResultsContext context)
        {
            _context = context;
        }

        // GET: api/Results
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Result>>> GetResults()
        {
          if (_context.Results == null)
          {
              return NotFound();
          }
            return await _context.Results.ToListAsync();
        }

        // GET: api/Results/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Result>> GetResult(int id)
        {
          if (_context.Results == null)
          {
              return NotFound();
          }
            var result = await _context.Results.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpGet("school_code={schoolCode}")]
        public async Task<ActionResult<IEnumerable<Result>>> GetResultBySchoolCode(int schoolCode)
        {
            if (_context.Results == null)
            {
                return NotFound();
            }

            var results = await _context.Results
                                        .Where(result => result.Student.SchoolCode.Equals(schoolCode))
                                        .ToListAsync();

            if (results == null || !results.Any())
            {
                return NotFound();
            }

            return results;
        }

        // PUT: api/Results/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResult(int id, Result result)
        {
            if (id != result.ResultId)
            {
                return BadRequest();
            }

            _context.Entry(result).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultExists(id))
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

        // POST: api/Results
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Result>> PostResult(Result result)
        {
          if (_context.Results == null)
          {
              return Problem("Entity set 'StudentExamResultsContext.Results'  is null.");
          }
            _context.Results.Add(result);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResult", new { id = result.ResultId }, result);
        }

        // DELETE: api/Results/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            if (_context.Results == null)
            {
                return NotFound();
            }
            var result = await _context.Results.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            _context.Results.Remove(result);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResultExists(int id)
        {
            return (_context.Results?.Any(e => e.ResultId == id)).GetValueOrDefault();
        }

        [HttpGet("average")]
        public async Task<ActionResult<IEnumerable<SchoolAverage>>> GetSchoolsAverage([FromQuery] IEnumerable<int> schoolCodes)
        {
            var results = await _context.Results
                .Where(result => schoolCodes.Contains(result.Student.SchoolCode))
                .GroupBy(result => result.Student.SchoolCode)
                .Select(group => new SchoolAverage
                {
                    SchoolCode = group.Key,
                    AverageSecondaryPoints = group.Average(result => result.SecondaryPoints),
                })
                .ToListAsync();

            //Console.WriteLine(results.ToQueryString());
                

            if (results == null || !results.Any())
            {
                return NotFound();
            }


            return results;
        }
    }
}
