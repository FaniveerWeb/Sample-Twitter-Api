using TwitterApiDotNet.Models;

namespace TwitterApiDotNet.Repository
{
    public class TweetTagRepositorycs : ITweetTag, IDisposable
    {
        private readonly TwitterDbContext _twitterDbContext;
        private bool disposed = false;

        public TweetTagRepositorycs(TwitterDbContext twitterDbContext)
        {
            _twitterDbContext = twitterDbContext;
        }

        IEnumerable<TweetTag> ITweetTag.GetTweetsTag()
        {
            return _twitterDbContext.TweetTags;
        }

        public void AddTweetTag(TweetTag tweet)
        {
            _twitterDbContext.TweetTags.Add(tweet);
        }

        public void Save()
        {
            _twitterDbContext.SaveChanges();
        }

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

        public IEnumerable<TweetGroupBy> TopTweetTagGroup()
        {
            var qResult = _twitterDbContext.TweetTags.GroupBy(w => w.Tag)
                                .Select(w => new TweetGroupBy
                                {
                                    TagName = w.Key,
                                    Count = w.Count()
                                }).OrderByDescending(o => o.Count).Take(10);
            return qResult;
        }
    }
}
