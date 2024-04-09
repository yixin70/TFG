using InstagramApiSharp.Classes.Models;
using TFG.Models;

namespace TFG.Services.Interfaces
{
    public interface IInstagramStoryService
    {
        Task<int> Save(InstaStoryItem story);
        Task<List<InstagramStory>> Find();
        Task<InstagramStory> FindOne(string id);
    }
}
