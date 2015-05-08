using Microsoft.AspNet.Authentication.Notifications;
using Microsoft.AspNet.Http;
using System;
using System.Threading.Tasks;

namespace BasicAuthentication.BasicAuthenticationMiddleware
{
    public class BasicAuthenticationNotifications
    {
        public BasicAuthenticationNotifications()
        {
            AuthenticationFailed = notification => Task.FromResult(0);
            CredentialReceived = notification => Task.FromResult(0);
        }

        public Func<AuthenticationFailedNotification<HttpContext, BasicAuthenticationOptions>, Task> AuthenticationFailed { get; set; }
        public Func<CredentialReceivedNotification<BasicAuthenticationOptions>, Task> CredentialReceived { get; set; }
    }
}