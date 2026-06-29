using FluentValidation;
using FluentValidation.Validators;

namespace QROrderSystem.Application.Features.GetCategoryListCommand;

public class GetCategoryListCommandValidator : AbstractValidator<GetCategoryListCommand>
{
    public GetCategoryListCommandValidator()
    {
    }
}