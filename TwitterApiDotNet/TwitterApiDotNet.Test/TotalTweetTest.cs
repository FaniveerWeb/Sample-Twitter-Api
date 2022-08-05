using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwitterApiDotNet.Controllers;
using TwitterApiDotNet.Models;
using Xunit;

namespace TwitterApiDotNet.Test
{
    public class TotalTweetTest
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
                context.Tweets.Add(new Tweets { Id = "1", lang = "EN", Like_Count = 1, Retweet_Count = 2, Text = "twat" });
                context.Tweets.Add(new Tweets { Id ="2" , lang ="EN", Like_Count=1,Retweet_Count=2,Text="twat"});

                context.SaveChanges();

                //Act
                TotalTweetsController totalTweets = new TotalTweetsController(new Microsoft.Extensions.Logging.Abstractions.NullLogger<TotalTweetsController>(), context);
                var result = (OkObjectResult)totalTweets.GetTweetCount();

                //Assert
                Assert.Equal(2,result.Value);

            }


        }

    }
}