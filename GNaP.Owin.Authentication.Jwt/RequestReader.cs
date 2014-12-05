namespace GNaP.Owin.Authentication.Jwt
{
    using Microsoft.Owin;
    using Newtonsoft.Json;
    using System.IO;
    using System.Threading.Tasks;

    internal static class RequestReader
    {
        internal static async Task<RequestPayload> ReadFrom(IOwinRequest request)
        {
            // read the payload
            var payload = await new StreamReader(request.Body).ReadToEndAsync();

            try
            {
                // read the payload as JSON
                var credentials = JsonConvert.DeserializeObject<RequestPayload>(payload);

                return credentials;
            }
            catch
            {
                // not a valid credentials JSON object
                return null;
            }
        }
    }
}
