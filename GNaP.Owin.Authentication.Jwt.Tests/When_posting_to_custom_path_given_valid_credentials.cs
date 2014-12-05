namespace GNaP.Owin.Authentication.Jwt.Tests
{
    using Microsoft.Owin.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [TestClass]
    public class When_posting_to_custom_path_given_valid_credentials
    {
        private TestServer _server;
        private const string CustomIssuerPath = "/custom";

        [TestInitialize]
        public void Setup()
        {
            _server = TestServer.Create(app =>
            {
                app.UseJwtTokenIssuer(new JwtTokenIssuerOptions
                {
                    Issuer = JwtTokenConstants.Issuer,
                    Audience = JwtTokenConstants.Audience,
                    TokenSigningKey = JwtTokenConstants.TokenSigningKey,
                    IssuerPath = CustomIssuerPath,
                    Authenticate = (username, password) =>
                    {
                        return new[]
                        {
                            new Claim("name", username)
                        };
                    }
                });
            });
        }

        [TestMethod]
        public async Task Should_return_success_status_code()
        {
            // arrange
            const string payload = "{ username: \"john\", password: \"pass\"}";

            // act
            var response = await HttpClientHelper.PostJsonAsync(_server.HttpClient, CustomIssuerPath, payload);
            
            // assert
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestCleanup]
        public void Teardown()
        {
            if (_server != null)
            {
                _server.Dispose();
            }
        }
    }
}
