using AutoBogus;
using Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using System.Net;
using System.Text.Json;
using Xunit;

namespace IntegrationTests;

public class EndpointTests : IClassFixture<EndpointTestsFixture>
{
    private readonly EndpointTestsFixture _fixture;

    public EndpointTests(EndpointTestsFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task GetShowsAsync_WithoutLimitAndOffset_ReturnsDefaultAmountOfShows()
    {
        // Arrange
        var defaultAmountOfShows = 100;
        var endpointUrl = "/shows";
        var client = _fixture.ApplicationFactory!.CreateClient();
        var serviceProvider = _fixture.ApplicationFactory.Services.GetRequiredService<IServiceProvider>();
        using var scope = serviceProvider.CreateScope();
        var bingeContext = scope.ServiceProvider.GetRequiredService<BingeContext>();
        var shows = new AutoFaker<Persistence.Models.Show>().Ignore(show => show.Id).Generate(200);
        bingeContext.Database.Migrate();
        bingeContext.Shows.AddRange(shows);
        bingeContext.SaveChanges();

        // Act
        var response = await client.GetAsync(endpointUrl);
        var responseContent = await response.Content.ReadAsStringAsync();
        var showData = JsonSerializer.Deserialize<IEnumerable<Show>>(responseContent, new JsonSerializerOptions(JsonSerializerDefaults.Web));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseContent.Should().NotBeNull();
        showData.Should().NotBeEmpty();
        showData.Should().HaveCount(defaultAmountOfShows);
        showData.Should().AllBeAssignableTo<Show>();
    }
}
