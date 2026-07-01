using FluentValidation;
using FluentValidation.Validators;

namespace QROrderSystem.Application.Features.UpdateOrderItemCommand;

public class UpdateOrderItemCommandValidator : AbstractValidator<UpdateOrderItemCommand>
{
    public UpdateOrderItemCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("Id cannot be empty")
            .NotEmpty()
            .WithMessage("Id cannot be empty");
        RuleFor(x => x.OrderId)
            .NotNull()
            .WithMessage("OrderId cannot be empty")
            .NotEmpty()
            .WithMessage("OrderId cannot be empty");
        RuleFor(x => x.Quantity)
            .GreaterThan(0);


    }
} 