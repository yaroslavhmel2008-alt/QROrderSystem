using FluentValidation;

namespace QROrderSystem.Application.Features.UpdateLocationCommand;

public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
{
    public UpdateLocationCommandValidator()
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
        RuleFor(input => input.Type)
            .NotEmpty().WithMessage("Type cannot be empty");
    }
}