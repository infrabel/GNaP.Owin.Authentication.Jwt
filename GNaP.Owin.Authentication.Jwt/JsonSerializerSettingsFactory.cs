namespace GNaP.Owin.Authentication.Jwt
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    internal static class JsonSerializerSettingsFactory
    {
        public static JsonSerializerSettings CreateDefault()
        {
            return new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }
    }
}
