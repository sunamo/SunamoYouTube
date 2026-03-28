namespace SunamoYouTube;

/// <summary>
/// YouTube Data API v3 helper: create playlists and manage video codes.
/// Relies on the Google APIs Client Library for .NET, v1.7.0 or higher.
/// See https://developers.google.com/api-client-library/dotnet/get_started
/// </summary>
public static class YouTubeHelper
{
    private static readonly Type helperType = typeof(YouTubeHelper);

    /// <summary>
    /// Extracts YouTube video codes from a list of URIs. Modifies the list directly.
    /// </summary>
    /// <param name="list">List of URIs or video codes to process.</param>
    /// <returns>The same list with URIs replaced by their video codes.</returns>
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

    /// <summary>
    /// Creates a new public YouTube playlist and adds videos to it.
    /// Note: This will create a new playlist even if one with the same name already exists.
    /// </summary>
    /// <param name="filePath">Path to the YouTube API client secrets JSON file.</param>
    /// <param name="playlistName">Name of the new playlist to create.</param>
    /// <param name="list">List of YouTube video codes to add to the playlist.</param>
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

        var newPlaylist = new Playlist();
        newPlaylist.Snippet = new PlaylistSnippet();
        newPlaylist.Snippet.Title = playlistName;
        newPlaylist.Snippet.Description = "A playlist created with the YouTube API v3";
        newPlaylist.Status = new PlaylistStatus();
        newPlaylist.Status.PrivacyStatus = "public";
        newPlaylist = await youtubeService.Playlists.Insert(newPlaylist, "snippet,status").ExecuteAsync();

        foreach (var item in list)
        {
            var newPlaylistItem = new PlaylistItem();
            newPlaylistItem.Snippet = new PlaylistItemSnippet();
            newPlaylistItem.Snippet.PlaylistId = newPlaylist.Id;
            newPlaylistItem.Snippet.ResourceId = new ResourceId();
            newPlaylistItem.Snippet.ResourceId.Kind = "youtube#video";
            newPlaylistItem.Snippet.ResourceId.VideoId = item;
            newPlaylistItem = await youtubeService.PlaylistItems.Insert(newPlaylistItem, "snippet").ExecuteAsync();
        }
    }
}
