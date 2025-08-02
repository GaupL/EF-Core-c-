namespace Samkong.Model
{
    public class Product
    {
        public string ProductId { get; set; } = Guid.NewGuid().ToString();
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public string? Contact { get; set; }
        public string? Tel { get; set; }
        public int Status { get; set; }
        public string? UserId { get; set; }
        public Register? Registers { get; set; }
        public string? EmpId { get; set; }
        public Employee? employees { get; set; }
        public string? CusId { get; set; }
        public Customer? customers { get; set; }
        public int MonthId { get; set; }
        public Month? months  { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
