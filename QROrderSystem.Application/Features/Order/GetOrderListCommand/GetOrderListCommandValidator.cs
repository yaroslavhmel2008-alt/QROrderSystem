using FluentValidation;
using FluentValidation.Validators;

namespace QROrderSystem.Application.Features.GetOrderListCommand;

public class GetOrderListCommandValidator : AbstractValidator<GetOrderListCommand>
{
    public GetOrderListCommandValidator()
    {
    }
}