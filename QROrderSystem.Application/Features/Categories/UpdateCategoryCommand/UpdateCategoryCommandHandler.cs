using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Features.UpdateCategoryCommand;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
{
    private readonly ICategoryService _categoryService;
    
    public UpdateCategoryCommandHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<CategoryDto> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        
        var category = await _categoryService.UpdateCategoryAsync(command.Id, command.Name, command?.Description);
        if (category == null)
        {
            throw new Exception($"Category {command.Id} does not exist");
        }
        
        return category;
    }
}