// ReSharper disable All
#pragma warning disable CA1054
#pragma warning disable CA2007
#pragma warning disable CA2234
#pragma warning disable CA2000
namespace DWD.UI.Monetary.Tests.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Service;
    using Xunit;
    using static Xunit.Assert;

    public class WageEntryControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;

        public WageEntryControllerTests(CustomWebApplicationFactory<Startup> factory) =>
            this.factory = factory;

        [Theory]
        [InlineData("WageEntry/GetClaimantWage/1", HttpStatusCode.OK)]
        [InlineData("WageEntry/GetAllClaimantWagesForClaimant/12345", HttpStatusCode.OK)]
        [InlineData("WageEntry/GetAllClaimantWages", HttpStatusCode.OK)]
        public async Task GetClaimantWages(string url, HttpStatusCode expectedStatusCode)
        {
            // Arrange
            var client = this.factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            NotNull(response);
            Equal(expectedStatusCode, response.StatusCode);
            Contains("\"claimantId\":\"12345\"", responseString, StringComparison.Ordinal);
        }

        [Fact]
        public async Task CreateClaimantWage()
        {
            // Arrange
            var client = this.factory.CreateClient();
            const string claimantIdValue = "12347";
            var postRequest = new HttpRequestMessage(HttpMethod.Post, $"WageEntry/CreateClaimantWage?claimantId={claimantIdValue}&year=2018&quarter=2&wages=500");

            var formModel = new Dictionary<string, string>();

            postRequest.Content = new FormUrlEncodedContent(formModel);

            // Act
            var response = await client.SendAsync(postRequest);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            NotNull(response);
            Equal(HttpStatusCode.OK, response.StatusCode);
            Contains($"\"claimantId\":\"{claimantIdValue}\"", responseString, StringComparison.Ordinal);
        }

        [Fact]
        public async Task UpdateClaimantWage()
        {
            // Arrange
            var client = this.factory.CreateClient();
            const string updatedWages = "350";
            var postRequest = new HttpRequestMessage(HttpMethod.Put, $"WageEntry/UpdateClaimantWage/1?year=2019&quarter=2&wages={updatedWages}");

            var formModel = new Dictionary<string, string>();

            postRequest.Content = new FormUrlEncodedContent(formModel);

            // Act
            var response = await client.SendAsync(postRequest);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            NotNull(response);
            Equal(HttpStatusCode.OK, response.StatusCode);
            Contains($"\"totalWages\":{updatedWages}", responseString, StringComparison.Ordinal);
        }

        [Fact]
        public async Task DeleteClaimantWage()
        {
            // Arrange
            var client = this.factory.CreateClient();

            // Act
            const string idValue = "1";
            var response = await client.DeleteAsync($"WageEntry/DeleteClaimantWage/{idValue}");
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            NotNull(response);
            Equal(HttpStatusCode.OK, response.StatusCode);
            DoesNotContain($"\"id\":{idValue}", responseString, StringComparison.Ordinal);
        }

    }
}
