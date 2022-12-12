using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Models.V2;

namespace JH.CodeAssignment.Api.Contracts.TwitterInfo;

public record UpdateInsertTwitterInfoRequest(
    string Name,
    string Description,
    int Duration,
    List<TweetV2> Tweets,
    Dictionary<string, int> Top10);