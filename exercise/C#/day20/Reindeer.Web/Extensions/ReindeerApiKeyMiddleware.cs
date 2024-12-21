namespace Reindeer.Web.Extensions;

public class ReindeerApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKeyHeaderName = "x-API-key";
    private readonly string? _validApiKey;
    
    public ReindeerApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        
        _validApiKey = configuration["ApiKey"];

        if (string.IsNullOrWhiteSpace(_validApiKey))
        {
            throw new InvalidOperationException("API key is missing in environment variables.");
        }
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        // Validate x-API-key header
        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("API Key was not provided.");
            return;
        }

        if (!string.Equals(extractedApiKey, _validApiKey, StringComparison.Ordinal))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized client.");
            return;
        }

        await _next(context); // Proceed to the next middleware
    }
}