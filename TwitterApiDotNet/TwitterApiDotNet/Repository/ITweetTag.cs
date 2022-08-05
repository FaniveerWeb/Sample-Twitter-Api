using TwitterApiDotNet.Models;

namespace TwitterApiDotNet.Repository
{
    public interface ITweetTag
    {
        IEnumerable<TweetTag> GetTweetsTag();
        void AddTweetTag(TweetTag tweet);

        IEnumerable<TweetGroupBy> TopTweetTagGroup();
        void Save();
    }
}
