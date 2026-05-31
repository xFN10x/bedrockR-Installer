using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace bedrockRInstall
{
    [JsonSourceGenerationOptions(IncludeFields = true)]
    [JsonSerializable(typeof(JsonVersionInfo))]
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(int))]
    public partial class JsonVersionInfoContext : JsonSerializerContext
    {
    }
}
