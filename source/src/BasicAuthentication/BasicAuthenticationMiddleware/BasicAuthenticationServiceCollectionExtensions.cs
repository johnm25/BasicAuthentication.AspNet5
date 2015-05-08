using BasicAuthentication.BasicAuthenticationMiddleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Framework.DependencyInjection
{
    public static class BasicAuthenticationServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureBasicAuthentication(this IServiceCollection services, Action<BasicAuthenticationOptions> configure)
        {
            return services.ConfigureBasicAuthentication(configure, optionsName: "");
        }

        public static IServiceCollection ConfigureBasicAuthentication(this IServiceCollection services, Action<BasicAuthenticationOptions> configure, string optionsName)
        {
            return services.Configure(configure, optionsName);
        }
    }
}
