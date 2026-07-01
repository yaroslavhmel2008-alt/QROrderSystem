using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Services;

public class CategoryService : ICategoryService
{
    public Task<CategoryDto> CreateCategoryAsync(string Name, string? Description)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CategoryDto>> GetCategoryListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CategoryDto> GetCategoryByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryDto> UpdateCategoryAsync(Guid id, string Name, string? Description)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCategoryAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}