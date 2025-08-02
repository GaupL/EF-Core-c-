using Microsoft.AspNetCore.Identity;

namespace Samkong.Model
{
    public class Register:IdentityUser
    {
        public string? FulllName { get; set; }
        public string? NickName { get; set; }
        public string? Address { get; set; }
        public DateOnly DOB { get; set; }
        public ICollection<Product>? Products { get; set; }

    }
}
