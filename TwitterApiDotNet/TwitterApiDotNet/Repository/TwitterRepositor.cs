using Tweetinvi.Core.Models;
using TwitterApiDotNet.Models;

namespace TwitterApiDotNet.Repository
{
    public class TwitterRepositor : ITwitterRepository, IDisposable
    {
        private readonly TwitterDbContext _twitterDbContext;

        public TwitterRepositor(TwitterDbContext twitterDbContext)
        {
            _twitterDbContext  = twitterDbContext;
        }
        public void AddTweet(Tweets tweet)
        {
            _twitterDbContext.Tweets.Add(tweet);
        }

        public IEnumerable<Tweets> GetTweets()
        {
            return _twitterDbContext.Tweets.Take(50);
        }

        public void Save()
        {
            _twitterDbContext.SaveChanges();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _twitterDbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        int ITwitterRepository.GetTweetsCount()
        {
            return _twitterDbContext.Tweets.Count();
        }
    }
}
