using FluentValidation;
using FluentValidation.Validators;

namespace QROrderSystem.Application.Features.DeleteOrderCommand;

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("Category Id cannot be null")
            .NotEmpty()
            .WithMessage("Category Id cannot be empty");
    }
}