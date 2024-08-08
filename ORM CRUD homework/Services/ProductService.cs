using Microsoft.EntityFrameworkCore;
using ORM_CRUD_homework.Contexts;
using ORM_CRUD_homework.Models;

namespace ORM_CRUD_homework.Services
{
    internal class ProductService
    {
        public async Task AddProduct(string name, int categoryId)
        {
            using (var context = new AppDbContext())
            {
                var product = new Product
                {
                    Name = name,
                    CategoryId = categoryId
                };
                await context.Products.AddAsync(product);
                await context.SaveChangesAsync();
                Console.WriteLine("Product added successfully.");
            }

        }
        public async Task<Product> GetProductById(int id)
        {
            using (var context = new AppDbContext())
            {
                var product = await context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);
                if (product != null)
                {
                    Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Category: {product.Category.Name}");
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
                return product;
            }
        }
        public async Task<List<Product>> GetAllProducts()
        {
            using (var context = new AppDbContext())
            {
                var products = await context.Products.Include(p => p.Category).ToListAsync();
                foreach (var product in products)
                {
                    Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Category: {product.Category.Name}");
                }
                return products;
            }
        }
        public async Task UpdateProduct(int id, string newName, int newCategoryId)
        {
            using (var context = new AppDbContext())
            {
                var product = await context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
                if (product != null)
                {
                    product.Name = newName;
                    product.CategoryId = newCategoryId;
                    await context.SaveChangesAsync();
                    Console.WriteLine("Product was updated.");
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
        }
        public async Task DeleteProduct(int id)
        {
            using (var context = new AppDbContext())
            {
                var product = await context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
                if (product != null)
                {
                    context.Products.Remove(product);
                    await context.SaveChangesAsync();
                    Console.WriteLine("Product deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }

        }
    }
}

