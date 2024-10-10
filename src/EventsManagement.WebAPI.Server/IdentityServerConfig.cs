using IdentityServer4.Models;

namespace EventsManagement.WebAPI.Server
{
    internal static class IdentityServerConfig
    {
        public static IEnumerable<Client> GetClients(ConfigurationManager configuration)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { configuration["Jwt:Audience"] },

                    // Настройка refresh token
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    AccessTokenLifetime = int.MaxValue,//3600, // 1 час
                    AbsoluteRefreshTokenLifetime = int.MaxValue,// 86400, // 24 часа
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes(ConfigurationManager configuration)
        {
            return new List<ApiScope>
            {
                new ApiScope(configuration["Jwt:Audience"], "My API")
            };
        }

        public static IEnumerable<ApiResource> GetApiResources(ConfigurationManager configuration)
        {
            return new List<ApiResource>
            {
                new ApiResource(configuration["Jwt:Audience"], "My API")
                {
                    Scopes = { configuration["Jwt:Audience"] }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources(ConfigurationManager configuration)
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
    }
}
