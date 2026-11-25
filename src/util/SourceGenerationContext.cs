using System.Text.Json.Serialization;

namespace bedrockRInstall.util
{
    [JsonSourceGenerationOptions(IncludeFields = true)]
    [JsonSerializable(typeof(JsonVersionInfo))]
    [JsonSerializable(typeof(List<JsonVersionInfo>))]
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(int))]
    public partial class JsonVersionInfoContext : JsonSerializerContext
    {
    }
}