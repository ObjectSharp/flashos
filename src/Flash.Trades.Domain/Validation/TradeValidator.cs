using FluentValidation;
using Flash.Core.Validation;
using Flash.Trades.Domain.Models;
using Flash.Core.Models;

namespace Flash.Trades.Domain.Validation
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

    public class TradeBatchValidator : AbstractValidator<Batch<Trade>>
    {
        public TradeBatchValidator()
        {
            RuleForEach(x => x.Data).SetValidator(new TradeValidator());
        }
    }
}
