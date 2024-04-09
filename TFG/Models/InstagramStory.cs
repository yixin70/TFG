using InstagramApiSharp.Classes.Models;

namespace TFG.Models
{
    public class InstagramStory
    {
        public string Id { get; set; }
        public byte[] Content { get; set; }
        public DateTime Date { get; set; }
        public string Uri { get; set; }

    }
}
