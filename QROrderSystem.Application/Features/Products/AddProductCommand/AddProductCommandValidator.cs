using FluentValidation;

namespace QROrderSystem.Application.Features.Products.AddProductCommand;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("CategoryId is required");
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(100)
            .WithMessage("Name cannot exceed 100 characters");
        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("Description cannot exceed 500 characters")
            .When(x => !string.IsNullOrEmpty(x.Description));
        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0")
            .PrecisionScale(18, 2, true)
            .WithMessage("Price can have at most 2 decimal places");
        RuleFor(x => x.ImageUrl)
            .MaximumLength(200)
            .WithMessage("ImageUrl cannot exceed 200 characters")
            .When(x => !string.IsNullOrEmpty(x.ImageUrl));
    }
    
}