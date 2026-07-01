using MediatR;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Features.DeleteCategoryCommand;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly ICategoryService _categoryService;
    
    public DeleteCategoryCommandHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<bool> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        
        var isDeleted = await _categoryService.DeleteCategoryAsync(command.Id);
        if (!isDeleted)
        {
            throw new Exception($"Category {command.Id} does not exist");
        }
        
        return isDeleted;
    }
}