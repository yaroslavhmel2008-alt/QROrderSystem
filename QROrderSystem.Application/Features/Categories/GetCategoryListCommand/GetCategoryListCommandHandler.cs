using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Features.GetCategoryListCommand;

public class GetCategoryListCommandHandler : IRequestHandler<GetCategoryListCommand, IEnumerable<CategoryDto>>
{
    private readonly ICategoryService _categoryService;
    
    public GetCategoryListCommandHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IEnumerable<CategoryDto>> Handle(GetCategoryListCommand command, CancellationToken cancellationToken)
    {
        return await _categoryService.GetCategoryListAsync();
    }
}