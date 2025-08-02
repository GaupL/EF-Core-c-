namespace Samkong.Model
{
    public class Month
    {
        public int MonthId { get; set; }
        public string? MonthName { get; set; }
        public int Sort { get; set; }
        public ICollection<Product>? Products  { get; set; }

    }
}
