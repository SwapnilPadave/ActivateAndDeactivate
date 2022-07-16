using Microsoft.EntityFrameworkCore;


namespace CoreCurdApplicationWithRoleBased.Models
{
   
    public class CompanyDBContext: DbContext
    {
        public CompanyDBContext(DbContextOptions<CompanyDBContext> options):base(options)
        {

        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
    }
}
