namespace SalesDemoProject.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
