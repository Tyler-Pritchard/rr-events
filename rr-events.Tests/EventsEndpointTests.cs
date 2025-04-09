using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using rr_events.DTOs;

namespace rr_events.Tests;

public class EventsEndpointTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public EventsEndpointTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateEvent_ReturnsCreated()
    {
        // Arrange
        var newEvent = new CreateEventRequest
        {
            Title = "Test Event",
            Description = "Integration test",
            StartTimeUtc = DateTime.UtcNow.AddDays(1),
            EndTimeUtc = DateTime.UtcNow.AddDays(1).AddHours(2),
            Location = "Test Location",
            IsPrivate = false,
            TicketLink = "https://example.com/tickets"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/events", newEvent);

        // Assert
        response.EnsureSuccessStatusCode();
        var created = await response.Content.ReadFromJsonAsync<EventResponse>();
        Assert.Equal("Test Event", created?.Title);
        Assert.Equal("Test Location", created?.Location);
    }
}
