using FluentValidation;
using FluentValidation.Validators;

namespace QROrderSystem.Application.Features.GetCategoryByIdCommand;

public class GetCategoryListCommandValidator : AbstractValidator<GetCategoryByIdCommand>
{
    public GetCategoryListCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("Id cannot be null")
            .NotEmpty()
            .WithMessage("Id cannot be empty");
    }
}