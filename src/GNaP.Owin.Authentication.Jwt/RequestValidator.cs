using Microsoft.Owin;

namespace GNaP.Owin.Authentication.Jwt
{
    internal static class RequestValidator
    {
        private const string ApplicationJsonMediaType = "application/json";

        internal static bool HasValidHeaders(IOwinRequest request)
        {
            if (request.Accept == null || !request.Accept.ToLowerInvariant().Contains(ApplicationJsonMediaType))
                return false;

            if (!request.ContentType.ToLowerInvariant().StartsWith(ApplicationJsonMediaType))
                return false;

            return true;
        }
    }
}
