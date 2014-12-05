namespace GNaP.Owin.Authentication.Jwt
{
    using global::Owin;
    using System;

    public static class AppBuilderExtensions
    {
        public static void UseJwtTokenIssuer(this IAppBuilder appBuilder, JwtTokenIssuerOptions options)
        {
            if (appBuilder == null)
                throw new ArgumentNullException("appBuilder");

            if (options == null)
                throw new ArgumentNullException("options");

            appBuilder.Use<JwtTokenIssuerMiddleware>(options);
        }
    }
}
