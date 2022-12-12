using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Models.V2;

namespace JH.CodeAssignment.Api.Contracts.TwitterInfo;

public record TwitterInfoResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime RequestedDateTime,
    int Duration,
    List<TweetV2> Tweets,
    Dictionary<string, int>Top10,
    string Note);