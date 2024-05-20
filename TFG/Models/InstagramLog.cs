namespace TFG.Models
{
    public class InstagramLog
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsSuspicious { get; set; }
        public virtual InstagramMedia? InstagramMedia { get; set; }
        public virtual InstagramStory? InstagramStory { get; set; }

    }
}
