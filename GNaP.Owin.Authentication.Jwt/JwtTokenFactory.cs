namespace GNaP.Owin.Authentication.Jwt
{
    using Microsoft.Owin.Security.DataHandler.Encoder;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens;
    using System.Security.Claims;

    internal class JwtTokenFactory
    {
        private static readonly JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();

        private readonly string _issuer;
        private readonly string _audience;
        private readonly byte[] _tokenSigningKey;

        public JwtTokenFactory(string issuer, string audience, string tokenSigningKey)
            : this(issuer,
                   audience,
                   TextEncodings.Base64.Decode(tokenSigningKey))
        {
        }

        public JwtTokenFactory(string issuer, string audience, byte[] tokenSigningKey)
        {
            _issuer = issuer;
            _audience = audience;
            _tokenSigningKey = tokenSigningKey;
        }

        public string GenerateToken(IEnumerable<Claim> claims, TimeSpan tokenLifetime)
        {
            if (claims == null)
                throw new ArgumentNullException("claims");

            // create JWT token
            var token = TokenHandler.CreateToken(
                subject: new ClaimsIdentity(claims),
                issuer: _issuer,
                audience: _audience,
                expires: DateTime.UtcNow.Add(tokenLifetime),
                signingCredentials: new SigningCredentials(
                    new InMemorySymmetricSecurityKey(_tokenSigningKey),
                    "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256",
                    "http://www.w3.org/2001/04/xmlenc#sha256")
                );

            // write JWT token to string format
            return TokenHandler.WriteToken(token);
        }
    }
}
