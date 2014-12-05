namespace GNaP.Owin.Authentication.Jwt
{
    using Microsoft.Owin;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    internal static class RequestHandler
    {
        internal static async Task Handle(IOwinContext context, JwtTokenIssuerOptions options)
        {
            if (!RequestValidator.HasValidHeaders(context.Request))
            {
                context.Response.StatusCode = 400; // bad request
                return;
            }

            // get request payload (a username/password object)
            var requestPayload = await RequestReader.ReadFrom(context.Request);
            if (requestPayload == null)
            {
                context.Response.StatusCode = 400; // bad request
                return;
            }

            // authenticate
            var claims = Authenticate(options, requestPayload).ToList();
            if (!claims.Any())
            {
                // no claims means not authenticated, return 400
                context.Response.StatusCode = 400; //todo: which status code to return?
                return;
            }

            // create JWT token
            var token = CreateToken(claims, options);

            // create response
            var responsePayload = new ResponsePayload
            {
                Token = token
            };
            await ResponseWriter.WriteTo(context.Response, responsePayload);
        }

        private static IEnumerable<Claim> Authenticate(JwtTokenIssuerOptions options, RequestPayload requestPayload)
        {
            var claims = options.Authenticate(requestPayload.Username, requestPayload.Password);

            // if no claims were created in the authenticate phase then replace with empty claims array
            claims = claims ?? new Claim[0];

            return claims;
        }

        private static string CreateToken(IEnumerable<Claim> claims, JwtTokenIssuerOptions options)
        {
            var jwtTokenFactory = new JwtTokenFactory(
                options.Issuer,
                options.Audience,
                options.TokenSigningKey);

            return jwtTokenFactory.GenerateToken(claims, options.TokenLifetime);
        }
    }
}
