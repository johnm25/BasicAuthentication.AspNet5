using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.OptionsModel;

namespace BasicAuthentication.BasicAuthenticationMiddleware
{
    public class BasicAuthenticationMiddleware : AuthenticationMiddleware<BasicAuthenticationOptions>
    {
        public BasicAuthenticationMiddleware(
            RequestDelegate next,
            IOptions<BasicAuthenticationOptions> options,
            ConfigureOptions<BasicAuthenticationOptions> configureOptions)
            : base(next, options, configureOptions)
        { }

        protected override AuthenticationHandler<BasicAuthenticationOptions> CreateHandler()
        {
            return new BasicAuthenticationHandler();
        }
    }
}
