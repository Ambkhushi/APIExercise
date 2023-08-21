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
    public class ProductInfoesController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public ProductInfoesController(ProductDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductInfo>>> GetProductInfos()
        {
          if (_context.ProductInfos == null)
          {
              return NotFound();
          }
            return await _context.ProductInfos.ToListAsync();
        }

        // GET: api/ProductInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductInfo>> GetProductInfo(int id)
        {
          if (_context.ProductInfos == null)
          {
              return NotFound();
          }
            var productInfo = await _context.ProductInfos.FindAsync(id);

            if (productInfo == null)
            {
                return NotFound();
            }

            return productInfo;
        }

        // PUT: api/ProductInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductInfo(int id, ProductInfo productInfo)
        {
            if (id != productInfo.Pid)
            {
                return BadRequest();
            }

            _context.Entry(productInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductInfoExists(id))
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

        // POST: api/ProductInfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductInfo>> PostProductInfo(ProductInfo productInfo)
        {
          if (_context.ProductInfos == null)
          {
              return Problem("Entity set 'ProductDbContext.ProductInfos'  is null.");
          }
            _context.ProductInfos.Add(productInfo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductInfoExists(productInfo.Pid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProductInfo", new { id = productInfo.Pid }, productInfo);
        }

        // DELETE: api/ProductInfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductInfo(int id)
        {
            if (_context.ProductInfos == null)
            {
                return NotFound();
            }
            var productInfo = await _context.ProductInfos.FindAsync(id);
            if (productInfo == null)
            {
                return NotFound();
            }

            _context.ProductInfos.Remove(productInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductInfoExists(int id)
        {
            return (_context.ProductInfos?.Any(e => e.Pid == id)).GetValueOrDefault();
        }
    }
}
