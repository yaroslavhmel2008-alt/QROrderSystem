using FluentValidation;
using FluentValidation.Validators;

namespace QROrderSystem.Application.Features.AddOrderItemToOrderCommand;

public class AddOrderItemToOrderCommandValidator : AbstractValidator<AddOrderItemToOrderCommand>
{
    public AddOrderItemToOrderCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotNull()
            .WithMessage("OrderId cannot be empty")
            .NotEmpty()
            .WithMessage("OrderId cannot be empty");
        RuleFor(x => x.ProductId)
            .NotNull()
            .WithMessage("ProductId cannot be empty")
            .NotEmpty()
            .WithMessage("ProductId cannot be empty");
        RuleFor(x => x.Quantity)
            .NotNull()
            .WithMessage("Quantity cannot be empty")
            .GreaterThan(0);


    }
} 