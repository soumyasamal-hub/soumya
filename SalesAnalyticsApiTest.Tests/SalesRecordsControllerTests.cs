using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
//using SalesAnalyticsApi.Controllers;
//using SalesAnalyticsApi.Data;
//using SalesAnalyticsApi.Models;
using SalesDemoProject.Controllers;
using SalesDemoProject.Data;
using SalesDemoProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SalesAnalyticsApi.Tests
{
    public class SalesRecordsControllerTests
    {
        private readonly Mock<SalesContext> _mockContext;
        private readonly SalesRecordsController _controller;

        public SalesRecordsControllerTests()
        {
            //var options = new DbContextOptionsBuilder<SalesContext>()
            //    .UseInMemoryDatabase(databaseName: "TestDatabase")
            //    .Options;
            _mockContext = new Mock<SalesContext>();
            _controller = new SalesRecordsController(_mockContext.Object);
        }

        [Fact]
        public async Task GetSalesRecords_ReturnsAllRecords()
        {
            var records = new List<SalesRecord>
            {
                new SalesRecord { Id = 1, ProductName = "Product1", Quantity = 10, Price = 100, SaleDate = DateTime.Now },
                new SalesRecord { Id = 2, ProductName = "Product2", Quantity = 5, Price = 50, SaleDate = DateTime.Now }
            };

            //_mockContext.Setup(c => c.SalesRecords.ToListAsync())
            //    .ReturnsAsync(records);

            var result = await _controller.GetSalesRecords();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<SalesRecord>>>(result);
            var returnValue = Assert.IsType<List<SalesRecord>>(actionResult.Value);
            Assert.Equal(2, returnValue.Count);
        }
    }
}
