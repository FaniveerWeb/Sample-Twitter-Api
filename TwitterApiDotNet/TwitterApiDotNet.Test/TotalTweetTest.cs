using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwitterApiDotNet.Controllers;
using TwitterApiDotNet.Models;
using TwitterApiDotNet.Repository;
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

            TwitterRepositor twitterRepositor = new TwitterRepositor(new TwitterDbContext(options));

            twitterRepositor.AddTweet(new Tweets { Id = "1", lang = "EN", Like_Count = 1, Retweet_Count = 2, Text = "twat" });
            twitterRepositor.AddTweet(new Tweets { Id = "2", lang = "EN", Like_Count = 1, Retweet_Count = 2, Text = "twat" });

            twitterRepositor.Save();



            //Act
            twitterRepositor.Save();

            //Assert
            Assert.Equal(2, twitterRepositor.GetTweets().Count());


        }

    }
}