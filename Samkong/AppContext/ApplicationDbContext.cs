using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Samkong.Model;

namespace Samkong.AppContext
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Customer>().HasKey(c => c.CusId);
            builder.Entity<Employee>().HasKey(e => e.EmpId);
            builder.Entity<Product>().HasKey(p => p.ProductId);
            builder.Entity<Month>().HasKey(m => m.MonthId);
            // builder.Entity<Product>().Property(p => p.Price).HasPrecision(10,2); //12345.67
            builder.Entity<Product>().HasOne(c => c.customers).WithMany(p => p.products).HasForeignKey(p => p.CusId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Product>().HasOne(c => c.employees).WithMany(p => p.products).HasForeignKey(p => p.EmpId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Product>().HasOne(p => p.Registers).WithMany(r => r.Products).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Product>().HasOne(p => p.months).WithMany(m=>m.Products).HasForeignKey(p=>p.MonthId).OnDelete(DeleteBehavior.Cascade);
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Month> Months { get; set; }
    }
}
