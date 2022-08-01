namespace TwitterApiDotNet.Models
{
    public class Tweets
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Location { get; set; }
        public int follower_Count { get; set; }
        public int following_Count { get; set; }
        public string ContextAnnotation { get; set; }
        public string Hashtag { get; set; }
    }
}
