using FluentValidation;
using FluentValidation.Validators;

namespace QROrderSystem.Application.Features.CreateCategoryCommand;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(input => input.Name)
            .NotNull().WithMessage("Name cannot be null")
            .NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(100).WithMessage("Name is too long");
        RuleFor(input => input.Description)
            .MaximumLength(500).WithMessage("Description is too long");
    }
}