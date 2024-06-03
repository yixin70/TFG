using InstagramApiSharp.API;

namespace TFG.Services.Interfaces
{
    public interface IInstagramApiService
    {
        Task<IInstaApi> GetInstance();
    }
}
