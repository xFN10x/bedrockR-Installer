using bedrockRInstall;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace bedrockRInstall
{
    public class FileUtils
    {
        private static Assembly assm = Assembly.GetExecutingAssembly();
        private static HttpClient client = new();

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
            return assm.GetManifestResourceStream("bedrockRInstall.assets." + path);
        }

        public static JsonVersionInfo[] GetVersions()
        {
            var res = client.Send(new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/repos/xFN10x/bedrockR/releases"));
            var releases = JsonSerializer.Deserialize<List<GHRelease>>(res.Content.ReadAsStream(), JsonVersionInfoContext.Default.ListGHRelease);
            List<JsonVersionInfo> vers = new();
            
            foreach (var ghRelease in releases)
            {
                vers.Add(JsonVersionInfo.FromRelease(ghRelease));
            }

            return vers.ToArray<JsonVersionInfo>();
        }

        public static JsonVersionInfo? GetBundledVersionInfo()
        {
            var stream = GetFile("bundled.json");
            if (stream != null)
                return JsonSerializer.Deserialize<JsonVersionInfo>(stream, JsonVersionInfoContext.Default.JsonVersionInfo);
            else
                return null;
        }
    }
}
