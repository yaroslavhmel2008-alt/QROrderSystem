using FluentValidation;
using FluentValidation.Validators;

namespace QROrderSystem.Application.Features.UpdateOrderStatusCommand;

public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
{
    public UpdateOrderStatusCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("Id cannot be null")
            .NotEmpty()
            .WithMessage("Id cannot be empty");
        RuleFor(x => x.OrderStatus)
            .IsInEnum()
            .WithMessage("Order status must be in enum");
    }
}