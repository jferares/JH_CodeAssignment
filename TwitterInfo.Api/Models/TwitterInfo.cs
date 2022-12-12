using ErrorOr;
using JH.CodeAssignment.Api.ServiceErrors;
using Tweetinvi.Models.V2;

namespace JH.CodeAssignment.Api.Models;
public sealed class TwitterInfo
{
    public Guid Id { get; }
    public string Name { get; }  // validate between 5 and 30 characters (arbitrary for assignment demonstration)
    public string Description { get; }  // validate between 10 and 50 characters (arbitrary for assignment demonstration)
    public DateTime RequestedDateTime { get; }
    public int Duration { get; } = 0;
    private  List<TweetV2> _tweets = new();
    public List<TweetV2> Tweets { get { return _tweets; } }
    public Dictionary<string, int> Top10 { get; set; } = new();
    private bool _isError = false;
    public bool IsError { get {return _isError; } }

    private string _note = string.Empty;
    public string Note { get {return _note; } }

    #region Validation Config Variables

    public const int NameMinLength = 5;
    public const int NameMaxLength = 30;
    public const int DescriptionMinLength = 10;
    public const int DescriptionMaxLength = 50;
    public const int DurationMin  = 5;
    public const int DurationMax = 60;

    #endregion Validation Config Variables

    private TwitterInfo(
        Guid id,
        string name,
        string description,
        DateTime requestedDateTime,
        int duration,
        List<TweetV2> tweets,
        Dictionary<string, int> top10)
    {
        Id = id;
        Name = name;
        Description = description;
        RequestedDateTime = requestedDateTime;
        Duration = duration;
        _tweets = tweets;
        Top10 = top10;
    }

    // factory
    public static ErrorOr<TwitterInfo> Create(
        string name,
        string description,
        int duration,
        List<TweetV2> tweets,
        Dictionary<string, int> top10,
        Guid? id = null)
    {
        // validate
        List<Error> errors = new();

        if(name.Length < NameMinLength || name.Length > NameMaxLength)
            errors.Add(Errors.TwitterInfo.InvalidName);

        // try it with the is / or keywords... I still prefer the || statement style
        if(description.Length is < DescriptionMinLength or > DescriptionMaxLength)
            errors.Add(Errors.TwitterInfo.InvalidDescription);

        if(duration < DurationMin || duration > DurationMax)
            errors.Add(Errors.TwitterInfo.InvalidDuration);

        if(errors.Count > 0)
            return errors;

        var TwitterInfo = new TwitterInfo(
            id ?? Guid.NewGuid(),
            name,
            description,
            DateTime.UtcNow,
            duration,
            tweets,
            top10);

        return TwitterInfo;
    }
    public static ErrorOr<TwitterInfo> Create(
        string name,
        string description,
        int duration,
        Guid? id = null)
    {
        return Create(
            name,
            description,
            duration,
            new List<TweetV2>(),
            new Dictionary<string, int>(),
            id);
    }

    public static Dictionary<string, int> GetTop10Hashtags(List<TweetV2> tweets)
    {
        // check for empty list of tweets, skip if so
        if(tweets.Count == 0)
            return new Dictionary<string, int>();

        // find top10 hashtags
        // The list of Hashtags may be null. We'll need to check for this as we iterate.
        // let's collapse the hashtags into a tally dictionary so we can sort it
        Dictionary<string, int> hashtagTally = new();
        foreach(var tweet in tweets)
        {
            if(tweet == null || tweet.Entities == null || tweet.Entities.Hashtags == null)
                continue;

            foreach(var hashtag in tweet.Entities.Hashtags)
            {
                if(hashtagTally.ContainsKey(hashtag.Tag))
                    hashtagTally[hashtag.Tag]++;
                else
                    hashtagTally.Add(hashtag.Tag, 1);
            }
        }

        // now sort and take the top 10
        return hashtagTally
            .OrderByDescending(tally => tally.Value)
            .Take(10)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }

    public void SetTweets(List<TweetV2> tweets)
    {
        _tweets = tweets;

        Top10 = GetTop10Hashtags(_tweets);
    }
}