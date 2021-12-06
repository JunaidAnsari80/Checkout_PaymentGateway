using FluentValidation;
using PaymentGateway.API.Models;

namespace PaymentGateway.API.Validations
{
    /// <summary>
    /// Validate transaction request
    /// </summary>
    public class CreateTransactionRequestValidation : AbstractValidator<CreateTransactionRequest>
    {
        public CreateTransactionRequestValidation()
        {
            When(x => x != null, () => {
                RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);
                RuleFor(x => x.IdempotentID).NotEmpty();
                RuleFor(x => x.MerchantID).NotEmpty().GreaterThan(0);
                RuleFor(x => x.CardNumber).NotEmpty().Length(16);
                RuleFor(x => x.CardCurrency).NotEmpty().Length(3);
                RuleFor(x => x.Cvv).NotEmpty().Length(3);
                RuleFor(x => x.FirstLineOfAddress).NotEmpty();
                RuleFor(x => x.CardHolderName).NotEmpty();
                RuleFor(x => x.PostCode).NotEmpty();
                RuleFor(x => x.ExpiryMonth).NotEmpty().GreaterThan(0).LessThanOrEqualTo(12);
                RuleFor(x => x.ExpiryYear).NotEmpty().GreaterThan(2018).LessThanOrEqualTo(2030);
            });
        }
    }
}
