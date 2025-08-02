namespace Samkong.Model
{
    public class Employee
    {
        public string EmpId { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Address { get; set; }
        public string? Tel { get; set; }
        public string? Email { get; set; }
        public string? Age { get; set; }
        public string? picture { get; set; }
        public DateTime Createdate { get; set; } 
        public DateTime? Updatedate { get; set; } 
        public ICollection<Product>? products { get; set; }
    }
}
