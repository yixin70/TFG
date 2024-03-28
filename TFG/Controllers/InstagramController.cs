using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TFG.Models;
using TFG.Services.Interfaces;
using TFG.ViewModels.Instagram;

namespace TFG.Controllers
{
    public class InstagramController : Controller
    {
        private readonly TFGContext _ctx;
        private readonly IInstagramApiService _instagramApiService;

        private IInstaApi _instaApi;

        public InstagramController(TFGContext ctx, IInstagramApiService instagramApiService)
        {
            _ctx=ctx;
            _instagramApiService=instagramApiService;
        }

        public async Task<IActionResult> Index()
        {
            _instaApi = await _instagramApiService.GetInstance();

            var d = await _instaApi.UserProcessor.GetUserMediaAsync("leonardomontes1962", InstagramApiSharp.PaginationParameters.MaxPagesToLoad(1));

            //IResult<InstaMediaList> media = await api.GetUserMediaAsync();
            var logs = await _ctx.InstagramLogs
                                .AsNoTracking()
                                .ToListAsync();

            InstagramIndexVM vm = new InstagramIndexVM();
            vm.Logs = logs;

            return View(vm);
        }
    }
}
