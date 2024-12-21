using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace Reindeer.Web.Tests;

public class ReindeerWebApplicationFactory : WebApplicationFactory<Program>
{
    public const string APIKEY = "TestApiKey";
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            var testConfig = new Dictionary<string, string>
            {
                { "ApiKey", APIKEY }
            };

            config.AddInMemoryCollection(testConfig);
        });
    }
}