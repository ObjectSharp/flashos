using Flash.Trades.Domain.Models;
using Flash.Trades.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Flash.Trades.UnitTests.Validators
{
    public class TradeValidatorTests
    {
        private readonly TradeValidator _validator;

        public TradeValidatorTests()
        {
            _validator = new TradeValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("\t\n")]
        [InlineData("AAA")]
        public void Trade_ticker_validation_should_return_error(string ticker)
        {
            var trade = new Trade { Ticker = ticker };
            _validator.ShouldHaveValidationErrorFor(x => x.Ticker, trade);
        }

        [Theory]
        [InlineData("APPL")]
        public void Trade_ticker_validation_should_not_return_error(string ticker)
        {
            var trade = new Trade { Ticker = ticker };
            _validator.ShouldNotHaveValidationErrorFor(x => x.Ticker, trade);
        }
    }
}
