#pragma warning disable CA1054
#pragma warning disable CA2234
#pragma warning disable CA2007
namespace DWD.UI.Monetary.Tests.IntegrationTests
{
    using System.Net;
    using System.Threading.Tasks;
    using Service;
    using Xunit;

    public class MonetaryControllerTests
        : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;

        public MonetaryControllerTests(CustomWebApplicationFactory<Startup> factory) =>
            this.factory = factory;

        [Theory]
        [InlineData("/BasePeriod/GetStandardBasePeriodFromInitialClaimDate?initialClaimDate=12/08/2019")]
        public async Task EndpointsReturnSuccess(string url)
        {
            // Arrange
            var client = this.factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
