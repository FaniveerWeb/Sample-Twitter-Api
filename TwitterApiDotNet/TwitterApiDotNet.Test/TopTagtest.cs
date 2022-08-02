using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApiDotNet.Controllers;
using TwitterApiDotNet.Models;

namespace TwitterApiDotNet.Test
{
    public class TopTagtest {
        [Fact]
        public void Test1()
        {
            // arrange 
            var options = new DbContextOptionsBuilder<TwitterDbContext>()
            .UseInMemoryDatabase(databaseName: "test")
            .Options;
            using (var context = new TwitterDbContext(options))
            {
                context.TweetTags.Add(new TweetTag { Tag = "test", TweetUniqID = 1, UserID = "1" });
                context.TweetTags.Add(new TweetTag { Tag = "test", TweetUniqID = 2, UserID = "2" });
                context.TweetTags.Add(new TweetTag { Tag = "take", TweetUniqID = 3, UserID = "3" });

                context.SaveChanges();
                TopTagController tagTweets = new TopTagController(new Microsoft.Extensions.Logging.Abstractions.NullLogger<TopTagController>(), context);
                var result = (OkObjectResult)tagTweets.Get();
                var countResult = result.Value as IEnumerable<TweetGroupBy>;

                Assert.Equal(2,countResult.Count());

            }


        }
    }
}
