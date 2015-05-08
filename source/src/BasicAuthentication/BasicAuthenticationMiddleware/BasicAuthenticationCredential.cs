using System;
using System.Text;

namespace BasicAuthentication.BasicAuthenticationMiddleware
{
    public class BasicAuthenticationCredential
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public string Header { get; set; }

        public static BasicAuthenticationCredential Extract(string header)
        {
            if (string.IsNullOrEmpty(header)) throw new ArgumentNullException(nameof(header));

            var credential = new BasicAuthenticationCredential
            {
                Header = header
            };

            string pair;
            try
            {
                pair = Encoding.UTF8.GetString(
                    Convert.FromBase64String(header.Substring("Basic ".Length)));
            }
            catch (FormatException ex)
            {
                throw new MalformedCredentialException(credential, ex);
            }
            catch (ArgumentException ex)
            {
                throw new MalformedCredentialException(credential, ex);
            }

            var ix = pair.IndexOf(':');
            if (ix == -1)
            {
                throw new MalformedCredentialException(credential);
            }

            credential.Id = pair.Substring(0, ix);
            credential.Secret = pair.Substring(ix + 1);

            return credential;
        }
    }
}