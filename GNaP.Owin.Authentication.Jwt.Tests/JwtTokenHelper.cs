using System.IdentityModel.Tokens;

namespace GNaP.Owin.Authentication.Jwt.Tests
{
    internal static class JwtTokenHelper
    {
        internal static SecurityToken ReadToken(string tokenString)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ReadToken(tokenString);
        }
    }
}
