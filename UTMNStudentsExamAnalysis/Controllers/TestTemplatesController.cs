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
    public class TestTemplatesController : ControllerBase
    {
        private readonly StudentExamResultsContext _context;

        public TestTemplatesController(StudentExamResultsContext context)
        {
            _context = context;
        }

        // GET: api/TestTemplates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestTemplate>>> GetTestTemplates()
        {
          if (_context.TestTemplates == null)
          {
              return NotFound();
          }
            return await _context.TestTemplates.ToListAsync();
        }

        [HttpGet("years")]
        public async Task<ActionResult<IEnumerable<int>>> GetYears()
        {
            if (_context.TestTemplates == null)
            {
                return NotFound();
            }

            var years = await _context.TestTemplates
                .Select(testTemplate => int.Parse(testTemplate.Year))
                .Distinct()
                .ToListAsync();

            if (years == null || !years.Any())
            {
                return NotFound();
            }

            years.Sort();

            return years;

        }

        // GET: api/TestTemplates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestTemplate>> GetTestTemplate(int id)
        {
          if (_context.TestTemplates == null)
          {
              return NotFound();
          }
            var testTemplate = await _context.TestTemplates.FindAsync(id);

            if (testTemplate == null)
            {
                return NotFound();
            }

            return testTemplate;
        }

        // PUT: api/TestTemplates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestTemplate(int id, TestTemplate testTemplate)
        {
            if (id != testTemplate.TestTemplateId)
            {
                return BadRequest();
            }

            _context.Entry(testTemplate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestTemplateExists(id))
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

        // POST: api/TestTemplates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TestTemplate>> PostTestTemplate(TestTemplate testTemplate)
        {
          if (_context.TestTemplates == null)
          {
              return Problem("Entity set 'StudentExamResultsContext.TestTemplates'  is null.");
          }
            _context.TestTemplates.Add(testTemplate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTestTemplate", new { id = testTemplate.TestTemplateId }, testTemplate);
        }

        // DELETE: api/TestTemplates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestTemplate(int id)
        {
            if (_context.TestTemplates == null)
            {
                return NotFound();
            }
            var testTemplate = await _context.TestTemplates.FindAsync(id);
            if (testTemplate == null)
            {
                return NotFound();
            }

            _context.TestTemplates.Remove(testTemplate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TestTemplateExists(int id)
        {
            return (_context.TestTemplates?.Any(e => e.TestTemplateId == id)).GetValueOrDefault();
        }
    }
}
