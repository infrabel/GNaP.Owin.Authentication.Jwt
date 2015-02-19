namespace GNaP.Owin.Authentication.Jwt
{
    using Microsoft.Owin;

    public class AuthenticationContext
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public IOwinContext Context { get; set; }
    }
}
