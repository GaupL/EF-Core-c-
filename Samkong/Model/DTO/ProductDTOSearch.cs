namespace Samkong.Model.DTO
{
    public class ProductDTOSearch
    {
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public decimal? ToPrice { get; set; }
        public int? Status  { get; set; }
        public string? EmpId { get; set; }
        public string? CusId { get; set; }
    }
}
