using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TwitterApiDotNet.Models;

namespace TwitterApiDotNet.Controllers
{
    [Route("[controller]")]
    public class TopTagController : ControllerBase
    {
        private readonly ILogger<TopTagController> _logger;
         private readonly TwitterDbContext _twitterDb;

        public TopTagController(ILogger<TopTagController> logger, TwitterDbContext twitterDbContext)
        {
            _logger = logger;
            _twitterDb = twitterDbContext;
        }

[HttpGet]
        public IActionResult Get()
        {
            try
            {
                var qResult = _twitterDb.TweetTags.GroupBy(w => w.Tag)
            .Select(w => new TweetGroupBy
            {
                TagName = w.Key,
                Count = w.Count()
            }).OrderByDescending(o => o.Count).Take(10);
                return Ok(qResult);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
            

        }

        
    }
}