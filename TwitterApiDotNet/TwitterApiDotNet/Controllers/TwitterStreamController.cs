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
        public async Task <IActionResult> Get()
        {
            try
            {
                Logger.LogInformation("Calling tweeter api for streaming");
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
                    Console.WriteLine(args.Tweet.Entities);
                };

                await sampleStreamV2.StartAsync();
                return Ok("test");

            }
            catch (Exception ex)
            {

                Logger.LogError(ex.ToString());
                return StatusCode(500)
;            }
        }

       
    }
}
