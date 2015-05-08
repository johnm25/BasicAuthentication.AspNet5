using Microsoft.AspNet.Authentication;

namespace BasicAuthentication.BasicAuthenticationMiddleware
{
    public class BasicAuthenticationOptions : AuthenticationOptions
    {
        public BasicAuthenticationOptions()
        {
            AuthenticationScheme = "Basic";
        }

        public BasicAuthenticationNotifications Notifications { get; set; } = new BasicAuthenticationNotifications();
        public string Realm { get; set; } = "localhost";
        //public Func<BasicAuthenticationCredential, Task<ClaimsPrincipal>> ValidationFunc { get; set; } = (credential) => Task.FromResult<ClaimsPrincipal>(null);
    }
}
