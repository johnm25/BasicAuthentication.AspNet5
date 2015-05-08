using BasicAuthentication.BasicAuthenticationMiddleware;
using Microsoft.Framework.OptionsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Builder
{
    public static class BasicAuthenticationAppBuilderExtensions
    {
        public static IApplicationBuilder UseBasicAuthentication(this IApplicationBuilder app, Action<BasicAuthenticationOptions> configureOptions = null, string optionsName = "")
        {
            return app.UseMiddleware<BasicAuthenticationMiddleware>(
                new ConfigureOptions<BasicAuthenticationOptions>(configureOptions ?? (o => { }))
                {
                    Name = optionsName
                });
        }
    }
}
