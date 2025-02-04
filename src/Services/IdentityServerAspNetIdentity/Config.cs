using Duende.IdentityServer.Models;
using Duende.IdentityServer;
using IdentityModel;

namespace Trinity.Services.Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource()
            {
                Name = "verification",
                UserClaims = new List<string> 
                { 
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified
                }
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        { 
            new ApiScope(name: "bff", displayName: "bff"),
            new ApiScope(name: "trinitygateway", displayName: "Trinity Gateway")
        };

    public static IEnumerable<Client> Clients =>
        new Client[] 
        {
            new Client
            {
                ClientId = "client",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AccessTokenLifetime = 10,
                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = { "bff" }
            },
                 new Client
                {
                    ClientName = "Trinity Client",
                    ClientId = "trinity",
                    ClientSecrets = { new Secret("ce766e16-df99-411d-8d31-0f5bbc6b8eba".Sha256()) },
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RedirectUris = { "https://localhost:5000/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:5000/signout-callback-oidc" },
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    AccessTokenLifetime = 60,
                    AllowedScopes = { "bff"  }
                },
        };
}
