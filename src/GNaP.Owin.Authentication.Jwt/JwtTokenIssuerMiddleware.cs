namespace GNaP.Owin.Authentication.Jwt
{
    using Microsoft.Owin;
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class JwtTokenIssuerMiddleware : OwinMiddleware
    {
        private readonly JwtTokenIssuerOptions _options;

        public JwtTokenIssuerMiddleware(OwinMiddleware next)
            : this(next, JwtTokenIssuerOptions.Default)
        {
        }

        public JwtTokenIssuerMiddleware(OwinMiddleware next, JwtTokenIssuerOptions options)
            : base(next)
        {
            if (options == null)
                throw new ArgumentNullException("options");

            ValidateOptions(options);

            _options = options;
        }

        public override async Task Invoke(IOwinContext context)
        {
            if (IsTokenIssuerRequest(context.Request))
            {
                await RequestHandler.Handle(context, _options);
            }
            else
            {
                await Next.Invoke(context);
            }
        }

        private static void ValidateOptions(JwtTokenIssuerOptions options)
        {
            if (options.Authenticate == null)
                throw new ArgumentException("The Authenticate property has no value set", "options");

            if (string.IsNullOrEmpty(options.Issuer))
                throw new ArgumentException("The Issuer property has no value set", "options");

            if (string.IsNullOrEmpty(options.Audience))
                throw new ArgumentException("The Audience property has no value set", "options");

            if (string.IsNullOrEmpty(options.TokenSigningKey))
                throw new ArgumentException("The TokenSigningKey property has no value set", "options");

            if (string.IsNullOrEmpty(options.IssuerPath))
                throw new ArgumentException("The IssuerPath property has no value set", "options");
        }

        private bool IsTokenIssuerRequest(IOwinRequest request)
        {
            var issuerPathRegex = Regex.Escape(_options.IssuerPath) + "$";
            return Regex.IsMatch(request.Uri.ToString(), issuerPathRegex) && request.Method == "POST";
        }
    }
}
