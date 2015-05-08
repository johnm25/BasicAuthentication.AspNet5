using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using System.Security.Claims;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.AspNet.Hosting;
using BasicAuthentication.BasicAuthenticationMiddleware;
using System;
using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Http.Authentication;

namespace BasicAuthentication
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            Configuration = new Configuration()
                                .AddJsonFile("config.json")
                                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true)
                                .AddEnvironmentVariables();          
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseBasicAuthentication(options =>
            {
                options.Realm = Configuration.Get("BasicAuthentication:Realm");
                options.AutomaticAuthentication = true;

                options.Notifications = new BasicAuthenticationNotifications
                {
                    CredentialReceived = n =>
                    {
                        if (n.Credential.Id == n.Credential.Secret)
                        {
                            var id = new ClaimsIdentity("Basic");
                            id.AddClaim(new Claim(ClaimTypes.Name, n.Credential.Id));

                            n.AuthenticationTicket = new AuthenticationTicket(
                                new ClaimsPrincipal(id),
                                new AuthenticationProperties(),
                                n.AuthenticationTicket.AuthenticationScheme);
                        }

                        return Task.FromResult(0);
                    }
                };
            });

            app.Run(async (context) =>
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    await context.Response.WriteAsync($"<h1>Hello {context.User.Identity.Name}!</h1>");
                }
                else
                {
                    if (context.Request.Path.Value.EndsWith("/protected"))
                    {
                        context.Response.StatusCode = 401;
                        return;
                    }

                    await context.Response.WriteAsync("<h1>anonymous</h1>");
                }
            });
        }
    }
}
