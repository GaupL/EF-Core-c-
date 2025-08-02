namespace Samkong.Model.DTO
{
    public class EmployeeDTO
    {
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Address { get; set; }
        public string? Tel { get; set; }
        public string? Email { get; set; }
        public string? Age { get; set; }
        public IFormFile? Picture { get; set; }
    }
}
