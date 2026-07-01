using FluentValidation;

namespace QROrderSystem.Application.Features.Products.GetProductListCommand;

public class GetProductListCommandValidator : AbstractValidator<GetProductListCommand>
{
    public GetProductListCommandValidator()
    {
    }
    
}