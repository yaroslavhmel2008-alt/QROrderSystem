using QROrderSystem.Application.DTOs;

namespace QROrderSystem.Application.Interfaces;

public interface ICategoryService
{
    Task<CategoryDto> CreateCategoryAsync(string Name, string? Description);
    Task<IEnumerable<CategoryDto>> GetCategoryListAsync();
    Task<CategoryDto> GetCategoryByIdAsync(Guid id);
    Task<CategoryDto> UpdateCategoryAsync(Guid id, string Name, string? Description);
    Task<bool> DeleteCategoryAsync(Guid id);
}