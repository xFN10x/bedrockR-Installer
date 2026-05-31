using bedrockRInstall;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace bedrockRInstall
{
    public class FileUtils
    {
        private static Assembly assm = Assembly.GetExecutingAssembly();

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
