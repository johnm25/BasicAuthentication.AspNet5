using Microsoft.AspNet.Authentication.Notifications;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicAuthentication.BasicAuthenticationMiddleware
{
    public class CredentialReceivedNotification<TOptions> : BaseNotification<TOptions>
    {
        public CredentialReceivedNotification(HttpContext context, TOptions options) : base(context, options)
        {
        }

        public BasicAuthenticationCredential Credential { get; set; }
    }
}
