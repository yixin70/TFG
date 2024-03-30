using TFG.Models;

namespace TFG.ViewModels.Instagram
{
    public class InstagramIndexVM
    {
        public List<InstagramLog> Logs { get; set; } = new List<InstagramLog>();
        public List<InstagramMedia> Medias { get; set; } = new List<InstagramMedia>();
    }
}
