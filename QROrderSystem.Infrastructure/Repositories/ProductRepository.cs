using Microsoft.EntityFrameworkCore;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces.Repositories;
using QROrderSystem.Domain.Entities;
using QROrderSystem.Infrastructure.Persistence;

namespace QROrderSystem.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<ProductEntity?> GetProductByNameAsync(string name)
    {
        return await _context.Products.FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task<ProductEntity> AddProductAsync(ProductEntity productEntity)
    {
        await _context.Products.AddAsync(productEntity);
        return productEntity;
    }

    public Task<ProductEntity> UpdateProductAsync(ProductEntity productEntity)
    {
        _context.Products.Update(productEntity);
        return Task.FromResult(productEntity);
    }

    public async Task<ProductEntity?> GetProductByIdAsync(Guid Id)
    {
        return await _context.Products.FirstOrDefaultAsync(c => c.Id == Id);
    }

    public async Task<IEnumerable<ProductEntity>> GetProductListAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<IEnumerable<ProductEntity>> GetProductsByCategoryIdAsync(Guid categoryId)
    {
        return await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
    }

    public Task<bool> DeleteProductByIdAsync(ProductEntity productEntity)
    {
        _context.Products.Remove(productEntity);
        return Task.FromResult(true);
    }
}