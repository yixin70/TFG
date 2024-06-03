using InstagramApiSharp.Classes.Models;
using TFG.Models;

namespace TFG.Services.Interfaces
{
    public interface IInstagramLogService
    {
        Task<int> SaveInstagramMedia(InstaMedia media);
        Task<int> SaveInstagramStory(InstaStoryItem story);
        Task<InstagramLog> FindOne(long id);
        Task<List<InstagramLog>> Find();
    }
}
