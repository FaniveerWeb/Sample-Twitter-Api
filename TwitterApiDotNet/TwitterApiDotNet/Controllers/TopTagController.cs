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
            var qResult = _twitterDb.TweetTags.GroupBy(w => w.Tag)
            .Select( w=> new {
                tagname= w.Key,
                count = w.Count()
            }).OrderByDescending( o => o.count).Take(10);
            return Ok(qResult);

        }

        
    }
}