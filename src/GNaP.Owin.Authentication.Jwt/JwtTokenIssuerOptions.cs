namespace GNaP.Owin.Authentication.Jwt
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;

    public class JwtTokenIssuerOptions
    {
        private const string DefaultIssuerPath = "/tokens";

        private static readonly TimeSpan DefaultTokenLifetime = new TimeSpan(1, 0, 0);

        public JwtTokenIssuerOptions()
        {
            IssuerPath = DefaultIssuerPath;
            TokenLifetime = DefaultTokenLifetime;
        }

        public static JwtTokenIssuerOptions Default
        {
            get { return new JwtTokenIssuerOptions(); }
        }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string TokenSigningKey { get; set; }

        public string IssuerPath { get; set; }

        public TimeSpan TokenLifetime { get; set; }

        public Func<string, string, IEnumerable<Claim>> Authenticate { get; set; }
    }
}
