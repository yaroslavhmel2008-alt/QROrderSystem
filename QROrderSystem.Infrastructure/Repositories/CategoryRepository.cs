using Microsoft.EntityFrameworkCore;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces.Repositories;
using QROrderSystem.Domain.Entities;
using QROrderSystem.Infrastructure.Persistence;

namespace QROrderSystem.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<CategoryEntity>> GetCategoryListAsync()
    {
        return await _context.Categories.ToListAsync();
    }
    public async Task<CategoryEntity?> GetCategoryByNameAsync(string Name)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Name == Name);
    }

    public async Task<CategoryEntity> AddCategoryAsync(CategoryEntity category)
    {
        await _context.Categories.AddAsync(category);
        return category;
    }

    public async Task<CategoryEntity> GetCategoryByIdAsync(Guid Id)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == Id);
    }

    public async Task<bool> DeleteCategoryAsync(CategoryEntity category)
    {
        _context.Categories.Remove(category);
        return await Task.FromResult(true);
    }

    public async Task<CategoryEntity> UpdateCategoryAsync(CategoryEntity category)
    {
        _context.Categories.Update(category);
        return category;
    }
}