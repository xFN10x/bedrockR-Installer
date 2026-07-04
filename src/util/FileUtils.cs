using System.Reflection;
using System.Text.Json;

namespace bedrockRInstall.util
{
    public class FileUtils
    {
        private static Assembly _assm = Assembly.GetExecutingAssembly();

        private static HttpClient _client = new(new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(5)
        });

        public readonly static Dictionary<string, int> SupportedVersions = new()
        {
            //these numeric versions are not checked, they are based on the current version, a2.1: 9
            ["a2.1"] = 9,
            ["a2.0.3"] = 8,
            ["a2.0.2"] = 7,
            ["a2.01"] = 6,
            ["a2.0"] = 5,
            ["a1.5"] = 4,
            ["a1.4"] = 3,
            ["a1.3.1"] = 2,
            ["a1.3"] = 1,
            ["a1.2.1"] = 0,
            ["a1.2"] = -1,
            ["a1.1"] = -2,
        };

        public readonly static Dictionary<string, string> VersionDescriptions = new()
        {
            ["a2.1"] = "Alpha release, adds settings menu, and themes.",
            ["a2.0.3"] = "Hotfix, Fixes the block downloading issue from a2.0.2",
        };


        /// <summary>
        /// Get a stream of an embedded asset.
        /// </summary>
        /// <param name="path">Should follow the format: 'folder.folder.file.ext', which would be in 'folder/folder/file.ext</param>
        /// <returns>The resource stream</returns>
        public static Stream? GetFile(string path)
        {
            /*foreach (var item in assm.GetManifestResourceNames())
            {
                Console.WriteLine(item);
            }*/
            return _assm.GetManifestResourceStream("bedrockRInstall.assets." + path);
        }

        public static JsonVersionInfo[]? GetVersions()
        {
            if (_cachedVersions != null) return _cachedVersions;
            var reqUrl = "https://api.github.com/repos/xFN10x/bedrockR/releases";
            var body = new HttpRequestMessage(HttpMethod.Get, reqUrl);
            body.Headers.Add("User-Agent", "bedrockR Installer");
            Console.WriteLine("Sending to {0}: {1}.", reqUrl, body);

            var res = _client.Send(body);
            Console.WriteLine("Got response from github, code {0}, {1}", res.StatusCode,
                res.Content.ReadAsStringAsync().Result);
            if (!res.IsSuccessStatusCode) return null;
            var releases = JsonSerializer.Deserialize<List<GhRelease>>(res.Content.ReadAsStream(),
                JsonVersionInfoContext.Default.ListGHRelease);
            List<JsonVersionInfo> vers = new();

            foreach (var ghRelease in releases)
            {
                vers.Add(JsonVersionInfo.FromRelease(ghRelease));
            }

            _cachedVersions = vers.ToArray<JsonVersionInfo>();
            return _cachedVersions;
        }

        private static JsonVersionInfo[]? _cachedVersions;

        public static JsonVersionInfo? GetBundledVersionInfo()
        {
            var stream = GetFile("bundled.json");
            if (stream != null)
                return JsonSerializer.Deserialize<JsonVersionInfo>(stream,
                    JsonVersionInfoContext.Default.JsonVersionInfo);
            else
                return null;
        }
    }
}