using Microsoft.EntityFrameworkCore;
using ORM_CRUD_homework.Contexts;
using ORM_CRUD_homework.Models;

namespace ORM_CRUD_homework.Services
{
    internal class CategoryService
    {
        public async Task AddCategory(string name)
        {
            using (var context = new AppDbContext())
            {
                var category = new Category
                {
                    Name = name
                };
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                Console.WriteLine("Category was added.");
            }
        }
        public async Task<Category> GetCategoryById(int id)
        {
            using (var context = new AppDbContext())
            {
                var category = await context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.CategoryId == id);
                if (category != null)
                {
                    Console.WriteLine($"ID: {category.CategoryId}, Name: {category.Name}, Products: {category.Products.Count}");
                }
                else
                {
                    Console.WriteLine("Category not found.");
                }
                return category;
            }
        }
        public async Task<List<Category>> GetAllCategories()
        {
            using (var context = new AppDbContext())
            {
                var categories = await context.Categories.Include(c => c.Products).ToListAsync();
                foreach (var category in categories)
                {
                    Console.WriteLine($"ID: {category.CategoryId}, Name: {category.Name}, Products: {category.Products.Count}");
                }
                return categories;
            }
        }
        public async Task UpdateCategory(int id, string newName)
        {
            using (var context = new AppDbContext())
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
                if (category != null)
                {
                    category.Name = newName;
                    await context.SaveChangesAsync();
                    Console.WriteLine("Category updated successfully.");
                }
                else
                {
                    Console.WriteLine("Category not found.");
                }
            }
        }
        public async Task DeleteCategory(int id)
        {
            using (var context = new AppDbContext())
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
                if (category != null)
                {
                    context.Categories.Remove(category);
                    await context.SaveChangesAsync();
                    Console.WriteLine("Category deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Category not found.");
                }
            }
        }
    }
}
