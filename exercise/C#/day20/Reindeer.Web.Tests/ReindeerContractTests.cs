using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using FluentAssertions;
using Newtonsoft.Json;
using Reindeer.Web.Service;

namespace Reindeer.Web.Tests;

public class ReindeerContractTests
{
    private readonly HttpClient _client;

    public ReindeerContractTests()
    {
        var webApplication = new ReindeerWebApplicationFactory();
        _client = webApplication.CreateClient();
        _client.DefaultRequestHeaders.Add("Accept", "application/json");
        _client.DefaultRequestHeaders.Add("x-API-key", ReindeerWebApplicationFactory.APIKEY);
    }

    [Fact]
    public async Task ShouldGetReindeer()
    {
        var response = await _client.GetAsync("reindeer/40F9D24D-D3E0-4596-ADC5-B4936FF84B19");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        await Verify(await response.Content.ReadAsStringAsync());
    }
        
    [Fact]
    public async Task ShouldCreateReindeer()
    {
        var request = new ReindeerToCreateRequest("Dasher", ReindeerColor.Black);
        var response = await _client.PostAsync("reindeer", JsonContent.Create(request));

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var content = await response.Content.ReadAsStringAsync();
        var json = JsonConvert.DeserializeObject<ReindeerToCreate>(content);
        
        var settings = new VerifySettings();
        settings.ScrubInlineGuids("id");
        
        await Verify(json, settings);
    }
    
    public class ReindeerFailures
    {
        private readonly HttpClient _client;

        public ReindeerFailures()
        {
            var webApplication = new ReindeerWebApplicationFactory();
            _client = webApplication.CreateClient();
        }
            
        [Fact]
        public async Task ErrorWhenCreatingReindeerWithoutDatas()
        {
            _client.DefaultRequestHeaders.Add("x-API-key", ReindeerWebApplicationFactory.APIKEY);
            var response = await _client.PostAsync("reindeer", null);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
            
        [Fact]
        public async Task NotFoundForNotExistingReindeer()
        {
            _client.DefaultRequestHeaders.Add("x-API-key", ReindeerWebApplicationFactory.APIKEY);
            var nonExistingReindeer = Guid.NewGuid().ToString();
            var response = await _client.GetAsync($"reindeer/{nonExistingReindeer}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        
        [Fact]
        public async Task ConflictWhenTryingToCreateExistingOne()
        {
            _client.DefaultRequestHeaders.Add("x-API-key", ReindeerWebApplicationFactory.APIKEY);
            var request = new ReindeerToCreateRequest("Petar", ReindeerColor.Purple);
            var response = await _client.PostAsync("reindeer", JsonContent.Create(request));

            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData("InvalidKey")]
        public async Task NotProvidedApiKeyShouldReturnUnauthorized(string apiKey)
        {
            _client.DefaultRequestHeaders.Add("x-API-key", apiKey);
            
            var request = new ReindeerToCreateRequest("Dasher", ReindeerColor.Purple);
            var response = await _client.PostAsync("reindeer", JsonContent.Create(request));

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}