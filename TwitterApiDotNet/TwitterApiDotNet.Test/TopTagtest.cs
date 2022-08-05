using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwitterApiDotNet.Controllers;
using TwitterApiDotNet.Models;
using TwitterApiDotNet.Repository;
using Xunit;

namespace TwitterApiDotNet.Test
{
    public class TopTagtest
    {
        [Fact]
        public void Test1()
        {
            ////Arrange 
            //var options = new DbContextOptionsBuilder<TwitterDbContext>()
            //.UseInMemoryDatabase(databaseName: "test")
            //.Options;
            //TweetTagRepositorycs tweetTagRepositorycs = new TweetTagRepositorycs(new TwitterDbContext(options));


            //tweetTagRepositorycs.AddTweetTag(new TweetTag { Tag = "test", TweetUniqID = 1, UserID = "1" });
            //tweetTagRepositorycs.AddTweetTag(new TweetTag { Tag = "test", TweetUniqID = 2, UserID = "2" });
            //tweetTagRepositorycs.AddTweetTag(new TweetTag { Tag = "take", TweetUniqID = 3, UserID = "3" });

            //tweetTagRepositorycs.Save();



            //  //Act
            //  TopTagController tagTweets = new TopTagController(new Microsoft.Extensions.Logging.Abstractions.NullLogger<TopTagController>(), tweetTagRepositorycs);
            //var result = (OkObjectResult)tagTweets.Get();
            //var countResult = result.Value as IEnumerable<TweetGroupBy>;

            ////Assert
            //Assert.Equal(2, countResult.Count());
        }
    }
}
