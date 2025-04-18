namespace SunamoYouTube;

/// <summary>
///     YouTube Data API v3 sample: create a playlist.
///     Relies on the Google APIs Client Library for .NET, v1.7.0 or higher.
///     See https://developers.google.com/api-client-library/dotnet/get_started
/// </summary>
public static class YouTubeHelper
{
    private static readonly Type type = typeof(YouTubeHelper);

    /// <summary>
    ///     Direct edit
    /// </summary>
    /// <param name="l"></param>
    public static List<string> GetYtCodesFromUri(List<string> l)
    {
        for (var i = 0; i < l.Count; i++)
        {
            var s = l[i];
            if (RegexHelper.IsUri(s)) l[i] = QSHelper.GetParameter(s, "v");
        }

        return l;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="name"></param>
    /// <param name="ytCodes"></param>
    public static async Task CreateNewPlaylist(string ytSecret, string name, List<string> ytCodes)
    {
        //CA.RemoveStringsEmpty(ytCodes);

        ytCodes = ytCodes.Where(d => !string.IsNullOrEmpty(d)).ToList();

        // Neustale mi to vytvari playlisty na puvodnim sunamocz@gmail.com, i prtesto ze json je stazeny se smutekutek

        #region MyRegion

        UserCredential credential;
        using (var stream = new FileStream(ytSecret, FileMode.Open, FileAccess.Read))
        {
            credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                // This OAuth 2.0 access scope allows for full read/write access to the
                // authenticated user's account.
                new[] { YouTubeService.Scope.Youtube },
                "user",
                CancellationToken.None,
                new FileDataStore(type.ToString())
            );
        }

        var youtubeService = new YouTubeService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = type.ToString()
        });

        #endregion

        //bacha, klidne vytvori dalsi playlist se stejnym jmenem

        // Create a new, private playlist in the authorized user's channel.
        var newPlaylist = new Playlist();
        newPlaylist.Snippet = new PlaylistSnippet();
        newPlaylist.Snippet.Title = name;
        newPlaylist.Snippet.Description = "A playlist created with the YouTube API v3";
        newPlaylist.Status = new PlaylistStatus();
        newPlaylist.Status.PrivacyStatus = "public";
        newPlaylist = await youtubeService.Playlists.Insert(newPlaylist, "snippet,status").ExecuteAsync();

        // I have to take attention whether dont contains actually otherwise I get it duplicated
        foreach (var item in ytCodes)
        {
            // Add a video to the newly created playlist.
            var newPlaylistItem = new PlaylistItem();
            newPlaylistItem.Snippet = new PlaylistItemSnippet();
            newPlaylistItem.Snippet.PlaylistId = newPlaylist.Id;
            newPlaylistItem.Snippet.ResourceId = new ResourceId();
            newPlaylistItem.Snippet.ResourceId.Kind = "youtube#video";
            newPlaylistItem.Snippet.ResourceId.VideoId = item;
            newPlaylistItem = await youtubeService.PlaylistItems.Insert(newPlaylistItem, "snippet").ExecuteAsync();

            //CL.WriteLine("Added " + item);
            //CL.WriteLine("Playlist item id {0} was added to playlist id {1}.", newPlaylistItem.Id, newPlaylist.Id);
        }
    }
}