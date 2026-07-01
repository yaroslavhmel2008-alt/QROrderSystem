using FluentValidation;
using FluentValidation.Validators;

namespace QROrderSystem.Application.Features.GetOrderByIdCommand;

public class GetOrderListCommandValidator : AbstractValidator<GetOrderByIdCommand>
{
    public GetOrderListCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("Id cannot be null")
            .NotEmpty()
            .WithMessage("Id cannot be empty");
    }
}