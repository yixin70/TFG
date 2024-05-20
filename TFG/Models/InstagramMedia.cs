using System.ComponentModel.DataAnnotations.Schema;

namespace TFG.Models
{
    public class InstagramMedia
    {
        
        public string Id { get; set; } //Insta Identifier
        public byte[] ImageData { get; set; }
        public DateTime Date { get; set; }
        public string Uri { get; set; }

        [ForeignKey("InstagramLog")]
        public long InstagramLogId { get; set; }
        public virtual InstagramLog InstagramLog { get; set; }
    }
}
