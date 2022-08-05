using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Tweetinvi;
using Tweetinvi.Models;
using TwitterApiDotNet.Models;
using TwitterApiDotNet.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace TwitterApiDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwitterStreamController : ControllerBase
    {
        public ILogger<TwitterStreamController> Logger { get; }
        public ITwitterRepository TwitterRepository { get; }
        public ITweetTag TweetTagRepository { get; }
        public IConfiguration Config { get; }

        public TwitterStreamController(ILogger<TwitterStreamController> logger, ITwitterRepository twitterRepository, ITweetTag tweetTag, IConfiguration config)
        {
            Logger = logger;
            TwitterRepository = twitterRepository;
            TweetTagRepository = tweetTag;
            Config = config;
        }

        // GET: api/<TwitterStreamController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                Logger.LogInformation("Calling tweeter api for streaming");
                
                // random number generator
                Random rnd = new Random();

                // string match pattern
                const string HashTagPattern = @"#([A-Za-z0-9\-_&;]+)";

                // authenticate the user here 
                var consumerOnlyCredentials = new ConsumerOnlyCredentials(Config["ClientApiKey"], Config["ClientApiKeySecret"]);
                var appClientWithoutBearer = new TwitterClient(consumerOnlyCredentials);
                consumerOnlyCredentials.BearerToken = await appClientWithoutBearer.Auth.CreateBearerTokenAsync();

                var appClient = new TwitterClient(consumerOnlyCredentials);
                var sampleStreamV2 = appClient.StreamsV2.CreateSampleStream();
                var count = 0;

                sampleStreamV2.TweetReceived += (sender, args) =>
                {
                    // Create the tweet and save to database
                    Tweets tweets = new Tweets
                    {
                        Id = args.Tweet.Id,
                        Text = args.Tweet.Text,
                        lang = args.Tweet.Lang,
                        Like_Count = args.Tweet.PublicMetrics.LikeCount,
                        Retweet_Count = args.Tweet.PublicMetrics.RetweetCount
                    };

                    TwitterRepository.AddTweet(tweets);
                    TwitterRepository.Save();

                    // Save the user tag 
                    if (args.Tweet.Text.Contains("#"))
                    {
                        var names = new List<string>();
                        foreach (Match match in Regex.Matches(args.Tweet.Text, HashTagPattern))
                        {
                            var hashTag = match.Groups[1].Value;
                            if (!names.Contains(hashTag))
                            {
                                names.Add(hashTag);
                            }
                        }

                        foreach (var tweettag in names)
                        {
                            TweetTag tweetTag = new TweetTag
                            {
                                UserID = tweets.Id,
                                TweetUniqID = count + rnd.Next(),
                                Tag = tweettag
                            };

                            TweetTagRepository.AddTweetTag(tweetTag);
                            count = count + 1;
                            TweetTagRepository.Save();
                        }
                    }
                };

                await sampleStreamV2.StartAsync();
                return Ok("test");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}
