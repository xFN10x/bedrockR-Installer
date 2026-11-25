using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace bedrockRInstall
{
    public class JsonVersionInfo
    {
        [JsonPropertyName("version")]
        public required string Version;
        [JsonPropertyName("numVer")]
        public required int NumericVersion;
        [JsonPropertyName("winVer")]
        public required string SemVersion;
        [JsonPropertyName("jdkVer")]
        public required int JdkLevel;

    }
}
