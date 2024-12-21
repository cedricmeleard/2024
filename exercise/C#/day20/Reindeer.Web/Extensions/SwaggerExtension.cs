namespace Reindeer.Web.Extensions;

public static class ReindeerSwaggerExtension {
    
    public static WebApplicationBuilder ConfigureSwagger(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("ApiKey",
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "x-API-key",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter your API key in the x-API-key header"
                });

            options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Reference = new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "ApiKey"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return webApplicationBuilder;
    }
}