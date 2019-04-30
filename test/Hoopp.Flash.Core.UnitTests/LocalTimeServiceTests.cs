using System;
using Hoopp.Flash.Core.Services;
using Xunit;

namespace Hoopp.Flash.Core.UnitTests
{
    // NOTE: You can use the test fixture pattern if your test has more complex
    // setup and teardown logic. Keeping it separate from the test class helps
    // for maintenance and reusability across tests.
    public class LocalTimeServiceTestFixture
    {
        public ILocalTimeService CreateService() => new LocalTimeService();
    }

    public class LocalTimeServiceTests : IClassFixture<LocalTimeServiceTestFixture>
    {
        private readonly LocalTimeServiceTestFixture _fixture;

        public LocalTimeServiceTests(LocalTimeServiceTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Can_get_current_date_time()
        {
            var service = _fixture.CreateService();
            var timestamp = service.Now();
            Assert.NotEqual(DateTimeOffset.MinValue, timestamp);
        }
    }
}
