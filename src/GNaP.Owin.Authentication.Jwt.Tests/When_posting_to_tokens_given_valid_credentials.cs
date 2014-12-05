namespace GNaP.Owin.Authentication.Jwt.Tests
{
    using Microsoft.Owin.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [TestClass]
    public class When_posting_to_tokens_given_valid_credentials
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
            var response = await HttpClientHelper.PostJsonAsync(_server.HttpClient, 
                                                                JwtTokenIssuerOptions.Default.IssuerPath, 
                                                                payload);

            // assert
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Should_return_json_content()
        {
            // arrange
            const string payload = "{ username: \"john\", password: \"pass\"}";

            // act
            var response = await HttpClientHelper.PostJsonAsync(_server.HttpClient, 
                                                                JwtTokenIssuerOptions.Default.IssuerPath, 
                                                                payload);

            // assert
            Assert.IsTrue(response.Content.Headers.Contains("Content-Type"));
            Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);
        }

        [TestMethod]
        public async Task Should_return_json_object()
        {
            // arrange
            const string payload = "{ username: \"john\", password: \"pass\"}";

            // act
            var response = await HttpClientHelper.PostJsonAsync(_server.HttpClient, 
                                                                JwtTokenIssuerOptions.Default.IssuerPath, 
                                                                payload);

            // assert
            var responseBody = await response.Content.ReadAsStringAsync();
            try
            {
                JsonConvert.DeserializeObject(responseBody);
            }
            catch (Exception)
            {
                Assert.Fail("Response body did not contain a valid JSON object");
            }
        }

        [TestMethod]
        public async Task Should_return_token()
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
            Assert.IsNotNull(deserialized.token);
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
