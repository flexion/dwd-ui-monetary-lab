#pragma warning disable CA1054
#pragma warning disable CA2234
#pragma warning disable CA2007
namespace DWD.UI.Monetary.Tests.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Domain.BusinessEntities;
    using Service;
    using Xunit;

    public class BasePeriodControllerTests
        : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;

        public BasePeriodControllerTests(CustomWebApplicationFactory<Startup> factory) =>
            this.factory = factory;

        [Theory]
        [InlineData("/BasePeriod/GetStandardBasePeriodFromInitialClaimDate?initialClaimDate=12/08/2019", HttpStatusCode.OK,
            "\"year\":2018,\"quarterNumber\":3")]
        [InlineData("/BasePeriod/GetStandardBasePeriodFromInitialClaimDate?initialClaimDate=12/08/1800", HttpStatusCode.BadRequest,
            "The supplied initial claim date is not valid")]
        [InlineData("/BasePeriod/GetAlternateBasePeriodFromInitialClaimDate?initialClaimDate=12/08/2019", HttpStatusCode.OK,
            "\"year\":2019,\"quarterNumber\":3")]
        [InlineData("/BasePeriod/GetAlternateBasePeriodFromInitialClaimDate?initialClaimDate=12/08/1800", HttpStatusCode.BadRequest,
            "The supplied initial claim date is not valid")]
        public async Task TestEndpointsResponse(string url, HttpStatusCode expectedStatusCode, string expectedContentSubString)
        {
            // Arrange
            var client = this.factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.NotNull(response);
            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.Contains(expectedContentSubString, responseString, StringComparison.Ordinal);
        }
    }
}
