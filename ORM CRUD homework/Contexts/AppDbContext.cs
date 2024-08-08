using Microsoft.EntityFrameworkCore;
using ORM_CRUD_homework.Models;
namespace ORM_CRUD_homework.Contexts
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-OABM90RP;Database=ProductCategoryDb;Integrated Security=True;");
            base.OnConfiguring(optionsBuilder);

        }
    }

}
