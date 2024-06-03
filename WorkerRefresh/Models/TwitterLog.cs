namespace TFG.Models
{
    public class TwitterLog
    {
        public long Id { get; set; }
        public string TweetId { get; set; }
        public string Text { get; set; }
        public DateTime DateTweeted { get; set; }
        public bool IsSuspicious { get; set; }
    }
}
