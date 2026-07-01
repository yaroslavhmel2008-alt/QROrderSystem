using System.Data;
using FluentValidation;
using FluentValidation.Validators;

namespace QROrderSystem.Application.Features.DeleteOrderItemCommand;

public class DeleteOrderItemCommandValidator : AbstractValidator<DeleteOrderItemCommand>
{
    public DeleteOrderItemCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("{Id} is required");
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("{OrderId} is required");
    }
} 