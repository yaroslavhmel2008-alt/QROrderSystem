using FluentValidation;

namespace QROrderSystem.Application.Features.GetLocationListCommand;

public class GetLocationListCommandValidator : AbstractValidator<GetLocationListCommand>
{
    public GetLocationListCommandValidator()
    {
    }
}