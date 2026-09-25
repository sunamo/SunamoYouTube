namespace SunamoYouTube;

public static class YouTubeHelper
{
    private static readonly Type helperType = typeof(YouTubeHelper);

    public static List<string> GetYtCodesFromUri(List<string> list)
    {
        for (var i = 0; i < list.Count; i++)
        {
            var text = list[i];
            if (RegexHelper.IsUri(text))
            {
                var videoCode = QSHelper.GetParameter(text, "v");
                if (videoCode != null) list[i] = videoCode;
            }
        }

        return list;
    }

    public static async Task CreateNewPlaylist(string filePath, string playlistName, List<string> list)
    {
        list = list.Where(text => !string.IsNullOrEmpty(text)).ToList();

        UserCredential credential;
        using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                (await GoogleClientSecrets.FromStreamAsync(stream)).Secrets,
                new[] { YouTubeService.Scope.Youtube },
                "user",
                CancellationToken.None,
                new FileDataStore(helperType.ToString())
            );
        }

        var youtubeService = new YouTubeService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = helperType.ToString()
        });

        var newPlaylist = new Playlist
        {
            Snippet = new PlaylistSnippet
            {
                Title = playlistName,
                Description = "A playlist created with the YouTube API v3"
            },
            Status = new PlaylistStatus
            {
                PrivacyStatus = "public"
            }
        };
        newPlaylist = await youtubeService.Playlists.Insert(newPlaylist, "snippet,status").ExecuteAsync();

        foreach (var item in list)
        {
            var newPlaylistItem = new PlaylistItem
            {
                Snippet = new PlaylistItemSnippet
                {
                    PlaylistId = newPlaylist.Id,
                    ResourceId = new ResourceId
                    {
                        Kind = "youtube#video",
                        VideoId = item
                    }
                }
            };
            await youtubeService.PlaylistItems.Insert(newPlaylistItem, "snippet").ExecuteAsync();
        }
    }
}
