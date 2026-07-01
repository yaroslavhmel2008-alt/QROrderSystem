using FluentValidation;
using FluentValidation.Validators;

namespace QROrderSystem.Application.Features.UpdateCategoryCommand;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("Id cannot be null")
            .NotEmpty()
            .WithMessage("Id cannot be empty");
        RuleFor(input => input.Name)
            .NotNull().WithMessage("Name cannot be null")
            .NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(100).WithMessage("Name is too long");
        RuleFor(input => input.Description)
            .MaximumLength(500).WithMessage("Description is too long");
    }
}