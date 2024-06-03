namespace TFG.Models
{
    public class InstagramLog
    {
        public long Id { get; set; }
        public string MediaId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        //Date at which the media was uploaded
        public DateTime DateTakenAt { get; set; }
        public bool IsSuspicious { get; set; }
        public byte[] ImageData { get; set; }
        public string Uri { get; set; }

        //Media o Historia
        public string Type { get; set; }

    }
}
