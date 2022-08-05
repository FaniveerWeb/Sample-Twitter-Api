using TwitterApiDotNet.Models;

namespace TwitterApiDotNet.Repository
{
    public interface ITwitterRepository:IDisposable
    {
        IEnumerable<Tweets> GetTweets();
        void AddTweet(Tweets tweet);
        void Save();
        int GetTweetsCount();
    }
}
