using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicAuthentication.BasicAuthenticationMiddleware
{
    public class MalformedCredentialException : Exception
    {
        private readonly BasicAuthenticationCredential _credential;

        public MalformedCredentialException() { }
        public MalformedCredentialException(BasicAuthenticationCredential credential) : base("Malformed credential") { _credential = credential; }
        public MalformedCredentialException(BasicAuthenticationCredential credential, Exception inner) : base("Malformed credential", inner) { _credential = credential; }
    }
}
