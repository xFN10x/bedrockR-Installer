using System.Text.Json.Serialization;

namespace bedrockRInstall.util
{
    public class JsonVersionInfo
    {
        [JsonPropertyName("version")] public required string Version;
        [JsonPropertyName("semVer")] public required int NumericVersion;

        [JsonPropertyName("bundled")] public required bool Bundled;

        [JsonPropertyName("desc")] public string Description = "(No description.)";

        public static JsonVersionInfo FromRelease(GhRelease re)
        {
            var bundled = FileUtils.GetBundledVersionInfo();
            var releaseVer = re.TagName;
            if (bundled != null && releaseVer.Equals(bundled.Version)) return bundled;
            return new JsonVersionInfo
            {
                Version = releaseVer,
                NumericVersion = FileUtils.SupportedVersions.GetValueOrDefault(releaseVer, 0),
                Bundled = false,
                Description = FileUtils.VersionDescriptions.GetValueOrDefault(releaseVer, "(No description provided.")
            };
        }
    }

    // Root myDeserializedClass = JsonSerializer.Deserialize<List<Root>>(myJsonResponse);
    public class Asset
    {
        [JsonPropertyName("url")] public string Url;

        [JsonPropertyName("id")] public int? Id;

        [JsonPropertyName("node_id")] public string NodeId;

        [JsonPropertyName("name")] public string Name;

        [JsonPropertyName("label")] public object Label;

        [JsonPropertyName("uploader")] public Uploader Uploader;

        [JsonPropertyName("content_type")] public string ContentType;

        [JsonPropertyName("state")] public string State;

        [JsonPropertyName("size")] public int? Size;

        [JsonPropertyName("digest")] public string Digest;

        [JsonPropertyName("download_count")] public int? DownloadCount;

        [JsonPropertyName("created_at")] public DateTime? CreatedAt;

        [JsonPropertyName("updated_at")] public DateTime? UpdatedAt;

        [JsonPropertyName("browser_download_url")]
        public string BrowserDownloadUrl;
    }

    public class Author
    {
        [JsonPropertyName("login")] public string Login;

        [JsonPropertyName("id")] public int? Id;

        [JsonPropertyName("node_id")] public string NodeId;

        [JsonPropertyName("avatar_url")] public string AvatarUrl;

        [JsonPropertyName("gravatar_id")] public string GravatarId;

        [JsonPropertyName("url")] public string Url;

        [JsonPropertyName("html_url")] public string HtmlUrl;

        [JsonPropertyName("followers_url")] public string FollowersUrl;

        [JsonPropertyName("following_url")] public string FollowingUrl;

        [JsonPropertyName("gists_url")] public string GistsUrl;

        [JsonPropertyName("starred_url")] public string StarredUrl;

        [JsonPropertyName("subscriptions_url")]
        public string SubscriptionsUrl;

        [JsonPropertyName("organizations_url")]
        public string OrganizationsUrl;

        [JsonPropertyName("repos_url")] public string ReposUrl;

        [JsonPropertyName("events_url")] public string EventsUrl;

        [JsonPropertyName("received_events_url")]
        public string ReceivedEventsUrl;

        [JsonPropertyName("type")] public string Type;

        [JsonPropertyName("user_view_type")] public string UserViewType;

        [JsonPropertyName("site_admin")] public bool? SiteAdmin;
    }

    public class Reactions
    {
        [JsonPropertyName("url")] public string Url;

        [JsonPropertyName("total_count")] public int? TotalCount;

        [JsonPropertyName("+1")] public int? Add1;

        [JsonPropertyName("-1")] public int? Minus1;

        [JsonPropertyName("laugh")] public int? Laugh;

        [JsonPropertyName("hooray")] public int? Hooray;

        [JsonPropertyName("confused")] public int? Confused;

        [JsonPropertyName("heart")] public int? Heart;

        [JsonPropertyName("rocket")] public int? Rocket;

        [JsonPropertyName("eyes")] public int? Eyes;
    }

    public class GhRelease
    {
        [JsonPropertyName("url")] public string Url;

        [JsonPropertyName("assets_url")] public string AssetsUrl;

        [JsonPropertyName("upload_url")] public string UploadUrl;

        [JsonPropertyName("html_url")] public string HtmlUrl;

        [JsonPropertyName("id")] public int? Id;

        [JsonPropertyName("author")] public Author Author;

        [JsonPropertyName("node_id")] public string NodeId;

        [JsonPropertyName("tag_name")] public string TagName;

        [JsonPropertyName("target_commitish")] public string TargetCommitish;

        [JsonPropertyName("name")] public string Name;

        [JsonPropertyName("draft")] public bool? Draft;

        [JsonPropertyName("immutable")] public bool? Immutable;

        [JsonPropertyName("prerelease")] public bool? Prerelease;

        [JsonPropertyName("created_at")] public DateTime? CreatedAt;

        [JsonPropertyName("updated_at")] public DateTime? UpdatedAt;

        [JsonPropertyName("published_at")] public DateTime? PublishedAt;

        [JsonPropertyName("assets")] public List<Asset> Assets;

        [JsonPropertyName("tarball_url")] public string TarballUrl;

        [JsonPropertyName("zipball_url")] public string ZipballUrl;

        [JsonPropertyName("body")] public string Body;

        [JsonPropertyName("discussion_url")] public string DiscussionUrl;

        [JsonPropertyName("mentions_count")] public int? MentionsCount;

        [JsonPropertyName("reactions")] public Reactions Reactions;
    }

    public class Uploader
    {
        [JsonPropertyName("login")] public string Login;

        [JsonPropertyName("id")] public int? Id;

        [JsonPropertyName("node_id")] public string NodeId;

        [JsonPropertyName("avatar_url")] public string AvatarUrl;

        [JsonPropertyName("gravatar_id")] public string GravatarId;

        [JsonPropertyName("url")] public string Url;

        [JsonPropertyName("html_url")] public string HtmlUrl;

        [JsonPropertyName("followers_url")] public string FollowersUrl;

        [JsonPropertyName("following_url")] public string FollowingUrl;

        [JsonPropertyName("gists_url")] public string GistsUrl;

        [JsonPropertyName("starred_url")] public string StarredUrl;

        [JsonPropertyName("subscriptions_url")]
        public string SubscriptionsUrl;

        [JsonPropertyName("organizations_url")]
        public string OrganizationsUrl;

        [JsonPropertyName("repos_url")] public string ReposUrl;

        [JsonPropertyName("events_url")] public string EventsUrl;

        [JsonPropertyName("received_events_url")]
        public string ReceivedEventsUrl;

        [JsonPropertyName("type")] public string Type;

        [JsonPropertyName("user_view_type")] public string UserViewType;

        [JsonPropertyName("site_admin")] public bool? SiteAdmin;
    }
}