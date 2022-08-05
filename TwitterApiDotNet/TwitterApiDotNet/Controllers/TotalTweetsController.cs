using Microsoft.AspNetCore.Mvc;
using TwitterApiDotNet.Models;

namespace TwitterApiDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TotalTweetsController : ControllerBase
    {
        private readonly ILogger<TotalTweetsController> _logger;
        private readonly TwitterDbContext _twitterDb;

        public TotalTweetsController(ILogger<TotalTweetsController> logger, TwitterDbContext twitterDbContext)
        {
            _logger = logger;
            _twitterDb = twitterDbContext;
        }

        [HttpGet]
        public IActionResult GetTweetCount()
        {
            _logger.LogInformation("Total number of tweets");

            try
            {
                return Ok(_twitterDb.Tweets.Count());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}