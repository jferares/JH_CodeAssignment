using JH.CodeAssignment.Api.Models;
using Microsoft.Extensions.Configuration;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Models.V2;

namespace JH.CodeAssignment.Tests;

public class MainTests
{
    [Fact]
    public void IsNameTooShort()
    {
        var twitterInfoErrorOr = TwitterInfo.Create(
            "Test",  // too short
            "Description",
            5,
            Guid.NewGuid());

        Assert.True(twitterInfoErrorOr.IsError, "There should be an error for a too short Name value");
    }

    [Fact]
    public void IsNameTooLong()
    {
        var twitterInfoErrorOr = TwitterInfo.Create(
            "Testxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",  // too long
            "Description",
            5,
            Guid.NewGuid());

        Assert.True(twitterInfoErrorOr.IsError, "There should be an error for a too long Name value");
    }

    [Fact]
    public void IsDescriptionTooShort()
    {
        var twitterInfoErrorOr = TwitterInfo.Create(
            "Test01",
            "Descxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",    // too long
            5,
            Guid.NewGuid());

        Assert.True(twitterInfoErrorOr.IsError, "There should be an error for a too long Description value");
    }

    [Fact]
    public void TwitterInfoCreateNoErrors()
    {
        var twitterInfoErrorOr = TwitterInfo.Create(
            "Test01",
            "Description",
            5,
            Guid.NewGuid());

        Assert.False(twitterInfoErrorOr.IsError, "Making a TwitterInfo object should not contain errors");

        var result = TwitterInfo.GetTop10Hashtags(twitterInfoErrorOr.Value.Tweets);

        Assert.True(result.Count == 0, "Top10 Hashtags should be zero with an empty tweet list");
    }

    [Fact]
    public async void IsTop10HashtagsPopulatedFirst4()
    {
        var config = new ConfigurationBuilder().AddJsonFile("./appsettings.json").Build();
        string consumerKey = config.GetValue<string>("Twitter:ApiKey") ?? string.Empty;
        string consumerKeySecret = config.GetValue<string>("Twitter:ApiKeySecret") ?? string.Empty;

        var consumerOnlyCredentials = new ConsumerOnlyCredentials(consumerKey, consumerKeySecret);
        var twitterClient = new TwitterClient(consumerOnlyCredentials);
        await twitterClient.Auth.InitializeClientBearerTokenAsync();

        string? json = File.ReadAllText("./mock_tweets.json");
        List<TweetV2> tweets = twitterClient.Json.Deserialize<List<TweetV2>>(json);

        var result = TwitterInfo.GetTop10Hashtags(tweets);

        Assert.True(result.Count > 0, "Top10 Hashtags should be greater than zero");
        Assert.True(result.First().Value == 4, "Top10 Hashtags first result should be 4");
    }
}