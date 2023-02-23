using System.ComponentModel;
using Dietonator.Application.Common.Interfaces;
using Dietonator.Infrastructure.Persistence;
using Dietonator.WebUI.Filters;
using Dietonator.WebUI.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services
            .AddControllers(options => {
                options.UseDateOnlyTimeOnlyStringConverters();
                options.Filters.Add<ApiExceptionFilterAttribute>();
            })
            .AddJsonOptions(options =>
            {
                options.UseDateOnlyTimeOnlyStringConverters();
            })
            .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

        // services.Configure<JsonOptions>(options => {
        //  Add(new DateOnlyJsonConverter());
        // });

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options => {
                options.Authority = "https://localhost:7001";
                // Our API app will call to the IdentityServer to get the authority

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false, // Validate 
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "DietonatorAPI");
            });
        });

        return services;
    }
}
