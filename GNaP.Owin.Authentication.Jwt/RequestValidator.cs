using Microsoft.Owin;

namespace GNaP.Owin.Authentication.Jwt
{
    internal static class RequestValidator
    {
        private const string ApplicationJsonMediaType = "application/json";

        internal static bool HasValidHeaders(IOwinRequest request)
        {
            if (request.Accept == null || !request.Accept.Contains(ApplicationJsonMediaType))
                return false;

            if (!request.ContentType.StartsWith(ApplicationJsonMediaType))
                return false;
            
            return true;
        }
    }
}
