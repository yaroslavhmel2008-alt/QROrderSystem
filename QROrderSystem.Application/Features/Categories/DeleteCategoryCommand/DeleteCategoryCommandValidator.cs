using FluentValidation;
using FluentValidation.Validators;

namespace QROrderSystem.Application.Features.DeleteCategoryCommand;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("Category Id cannot be null")
            .NotEmpty()
            .WithMessage("Category Id cannot be empty");
    }
}