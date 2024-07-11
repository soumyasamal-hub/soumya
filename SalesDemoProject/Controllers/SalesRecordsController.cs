using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
//using SalesAnalyticsApi.Data;
//using SalesAnalyticsApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesDemoProject.Data;
using SalesDemoProject.Models;

namespace SalesDemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalesRecordsController : Controller
    {
        private readonly SalesContext _context;

        public SalesRecordsController(SalesContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesRecord>>> GetSalesRecords()
        {
            return await _context.SalesRecords.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesRecord>> GetSalesRecord(int id)
        {
            var salesRecord = await _context.SalesRecords.FindAsync(id);

            if (salesRecord == null)
            {
                return NotFound();
            }

            return salesRecord;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesRecord(int id, SalesRecord salesRecord)
        {
            if (id != salesRecord.Id)
            {
                return BadRequest();
            }
            _context.Entry(salesRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesRecordExists(id))
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
        [HttpPost]
        public async Task<ActionResult<SalesRecord>> PostSalesRecord(SalesRecord salesRecord)
        {
            _context.SalesRecords.Add(salesRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalesRecord", new { id = salesRecord.Id }, salesRecord);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesRecord(int id)
        {
            var salesRecord = await _context.SalesRecords.FindAsync(id);
            if (salesRecord == null)
            {
                return NotFound();
            }

            _context.SalesRecords.Remove(salesRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool SalesRecordExists(int id)
        {
            return _context.SalesRecords.Any(e => e.Id == id);
        }

    }
}
