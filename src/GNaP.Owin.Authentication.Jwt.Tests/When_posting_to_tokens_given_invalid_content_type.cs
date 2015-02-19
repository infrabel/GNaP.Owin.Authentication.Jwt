namespace GNaP.Owin.Authentication.Jwt.Tests
{
    using Microsoft.Owin.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    [TestClass]
    public class When_posting_to_tokens_given_invalid_content_type
    {
        private TestServer _server;

        [TestInitialize]
        public void Setup()
        {
            _server = TestServer.Create(app =>
            {
                app.UseJwtTokenIssuer(new JwtTokenIssuerOptions
                {
                    Issuer = "urn:test",
                    Audience = "urn:test",
                    TokenSigningKey = "U0lHTklOR19LRVlfR09FU19IRVJF",
                    Authenticate = (context) =>
                    {
                        return new[]
                        {
                            new Claim("name", context.Username)
                        };
                    }
                });
            });
        }

        [TestMethod]
        public async Task Should_return_bad_request()
        {
            // arrange
            var payload = new StringContent("{ username: \"john\", password: \"pass\"}", 
                                            Encoding.UTF8, 
                                            "application/xml");

            var client = _server.HttpClient;
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // act
            var response = await client.PostAsync("/tokens", payload);

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
