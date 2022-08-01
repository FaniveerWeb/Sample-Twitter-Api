using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Tweetinvi;
using Tweetinvi.Models;
using TwitterApiDotNet.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TwitterApiDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwitterStreamController : ControllerBase
    {
        public ILogger<TwitterStreamController> Logger { get; }
        public TwitterDbContext TwitterDbContext { get; }
        public IConfiguration Config { get; }

        public TwitterStreamController(ILogger<TwitterStreamController> logger, TwitterDbContext twitterDbContext, IConfiguration config)
        {
            Logger = logger;
            TwitterDbContext = twitterDbContext;
            Config = config;
        }


        // GET: api/<TwitterStreamController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                Logger.LogInformation("Calling tweeter api for streaming");
                // string match pattern
                const string HashTagPattern = @"#([A-Za-z0-9\-_&;]+)";
                // authenticate the user here 
                var consumerOnlyCredentials = new ConsumerOnlyCredentials(Config["ClientApiKey"], Config["ClientApiKeySecret"]);
                var appClientWithoutBearer = new TwitterClient(consumerOnlyCredentials);

                var bearerToken = await appClientWithoutBearer.Auth.CreateBearerTokenAsync();
                var appCredentials = new ConsumerOnlyCredentials(Config["ClientApiKey"], Config["ClientApiKeySecret"])
                {
                    BearerToken = bearerToken
                };

                var appClient = new TwitterClient(appCredentials);
                var sampleStreamV2 = appClient.StreamsV2.CreateSampleStream();
                sampleStreamV2.TweetReceived += (sender, args) =>
                {
                    // create the tweet and save to database
                    //Console.WriteLine(args.Tweet.Text);
                    //Console.WriteLine(args.Tweet.ToString());
                    Tweets tweets = new Tweets
                    {
                        Id = args.Tweet.Id,
                        Text = args.Tweet.Text,
                        Like_Count = args.Tweet.PublicMetrics.LikeCount,
                        Retweet_Count = args.Tweet.PublicMetrics.RetweetCount
                    };
                    TwitterDbContext.Tweets.Add(tweets);
                    TwitterDbContext.SaveChanges();


                    //  save the user tag 
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
                                ID = tweets.Id,
                                Tag = tweettag
                            };
                            TwitterDbContext.TweetTags.Add(tweetTag);
                            TwitterDbContext.SaveChanges();
                        }
                    }

                    Console.WriteLine(args.Json);

                };

                await sampleStreamV2.StartAsync();
                return Ok("test");

            }
            catch (Exception ex)
            {

                Logger.LogError(ex.ToString());
                return StatusCode(500)
;
            }
        }


    }
}
