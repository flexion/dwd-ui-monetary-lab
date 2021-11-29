#pragma warning disable CA1054
#pragma warning disable CA2234
#pragma warning disable CA2007
namespace DWD.UI.Monetary.Tests.IntegrationTests
{
    using System.Net;
    using System.Threading.Tasks;
    using Service;
    using Xunit;

    public class BasePeriodControllerTests
        : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;

        public BasePeriodControllerTests(CustomWebApplicationFactory<Startup> factory) =>
            this.factory = factory;

        [Theory]
        [InlineData("/BasePeriod/GetStandardBasePeriodFromInitialClaimDate?initialClaimDate=12/08/2019", HttpStatusCode.OK)]
        [InlineData("/BasePeriod/GetStandardBasePeriodFromInitialClaimDate?initialClaimDate=12/08/1800", HttpStatusCode.BadRequest)]
        [InlineData("/BasePeriod/GetAlternateBasePeriodFromInitialClaimDate?initialClaimDate=12/08/2019", HttpStatusCode.OK)]
        [InlineData("/BasePeriod/GetAlternateBasePeriodFromInitialClaimDate?initialClaimDate=12/08/1800", HttpStatusCode.BadRequest)]
        public async Task TestEndpointsResponse(string url, HttpStatusCode expectedStatusCode)
        {
            // Arrange
            var client = this.factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }
    }
}
