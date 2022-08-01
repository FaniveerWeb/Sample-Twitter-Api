namespace TwitterApiDotNet.Models
{
    public class Tweets
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string lang { get; set;}
        public int Like_Count { get; set; }
        public int Retweet_Count { get; set; }
    }
}
