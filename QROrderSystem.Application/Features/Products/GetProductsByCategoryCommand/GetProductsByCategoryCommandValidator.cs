using FluentValidation;

namespace QROrderSystem.Application.Features.Products.GetProductsByCategoryCommand;

public class GetProductsByCategoryCommandValidator : AbstractValidator<GetProductsByCategoryCommand>
{
    public GetProductsByCategoryCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("CategoryId is required");
    }
    
}