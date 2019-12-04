using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace Marvin.IDP
{
    public static class Config
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "Kevin",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Kevin"),
                        new Claim("family_name", "Dockx"),
                        new Claim("role", "Administrator"),
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "Sven",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Sven"),
                        new Claim("family_name", "Vercauteren"),
                        new Claim("role", "Tour Manager"),
                    }
                }
            };
        }

        public static List<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("roles", "Your role(s)", new[] {"role"}),
            };
        }

        internal static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("tourmanagementapi", "Tour Management API", new[] {"role"})
            };
        }

        public static List<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "Tour Management",
                    ClientId = "tourmanagementclient",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequireConsent = false,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                        "http://localhost:4200/signin-oidc",
//                        "https://localhost:4200/signin-oidc"
                        "http://localhost:4200/redirect-silentrenew"
//                        "https://localhost:4200/redirect-silentrenew"
                    },
                    AccessTokenLifetime = 180,
                    PostLogoutRedirectUris = new[]
                    {
                        "http://localhost:4200/"
//                        "https://localhost:4200/"
                    },
                    AllowedScopes = new[]
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        "tourmanagementapi",
                    }
                }
            };
        }
    }
}