#pragma warning restore CA1054
#pragma warning disable CA2234
namespace DWD.UI.Monetary.Tests.IntegrationTests
{
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
        [InlineData("/GetBasePeriodFromInitialClaimDate")]
        public async Task EndpointsReturnSuccess(string url)
        {
            // Arrange
            var client = this.factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }
    }
}
