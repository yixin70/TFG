using InstagramApiSharp.Classes.Models;

namespace TFG.Services.Interfaces
{
    public interface IInstagramStoryService
    {
        Task<int> Save(InstaStoryItem story);
    }
}
