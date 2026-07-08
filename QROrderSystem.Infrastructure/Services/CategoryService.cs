using AutoMapper;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Exceptions;
using QROrderSystem.Application.Interfaces.Persistence;
using QROrderSystem.Application.Interfaces.Repositories;
using QROrderSystem.Application.Interfaces.Services;
using QROrderSystem.Domain.Entities;

namespace QROrderSystem.Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(ICategoryRepository categoryRepository,  IUnitOfWork unitOfWork, IMapper mapper, ILogger<CategoryService> logger)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<CategoryDto> CreateCategoryAsync(string Name, string? Description)
    {
        _logger.LogInformation("Attempting to create a new category with Name: {CategoryName}", Name);
        var exsistingCategory = await _categoryRepository.GetCategoryByNameAsync(Name);
        if (exsistingCategory != null)
        {
            _logger.LogWarning("Category with name {CategoryName} already exists", Name);
            throw new BadRequestException("Category", $"Category with name '{Name}' already exists");
        }

        var newCategory = new CategoryEntity()
        {
            Id = Guid.NewGuid(),
            Name = Name,
            Description = Description,
            CreatedAt = DateTime.UtcNow,
            Products = new List<ProductEntity>()
        };
        
        await _categoryRepository.AddCategoryAsync(newCategory);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Category {CategoryName} (ID: {Id}) created successfully", newCategory.Name, newCategory.Id);
        return _mapper.Map<CategoryDto>(newCategory);
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoryListAsync()
    {
        _logger.LogInformation("GetCategoryListAsync");
        var categories = await _categoryRepository.GetCategoryListAsync();
        var categoryList = categories.ToList();
        _logger.LogInformation("Successfully retrieved {Count} categories", categoryList.Count);
        return _mapper.Map<IEnumerable<CategoryDto>>(categoryList);
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id);
        if (category == null)
        {
            _logger.LogWarning("Category with id {CategoryId} does not exist", id);
            throw new NotFoundException("Category", $"Category with id {id} does not exist");
        }
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> UpdateCategoryAsync(Guid Id, string Name, string? Description)
    {
        var categoryToUpdate = await _categoryRepository.GetCategoryByIdAsync(Id);
        if (categoryToUpdate == null)
        {
            _logger.LogWarning("Category with id {CategoryId} does not exist", Id);
            throw new NotFoundException("Category", $"Category with id {Id} does not exist");
        }
        
        var existingCategoryWithSameName = await _categoryRepository.GetCategoryByNameAsync(Name);
        if (existingCategoryWithSameName != null && existingCategoryWithSameName.Id != Id)
        {
            _logger.LogWarning("Category with id {CategoryId} does not exist", Id);
            throw new BadRequestException("Category", $"Category with id {Id} does not exist");
        }
        
        categoryToUpdate.Name = Name;
        categoryToUpdate.Description = Description;
        
        await _categoryRepository.UpdateCategoryAsync(categoryToUpdate);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<CategoryDto>(categoryToUpdate);
        
    }

    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id);
        if (category == null)
        {
            _logger.LogWarning("Category with id {CategoryId} does not exist", id);
            throw new NotFoundException("Category", $"Category with id {id} does not exist");
        }
        
        var isDeleted = await _categoryRepository.DeleteCategoryAsync(category);
        await _unitOfWork.SaveChangesAsync();
    
        _logger.LogInformation("Category with ID {Id} deleted successfully", id);
        return isDeleted;
    }
}