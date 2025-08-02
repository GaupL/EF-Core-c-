namespace Samkong.Model.DTO
{
    public class ProductDTO1
    {
        public string? ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public string? Contact { get; set; }
        public int? Status = 0; 
        public string? Tel { get; set; }
        public string? EmpId { get; set; }
        public string? EmpName { get; set; }
        public string? CusId { get; set; }
        public string? CusName { get; set; }
        public int MonthId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
