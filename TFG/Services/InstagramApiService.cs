using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Logger;
using TFG.Services.Interfaces;

namespace TFG.Services
{
    public class InstagramApiService : IInstagramApiService
    {

        private IInstaApi _instaApi;

        public InstagramApiService() { 
            
        }

        private async Task InitApi()
        {
            var delay = RequestDelay.FromSeconds(2, 2);

            var userSession = new UserSessionData
            {
                UserName = "leonardomontes1962",
                Password = "xxx555xxx222"
            };

            this._instaApi = InstaApiBuilder.CreateBuilder()
                    .SetUser(userSession)
                    .UseLogger(new DebugLogger(InstagramApiSharp.Logger.LogLevel.Exceptions)) // use logger for requests and debug messages
                    .SetRequestDelay(delay)
                    .Build();

            if (!_instaApi.IsUserAuthenticated)
            {
                // login
                Console.WriteLine($"Logging in as {userSession.UserName}");
                delay.Disable();
                var logInResult = await _instaApi.LoginAsync();
                delay.Enable();
                if (!logInResult.Succeeded)
                {
                    Console.WriteLine($"Unable to login: {logInResult.Info.Message}");
                    return;
                }
            }
        }
        public async Task<IInstaApi> GetInstance()
        {
            if(_instaApi == null)
            {
                await InitApi();
            }

            return _instaApi;
        }
    }
}
