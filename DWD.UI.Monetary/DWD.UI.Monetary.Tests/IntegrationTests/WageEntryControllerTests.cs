#pragma warning disable CA1054
#pragma warning disable CA2007
#pragma warning disable CA2234
namespace DWD.UI.Monetary.Tests.IntegrationTests
{
    using System.Net;
    using System.Threading.Tasks;
    using Service;
    using Xunit;

    public class WageEntryControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;

        public WageEntryControllerTests(CustomWebApplicationFactory<Startup> factory) =>
            this.factory = factory;

        [Fact]
        public async Task EndpointsReturnSuccess()
        {
            // Arrange
            var client = this.factory.CreateClient();

            // Act
            var response = await client.GetAsync("WageEntry/GetAllClaimantWages");

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
