using Microsoft.AspNetCore.Mvc;
using TwitterApiDotNet.Models;
using TwitterApiDotNet.Repository;

namespace TwitterApiDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TotalTweetsController : ControllerBase
    {
        private readonly ILogger<TotalTweetsController> _logger;
        private readonly ITwitterRepository _twitterRepository;
        private readonly TwitterDbContext _twitterDb;

        public TotalTweetsController(ILogger<TotalTweetsController> logger, ITwitterRepository twitterRepository)
        {
            _logger = logger;
            _twitterRepository = twitterRepository;
        }

        [HttpGet]
        public IActionResult GetTweetCount()
        {
            _logger.LogInformation("Total number of tweets");

            try
            {
                return Ok(_twitterRepository.GetTweetsCount());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}