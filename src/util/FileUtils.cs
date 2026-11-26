using System.Reflection;
using System.Text.Json;

namespace bedrockRInstall.util
{
    public class FileUtils
    {
        private static readonly Assembly _assm = Assembly.GetExecutingAssembly();

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

        public static JsonVersionInfo? GetBundledVersionInfo()
        {
            var stream = GetFile("bundle/data.json");
            if (stream != null)
                return JsonSerializer.Deserialize<JsonVersionInfo>(stream,
                    JsonVersionInfoContext.Default.JsonVersionInfo);
            else
                return null;
        }
    }
}