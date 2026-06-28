using FluentValidation;
using FluentValidation.Validators;

namespace QROrderSystem.Application.Features.UpdateOrderCommand;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("Id cannot be null")
            .NotEmpty()
            .WithMessage("Id cannot be empty");
        RuleFor(x => x.LocationId)
            .NotNull()
            .WithMessage("LocationId cannot be null")
            .NotEmpty()
            .WithMessage("LocationId cannot be empty");
    }
}