using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.IdentityModel.Tokens.Jwt;
using Trinity.Gateway.DelegatingHandlers;

namespace Microsoft.Extensions.Hosting
{
    public static class Extensions
    {
        public static void ConfigureIdentity(this IHostApplicationBuilder builder)
        {
            builder.Services.AddAccessTokenManagement();

            var identityUrl = builder.Configuration.GetValue<string>("IdentityUrl");
            var authenticationProviderKey = "IdentityApiKey";
             //…
            builder.Services.AddAuthentication()
                .AddJwtBearer(authenticationProviderKey, x =>
                {
                    x.Authority = identityUrl;
                    x.RequireHttpsMetadata = false;
                    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidAudiences = new[] { "bff"}
                    };
                });
        }
    }
}