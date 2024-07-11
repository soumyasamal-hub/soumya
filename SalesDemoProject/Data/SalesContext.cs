using Microsoft.EntityFrameworkCore;
using SalesDemoProject.Models;

namespace SalesDemoProject.Data
{
    public class SalesContext:DbContext
    {
        public SalesContext(DbContextOptions<SalesContext> options) : base(options) { }
        public DbSet<SalesRecord> SalesRecords { get; set; }
        public object SalesRecord { get; set; }
    }
}
