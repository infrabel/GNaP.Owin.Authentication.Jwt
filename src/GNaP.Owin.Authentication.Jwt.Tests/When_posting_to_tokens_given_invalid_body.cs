namespace GNaP.Owin.Authentication.Jwt.Tests
{
    using Microsoft.Owin.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Net;
    using System.Threading.Tasks;

    [TestClass]
    public class When_posting_to_tokens_given_invalid_body
    {
        private TestServer _server;

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
                    Authenticate = (context) =>
                    {
                        return null; // simulate invalid credentials
                    }
                });
            });
        }

        [TestMethod]
        public async Task Should_return_bad_request_status_code()
        {
            // arrange
            const string payload = "{ Invalid: 'Invalid' }";

            // act
            var response = await HttpClientHelper.PostJsonAsync(_server.HttpClient, 
                                                                JwtTokenIssuerOptions.Default.IssuerPath, 
                                                                payload);

            // assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
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
