using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace DAL
{
    public class DataContext : DbContext
    {
        IConfiguration _configuration;
        public DataContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=database-1.c9h8onkbtbz8.us-east-2.rds.amazonaws.com;Port=5432;Database=database-1;User Id=postgres;Password=12345678;");
        }
        public DbSet<Student> Students { get; set; }

    }
}