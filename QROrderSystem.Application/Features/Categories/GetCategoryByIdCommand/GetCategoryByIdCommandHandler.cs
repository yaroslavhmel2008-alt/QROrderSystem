using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Features.GetCategoryByIdCommand;

public class GetCategoryByIdCommandHandler : IRequestHandler<GetCategoryByIdCommand, CategoryDto>
{
    private readonly ICategoryService _categoryService;
    
    public GetCategoryByIdCommandHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<CategoryDto> Handle(GetCategoryByIdCommand command, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetCategoryByIdAsync(command.Id);
        if (category == null)
        {
            throw new Exception($"Category {command.Id} does not exist");
        }
        return category;
    }
}