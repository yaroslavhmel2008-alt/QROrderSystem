using AutoMapper;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces.Persistence;
using QROrderSystem.Application.Interfaces.Services;
using QROrderSystem.Application.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.Exceptions;
using QROrderSystem.Domain.Entities;

namespace QROrderSystem.Infrastructure.Services;

public class ProductService : IProductService
{
    
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;
    public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    } 
    
    public Task<ProductDto> GetProductByIdAsync(Guid productId)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductDto> AddProductAsync(Guid categoryId, string name, string? description, decimal price, string? imageUrl,
        bool isAvailable)
    {
        var existingProduct = await _productRepository.GetProductByNameAsync(name);
        if (existingProduct != null)
        {
            _logger.LogWarning("Product with name {ProductName} already exists", name);
            throw new BadRequestException("Product", $"Product with name '{name}' already exists");
        }
        
        var newProduct = new ProductEntity
        {
            Id = Guid.NewGuid(),
            CategoryId = categoryId,
            Name = name,
            Description = description,
            Price = price,
            ImageUrl = imageUrl,
            IsAvailable = isAvailable
        };
        
        await _productRepository.AddProductAsync(newProduct);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Product {ProductName} created successfully with ID {ProductId}", newProduct.Name, newProduct.Id);
        return _mapper.Map<ProductDto>(newProduct);
    }

    public async Task<IEnumerable<ProductDto>> GetProductListAsync()
    {
        _logger.LogInformation("Attempting to retrieve all products");
        var products = await _productRepository.GetProductListAsync();
        var productList = products.ToList();
        _logger.LogInformation("Successfully retrieved {Count} products", productList.Count);
        return _mapper.Map<IEnumerable<ProductDto>>(productList);
    }

    public async Task<ProductDto> UpdateProductAsync(Guid Id, Guid categoryId, string name, string? description, decimal price, string? imageUrl,
        bool isAvailable)
    {
        var productToUpdate = await _productRepository.GetProductByIdAsync(Id);
        if (productToUpdate == null)
        {
            _logger.LogWarning("Product with ID {Id} was not found", Id);
            throw new NotFoundException("Product", Id);
        }
        
        var existingProductWithSameName = await _productRepository.GetProductByNameAsync(name);
        if (existingProductWithSameName != null && existingProductWithSameName.Id != Id)
        {
            _logger.LogWarning("Another product with name {Name} already exists", name);
            throw new BadRequestException("Product", $"Product with name '{name}' already exists");
        }

        productToUpdate.Name = name;
        productToUpdate.Description = description;
        productToUpdate.Price = price;
        productToUpdate.ImageUrl = imageUrl;
        productToUpdate.IsAvailable = isAvailable;
        
        await _productRepository.UpdateProductAsync(productToUpdate);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ProductDto>(productToUpdate);
    }

    public Task<bool> DeleteProductAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(Guid categoryId)
    {
        _logger.LogInformation("Attempting to retrieve products for CategoryId: {CategoryId}", categoryId);
        var productsByCategoryId = await _productRepository.GetProductsByCategoryIdAsync(categoryId);
        var productListByCategoryId = productsByCategoryId.ToList();
        _logger.LogInformation("Successfully retrieved {Count} products for CategoryId: {CategoryId}", productListByCategoryId.Count, categoryId);
        return _mapper.Map<IEnumerable<ProductDto>>(productListByCategoryId);
    }
}