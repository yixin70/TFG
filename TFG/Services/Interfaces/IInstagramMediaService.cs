using InstagramApiSharp.API;
using InstagramApiSharp.Classes.Models;
using TFG.Models;

namespace TFG.Services.Interfaces
{
    public interface IInstagramMediaService
    {
        Task<int> Save(InstaMedia media);
        Task<List<InstagramMedia>> Find();
    }
}
