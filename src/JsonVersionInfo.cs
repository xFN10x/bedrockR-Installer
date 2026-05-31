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
        [JsonPropertyName("semVer")]
        public required int NumericVersion;

        [JsonPropertyName("bundled")]
        public bool Bundled;
    }
}
