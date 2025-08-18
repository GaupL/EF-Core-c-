namespace Samkong.Model.DTO
{
    public class ChartSaleDTO
    {
        public int MonthId { get; set; }
        public string? MonthName { get; set; }
        public decimal? Price { get; set; }
        public string? NameCus { get; set; }
        //  public string? CusId { get; set; }
    }
    public class SummaryCus
    {
        public string? CusId { get; set; }
        public string? Year { get; set; }
    }
}
