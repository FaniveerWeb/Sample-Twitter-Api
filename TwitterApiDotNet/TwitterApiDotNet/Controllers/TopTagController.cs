using Microsoft.AspNetCore.Mvc;
using TwitterApiDotNet.Models;
using TwitterApiDotNet.Repository;

namespace TwitterApiDotNet.Controllers
{
    [Route("[controller]")]
    public class TopTagController : ControllerBase
    {
        private readonly ILogger<TopTagController> _logger;
        private readonly ITweetTag _tweetTag;

        public TopTagController(ILogger<TopTagController> logger, ITweetTag tweetTag)
        {
            _logger = logger;
            _tweetTag = tweetTag;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
               
                return Ok(_tweetTag.TopTweetTagGroup());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}