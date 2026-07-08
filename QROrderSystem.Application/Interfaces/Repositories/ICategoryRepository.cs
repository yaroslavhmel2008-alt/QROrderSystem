using QROrderSystem.Application.DTOs;
using QROrderSystem.Domain.Entities;

namespace QROrderSystem.Application.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryEntity>> GetCategoryListAsync();
    Task<CategoryEntity?> GetCategoryByNameAsync(string Name);
    Task<CategoryEntity> AddCategoryAsync(CategoryEntity category);
    Task<CategoryEntity> UpdateCategoryAsync(CategoryEntity category);
    Task<CategoryEntity> GetCategoryByIdAsync(Guid Id);
    Task<bool> DeleteCategoryAsync(CategoryEntity category);
}