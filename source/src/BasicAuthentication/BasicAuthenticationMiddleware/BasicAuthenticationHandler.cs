using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Authentication.Notifications;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicAuthentication.BasicAuthenticationMiddleware
{
    public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        protected override AuthenticationTicket AuthenticateCore()
        {
            return AuthenticateCoreAsync().GetAwaiter().GetResult();
        }

        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            string[] authHeader;

            if (Request.Headers.TryGetValue("Authorization", out authHeader) &&
                authHeader.Any() &&
                authHeader.First().StartsWith("Basic "))
            {
                try
                {
                    var credential = BasicAuthenticationCredential.Extract(authHeader.FirstOrDefault());
                    
                    var credentialReceicedNotification = new CredentialReceivedNotification<BasicAuthenticationOptions>(Context, Options)
                    {
                        Credential = credential,
                        AuthenticationTicket = new AuthenticationTicket(new AuthenticationProperties(), Options.AuthenticationScheme)
                    };

                    await Options.Notifications.CredentialReceived(credentialReceicedNotification);
                    return credentialReceicedNotification.AuthenticationTicket;
                }
                catch (Exception ex)
                {
                    var authenticationFailedNotification =
                        new AuthenticationFailedNotification<HttpContext, BasicAuthenticationOptions>(Context, Options)
                        {
                            ProtocolMessage = Context,
                            Exception = ex
                        };

                    await Options.Notifications.AuthenticationFailed(authenticationFailedNotification);
                    if (authenticationFailedNotification.HandledResponse)
                    {
                        return authenticationFailedNotification.AuthenticationTicket;
                    }

                    if (authenticationFailedNotification.Skipped)
                    {
                        return null;
                    }

                    throw;
                }
            }
            else
            {
                return null;
            }   
        }

        protected override void ApplyResponseChallenge()
        {
            if (ShouldConvertChallengeToForbidden())
            {
                Response.StatusCode = 403;
                return;
            }

            if ((Response.StatusCode != 401) || (ChallengeContext == null && !Options.AutomaticAuthentication))
            {
                return;
            }

            Response.Headers.AppendValues("WWW-Authenticate", new[] { $"Basic realm=\"{Options.Realm}\"" });
        }

        protected override void ApplyResponseGrant()
        {
            // N/A
        }
    }
}
