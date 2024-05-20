using InstagramApiSharp.Classes.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace TFG.Models
{
    public class InstagramStory
    {
        public string Id { get; set; }
        public byte[] Content { get; set; }
        public DateTime Date { get; set; }
        public string Uri { get; set; }

        [ForeignKey("InstagramLog")]
        public long InstagramLogId { get; set; }
        public virtual InstagramLog InstagramLog { get; set; }
    }
}
