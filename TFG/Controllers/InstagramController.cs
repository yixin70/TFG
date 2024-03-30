using AutoMapper;
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
        private readonly IInstagramApiService _instagramApiService;
        private readonly IInstagramMediaService _instagramMediaService;
        private readonly IInstagramLogService _instagramLogService;

        public InstagramController(IInstagramApiService instagramApiService,
                                    IInstagramMediaService instagramMediaService,
                                    IInstagramLogService instagramLogService)
        {
            _instagramApiService = instagramApiService;
            _instagramMediaService = instagramMediaService;
            _instagramLogService=instagramLogService;
        }

        public async Task<IActionResult> Index()
        {
            IInstaApi _instaApi = await _instagramApiService.GetInstance();

            var media = await _instaApi.UserProcessor.GetUserMediaAsync("leonardomontes1962", InstagramApiSharp.PaginationParameters.MaxPagesToLoad(6));

            foreach (var med in media.Value)
            {
                await _instagramMediaService.Save(med);
            }

            InstagramIndexVM vm = new InstagramIndexVM();
            vm.Medias = await _instagramMediaService.Find();
            vm.Logs = await _instagramLogService.Find();


            return View(vm);
        }
    }
}
