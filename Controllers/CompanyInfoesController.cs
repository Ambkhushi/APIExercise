using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyInfoesController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public CompanyInfoesController(ProductDbContext context)
        {
            _context = context;
        }

        // GET: api/CompanyInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyInfo>>> GetCompanyInfos()
        {
          if (_context.CompanyInfos == null)
          {
              return NotFound();
          }
            return await _context.CompanyInfos.ToListAsync();
        }

        // GET: api/CompanyInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyInfo>> GetCompanyInfo(int id)
        {
          if (_context.CompanyInfos == null)
          {
              return NotFound();
          }
            var companyInfo = await _context.CompanyInfos.FindAsync(id);

            if (companyInfo == null)
            {
                return NotFound();
            }

            return companyInfo;
        }

        // PUT: api/CompanyInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompanyInfo(int id, CompanyInfo companyInfo)
        {
            if (id != companyInfo.Cid)
            {
                return BadRequest();
            }

            _context.Entry(companyInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyInfoExists(id))
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

        // POST: api/CompanyInfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompanyInfo>> PostCompanyInfo(CompanyInfo companyInfo)
        {
          if (_context.CompanyInfos == null)
          {
              return Problem("Entity set 'ProductDbContext.CompanyInfos'  is null.");
          }
            _context.CompanyInfos.Add(companyInfo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CompanyInfoExists(companyInfo.Cid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCompanyInfo", new { id = companyInfo.Cid }, companyInfo);
        }

        // DELETE: api/CompanyInfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyInfo(int id)
        {
            if (_context.CompanyInfos == null)
            {
                return NotFound();
            }
            var companyInfo = await _context.CompanyInfos.FindAsync(id);
            if (companyInfo == null)
            {
                return NotFound();
            }

            _context.CompanyInfos.Remove(companyInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyInfoExists(int id)
        {
            return (_context.CompanyInfos?.Any(e => e.Cid == id)).GetValueOrDefault();
        }
    }
}
