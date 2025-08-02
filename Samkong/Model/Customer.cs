namespace Samkong.Model
{
    public class Customer
    {
        public string CusId { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Address { get; set; }
        public string? Tel { get; set; }
        public DateTime DOB { get; set; }
        public DateTime Createdate { get; set; } 
        public DateTime Updatedate { get; set; } 
        public ICollection<Product>? products { get; set; }
    }
}
