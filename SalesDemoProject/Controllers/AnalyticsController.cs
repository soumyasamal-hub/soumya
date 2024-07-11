using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using SalesAnalyticsApi.Data;
using SalesDemoProject.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnalyticsController : Controller
    {
        private readonly SalesContext _context;

        public AnalyticsController(SalesContext context)
        {
            _context = context;
        }
        [HttpGet("TotalSales")]
        public async Task<ActionResult<decimal>> GetTotalSales()
        {
            return await _context.SalesRecords.SumAsync(s => s.Price * s.Quantity);
        }
        [HttpGet("TopProducts")]
        public async Task<ActionResult<IEnumerable<object>>> GetTopProducts()
        {
            var topProducts = await _context.SalesRecords
                .GroupBy(s => s.ProductName)
                .Select(g => new
                {
                    ProductName = g.Key,
                    TotalSales = g.Sum(s => s.Price * s.Quantity)
                })
                .OrderByDescending(g => g.TotalSales)
                .Take(5)
                .ToListAsync();

            return Ok(topProducts);
        }
        [HttpGet("SalesTrends")]
        public async Task<ActionResult<IEnumerable<object>>> GetSalesTrends()
        {
            var salesTrends = await _context.SalesRecords
                .GroupBy(s => s.SaleDate.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    TotalSales = g.Sum(s => s.Price * s.Quantity)
                })
                .OrderBy(g => g.Date)
                .ToListAsync();

            return Ok(salesTrends);
        }
    }
}
