using FluentValidation;

namespace QROrderSystem.Application.Features.CreateLocationCommand;

public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
{
    public CreateLocationCommandValidator()
    {
        RuleFor(input => input.Name)
            .NotNull().WithMessage("Name cannot be null")
            .NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(100).WithMessage("Name is too long");
        RuleFor(input => input.Type)
            .NotEmpty().WithMessage("Type cannot be empty");
    }
}