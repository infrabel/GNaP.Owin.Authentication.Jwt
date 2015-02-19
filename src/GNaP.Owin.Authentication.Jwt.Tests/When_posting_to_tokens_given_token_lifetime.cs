namespace GNaP.Owin.Authentication.Jwt.Tests
{
    using Microsoft.Owin.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [TestClass]
    public class When_posting_to_tokens_given_token_lifetime
    {
        private TestServer _server;
        private static readonly TimeSpan TokenLifetime = new TimeSpan(6, 0, 0);

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
                    TokenLifetime = new TimeSpan(6, 0, 0),
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
        public async Task Should_return_token_with_expiration_date_for_lifetime()
        {
            // arrange
            const string payload = "{ username: \"john\", password: \"pass\"}";

            // act
            var response = await HttpClientHelper.PostJsonAsync(_server.HttpClient, 
                                                                JwtTokenIssuerOptions.Default.IssuerPath, 
                                                                payload);

            // assert
            var responseBody = await response.Content.ReadAsStringAsync();
            dynamic deserialized = JsonConvert.DeserializeObject(responseBody);
            string tokenString = deserialized.token;
            var token = JwtTokenHelper.ReadToken(tokenString);

            // token expiration should be in six hours (allowing 10 seconds skew for unit test execution)
            Assert.IsTrue((DateTime.UtcNow + TokenLifetime) - token.ValidTo < new TimeSpan(0, 0, 10));
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
