using System.Text.Json.Serialization;

namespace bedrockRInstall.util
{
    public class JsonVersionInfo
    {
        [JsonPropertyName("version")] public required string Version;
        private string _tagName;

        [JsonPropertyName("tagName")]
        public string TagName
        {
            get => _tagName.IsWhiteSpace() || _tagName == null ? Version : _tagName;
            set => _tagName = value;
        }

        [JsonPropertyName("semVer")] public required int NumericVersion;

        [JsonPropertyName("bundled")] public required bool Bundled;

        [JsonPropertyName("desc")] public string Description = "(No description.)";
        [JsonPropertyName("fileLoc")] public required string FileLocation;
        [JsonPropertyName("jdkLoc")] public required string JdkLocation;
        [JsonPropertyName("installerFormat")] public required int InstallerFormat;

        public override bool Equals(object? obj)
        {
            if (obj is JsonVersionInfo info)
                return TagName.Equals(info?.TagName);
            return base.Equals(obj);
        }
    }
}