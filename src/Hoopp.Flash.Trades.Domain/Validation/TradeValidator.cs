using FluentValidation;
using Hoopp.Flash.Core.Validation;
using Hoopp.Flash.Trades.Domain.Models;

namespace Hoopp.Flash.Trades.Domain.Validation
{
    // TIP: Built in validators: https://fluentvalidation.net/built-in-validators
    public class TradeValidator : AbstractValidator<Trade>
    {
        public TradeValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.Notes).NotEmpty();
            RuleFor(x => x.Ticker).NotEmpty().Must(BeAValidTicker);
            RuleFor(x => x.Transactions).NotEmpty();
            RuleForEach(x => x.Transactions).Must(t => t.Amount > 0);
        }

        private bool BeAValidTicker(string ticker)
        {
            return CommonValidators.IsValidTicker(ticker);
        }
    }
}
