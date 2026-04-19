using dotnet_example_clean_arch_with_entity_framework.Data;
using dotnet_example_clean_arch_with_entity_framework.Models;
using dotnet_example_clean_arch_with_entity_framework.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_example_clean_arch_with_entity_framework.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Products>> GetAll() =>
            await _context.Products.ToListAsync();

        public async Task<Products> GetById(int id) =>
            await _context.Products.FindAsync(id);

        public async Task<int> Add(Products product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task Update(Products product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Products product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id) =>
            await _context.Products.AnyAsync(x => x.Id == id);

    }
}
