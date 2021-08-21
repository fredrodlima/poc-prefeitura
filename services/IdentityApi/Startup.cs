using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;

namespace IdentityApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
            .AddInMemoryClients(Clients.Get())
            .AddInMemoryIdentityResources(Resources.GetIdentityResources())
            .AddInMemoryApiResources(Resources.GetApiResources())
            .AddInMemoryApiScopes(Resources.GetApiScopes())
            .AddTestUsers(Users.Get())
            .AddDeveloperSigningCredential();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }

        internal class Clients
        {
            public static IEnumerable<Client> Get()
            {
                return new List<Client>{
                    new Client
                    {
                        ClientId = "oauthClient",
                        ClientName = "Example client application using client credentials",
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        ClientSecrets = new List<Secret> { new Secret("SuperSecretPassword".Sha512())},
                        AllowedScopes = new List<string> {"api1.read"}
                    },
                    new Client
                    {
                        ClientId = "oidcClient",
                        ClientName = "Example Client Application",
                        ClientSecrets =new List<Secret> { new Secret("SuperSecretPassword".Sha512())},
                        AllowedGrantTypes = GrantTypes.Code,
                        RedirectUris =  new List<string> {"https://localhost:5003/signin-oidc"},
                        AllowedScopes = new List<string>{
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.StandardScopes.Email,
                            "role",
                            "api1.read"
                        },
                        RequirePkce = true,
                        AllowPlainTextPkce = false

                    }
                };
            }
        }

        internal class Resources{
            public static IEnumerable<IdentityResource> GetIdentityResources()
            {
                return new [] {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile(),
                    new IdentityResources.Email(),
                    new IdentityResource 
                    {
                        Name = "role",
                        UserClaims = new List<string> {"role"}
                    }
                };
            }

            public static IEnumerable<ApiResource> GetApiResources()
            {
                return new[]
                {
                    new ApiResource
                    {
                        Name = "api1",
                        DisplayName = "API #1",
                        Description = "Allow the application to access API #1 on your behalf",
                        Scopes = new List<string> {"api1.read", "api1.write"},
                        ApiSecrets = new List<Secret>{ new Secret("ScopeSecret".Sha512())},
                        UserClaims = new List<string> {"role"}
                    }
                };
            }

            public static IEnumerable<ApiScope> GetApiScopes()
            {
                return new[]
                {
                    new ApiScope("api1.read", "Read Access to API #1"),
                    new ApiScope("api1.write", "Write Access to API #1")
                };
            }
        }

        internal class Users 
        {
            public static List<TestUser> Get() {
                return new List<TestUser> 
                {
                    new TestUser {
                        SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                        Username = "fred",
                        Password = "apassword",
                        Claims = new List<Claim> {
                            new Claim(JwtClaimTypes.Email, "fredrodlima@gmail.com"),
                            new Claim(JwtClaimTypes.Role, "admin")
                        }
                    }
                };
            }
        }
    }
}
