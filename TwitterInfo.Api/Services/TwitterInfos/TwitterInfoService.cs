using JH.CodeAssignment.Api.Models;
using JH.CodeAssignment.Api.ServiceErrors;
using ErrorOr;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Models.V2;
using System.Diagnostics;

namespace JH.CodeAssignment.Api.Services.TwitterInfos;

public class TwitterInfoService : ITwitterInfoService
{
    private static readonly Dictionary<Guid, TwitterInfo> _twitterInfos = new();

    #region Helpers

    private async static Task<ErrorOr<List<TweetV2>>> GetTwitterResponseAsync(IConfiguration config, TimeSpan duration)
    {
        List<TweetV2> tweets = new();
        string capturedError = string.Empty;

        string consumerKey = config.GetValue<string>("Twitter:ApiKey") ?? string.Empty;
        string consumerKeySecret = config.GetValue<string>("Twitter:ApiKeySecret") ?? string.Empty;

        var consumerOnlyCredentials = new ConsumerOnlyCredentials(consumerKey, consumerKeySecret);
        var twitterClient = new TwitterClient(consumerOnlyCredentials);
        await twitterClient.Auth.InitializeClientBearerTokenAsync();

        var sampleStream = twitterClient.StreamsV2.CreateSampleStream();
        //bool bCapturedOne = false;
        sampleStream.TweetReceived += (sender, e) =>
        {
            //if(!bCapturedOne && e.Tweet != null && e.Tweet.Entities != null && e.Tweet.Entities.Hashtags != null && e.Tweet.Entities.Hashtags.Length > 0)
            //{
                //Console.Write(e.Json);
                //bCapturedOne = true;
            //}

            if(e.Json.Contains("\"status\": \"429\""))
                capturedError = e.Json;
            else if(e.Tweet != null)
                tweets.Add(e.Tweet);
        };

        Stopwatch stopwatch = new();
        stopwatch.Start();
        _ = sampleStream.StartAsync();

        while (stopwatch.IsRunning)
        {
            if(stopwatch.Elapsed > duration)
            {
                sampleStream.StopStream();
                stopwatch.Stop();
            }
        }

        return string.IsNullOrWhiteSpace(capturedError) ? tweets : Errors.TwitterInfo.TwitterError(capturedError);
    }

    #endregion Helpers

    public ErrorOr<Created> CreateTwitterInfo(IConfiguration config, TwitterInfo twitterInfo)
    {
        var tweetsTask = GetTwitterResponseAsync(config, TimeSpan.FromSeconds(twitterInfo.Duration));
        if(tweetsTask.Result.IsError)
            return tweetsTask.Result.Errors;

        twitterInfo.SetTweets(tweetsTask.Result.Value);

        if(twitterInfo.IsError)
            return Errors.TwitterInfo.TwitterError(twitterInfo.Note);

        _twitterInfos.Add(twitterInfo.Id, twitterInfo);

        return Result.Created;
    }

    public ErrorOr<TwitterInfo> GetTwitterInfo(Guid id)
    {
        if (_twitterInfos.TryGetValue(id, out TwitterInfo? twitterInfo))
            return twitterInfo;
        return Errors.TwitterInfo.NotFound;
    }

    public ErrorOr<UpdateInsertedTwitterInfo> UpdateInsertTwitterInfo(IConfiguration config, TwitterInfo twitterInfo)
    {
        bool bMadeNewRecord = !_twitterInfos.ContainsKey(twitterInfo.Id);
        _twitterInfos[twitterInfo.Id] = twitterInfo; // dictionary creates if the key entry is not found

        return new UpdateInsertedTwitterInfo(bMadeNewRecord);
    }
    public ErrorOr<Deleted> DeleteTwitterInfo(Guid id)
    {
        _twitterInfos.Remove(id);

        return Result.Deleted;
    }
}