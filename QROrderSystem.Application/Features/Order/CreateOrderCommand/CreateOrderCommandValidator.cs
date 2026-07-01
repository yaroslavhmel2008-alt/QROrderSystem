using FluentValidation;
using FluentValidation.Validators;

namespace QROrderSystem.Application.Features.CreateOrderCommand;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.LocationId)
            .NotNull()
            .WithMessage("LocationId cannot be empty")
            .NotEmpty()
            .WithMessage("LocationId cannot be empty");
    }
} 