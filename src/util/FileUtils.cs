using System.Reflection;
using System.Text.Json;

namespace bedrockRInstall.util
{
    public class FileUtils
    {
        private static readonly Assembly _assm = Assembly.GetExecutingAssembly();

        private static readonly HttpClient _client = new(new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(5)
        });

        public static readonly int installerVer = 0;


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
            var reqUrl = "https://api.xplate.dev/static/bedrockr/vers.json";
            var body = new HttpRequestMessage(HttpMethod.Get, reqUrl);
            body.Headers.Add("User-Agent", "bedrockR Installer");
            Console.WriteLine("Sending to {0}: {1}.", reqUrl, body);

            var res = _client.Send(body);
            Console.WriteLine("Got response from website, code {0}, {1}", res.StatusCode,
                res.Content.ReadAsStringAsync().Result);
            if (!res.IsSuccessStatusCode) return null;
            var releases = JsonSerializer.Deserialize<List<JsonVersionInfo>>(res.Content.ReadAsStream(),
                JsonVersionInfoContext.Default.ListJsonVersionInfo);

            if (releases == null) return _cachedVersions;
            var bundled = GetBundledVersionInfo();
            if (bundled != null)
                releases.Remove(bundled);
            _cachedVersions = releases.ToArray<JsonVersionInfo>();
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