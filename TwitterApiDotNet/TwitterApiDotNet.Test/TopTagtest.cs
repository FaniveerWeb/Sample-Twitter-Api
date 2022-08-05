using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwitterApiDotNet.Controllers;
using TwitterApiDotNet.Models;
using Xunit;

namespace TwitterApiDotNet.Test
{
    public class TopTagtest
    {
        [Fact]
        public void Test1()
        {
            //Arrange 
            var options = new DbContextOptionsBuilder<TwitterDbContext>()
            .UseInMemoryDatabase(databaseName: "test")
            .Options;


            using (var context = new TwitterDbContext(options))
            {
                context.TweetTags.Add(new TweetTag { Tag = "test", TweetUniqID = 1, UserID = "1" });
                context.TweetTags.Add(new TweetTag { Tag = "test", TweetUniqID = 2, UserID = "2" });
                context.TweetTags.Add(new TweetTag { Tag = "take", TweetUniqID = 3, UserID = "3" });

                context.SaveChanges();

                //Act
                TopTagController tagTweets = new TopTagController(new Microsoft.Extensions.Logging.Abstractions.NullLogger<TopTagController>(), context);
                var result = (OkObjectResult)tagTweets.Get();
                var countResult = result.Value as IEnumerable<TweetGroupBy>;

                //Assert
                Assert.Equal(2, countResult.Count());

            }
        }
    }
}
