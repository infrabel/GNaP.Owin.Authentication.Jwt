namespace GNaP.Owin.Authentication.Jwt.Tests
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    internal static class HttpClientHelper
    {
        public static async Task<HttpResponseMessage> PostJsonAsync(HttpClient client, string url, string content)
        {
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            return await client.PostAsync(url, stringContent);
        }
    }
}
