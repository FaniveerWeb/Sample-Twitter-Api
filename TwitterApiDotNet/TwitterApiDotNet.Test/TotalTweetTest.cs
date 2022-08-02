using Castle.Core.Logging;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TwitterApiDotNet.Controllers;
using TwitterApiDotNet.Models;

namespace TwitterApiDotNet.Test
{
    public class TotalTweetTest
    {
        [Fact]
        public void Test1()
        {
            // arrange 
            var options = new DbContextOptionsBuilder<TwitterDbContext>()
            .UseInMemoryDatabase(databaseName: "test")
            .Options;
            using (var context = new TwitterDbContext(options))
            {
                context.Tweets.Add(new Tweets { Id = "1", lang = "EN", Like_Count = 1, Retweet_Count = 2, Text = "twat" });
                context.Tweets.Add(new Tweets { Id ="2" , lang ="EN", Like_Count=1,Retweet_Count=2,Text="twat"});

                context.SaveChanges();
                TotalTweetsController totalTweets = new TotalTweetsController(new Microsoft.Extensions.Logging.Abstractions.NullLogger<TotalTweetsController>(), context);
                var result = (OkObjectResult)totalTweets.GetTweetCount();
                Assert.Equal(2,result.Value);

            }


        }

    }
}