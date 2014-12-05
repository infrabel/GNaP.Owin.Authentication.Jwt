namespace GNaP.Owin.Authentication.Jwt
{
    using Microsoft.Owin;
    using Newtonsoft.Json;
    using System.IO;
    using System.Threading.Tasks;

    internal static class ResponseWriter
    {
        internal static async Task WriteTo(IOwinResponse response, ResponsePayload payload)
        {
            var payloadAsJson = JsonConvert.SerializeObject(new { token = payload.Token }, 
                                                            JsonSerializerSettingsFactory.CreateDefault());

            response.ContentType = "application/json";

            var writer = new StreamWriter(response.Body);
            await writer.WriteAsync(payloadAsJson);
            await writer.FlushAsync();
        }
    }
}
