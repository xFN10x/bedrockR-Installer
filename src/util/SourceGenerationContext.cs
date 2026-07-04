using System.Text.Json.Serialization;

namespace bedrockRInstall.util
{
    [JsonSourceGenerationOptions(IncludeFields = true)]
    [JsonSerializable(typeof(JsonVersionInfo))]
    [JsonSerializable(typeof(Uploader))]
    [JsonSerializable(typeof(GhRelease))]
    [JsonSerializable(typeof(Reactions))]
    [JsonSerializable(typeof(Author))]
    [JsonSerializable(typeof(Asset))]
    [JsonSerializable(typeof(List<GhRelease>))]
    [JsonSerializable(typeof(List<JsonVersionInfo>))]
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(int))]
    public partial class JsonVersionInfoContext : JsonSerializerContext
    {
    }
}