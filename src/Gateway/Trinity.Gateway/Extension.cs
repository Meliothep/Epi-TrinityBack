using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Hosting
{
    public static class Extensions
    {
        public static void ConfigureIdentity(this IHostApplicationBuilder builder)
        {
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
            //...
        }
    }
}