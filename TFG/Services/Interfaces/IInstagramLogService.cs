using TFG.Models;

namespace TFG.Services.Interfaces
{
    public interface IInstagramLogService
    {
        Task<int> SaveLogMedia(InstagramMedia media);
        Task<List<InstagramLog>> Find();
    }
}
