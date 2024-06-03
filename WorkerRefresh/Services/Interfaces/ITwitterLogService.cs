using TFG.Models;

namespace TFG.Services.Interfaces
{
    public interface ITwitterLogService
    {
        Task<int> Save(TwitterLog log);
        Task<TwitterLog> FindOne(string tweetId);
        Task<List<TwitterLog>> Find();
    }
}
