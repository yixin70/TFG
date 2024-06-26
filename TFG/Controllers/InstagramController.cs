﻿using InstagramApiSharp.API;
using Microsoft.AspNetCore.Mvc;
using TFG.Services.Interfaces;
using TFG.ViewModels.Instagram;

namespace TFG.Controllers
{
    public class InstagramController : Controller
    {
        private readonly IInstagramApiService _instagramApiService;
        private readonly IInstagramLogService _instagramLogService;

        public InstagramController(IInstagramApiService instagramApiService,
                                    IInstagramLogService instagramLogService)
        {
            _instagramApiService = instagramApiService;
            _instagramLogService=instagramLogService;
        }

        public async Task<IActionResult> Index()
        {
            InstagramIndexVM vm = new InstagramIndexVM();
            vm.Logs = await _instagramLogService.Find();

            return View(vm);
        }

        public async Task<IActionResult> Refresh()
        {
            IInstaApi _instaApi = await _instagramApiService.GetInstance();

            if (!_instaApi.IsUserAuthenticated)
                await _instaApi.LoginAsync();

            var media = await _instaApi.UserProcessor.GetUserMediaAsync("leonardomontes1962", InstagramApiSharp.PaginationParameters.MaxPagesToLoad(6));
            var user = await _instaApi.UserProcessor.GetCurrentUserAsync();
            var stories = await _instaApi.StoryProcessor.GetUserStoryAsync(user.Value.Pk);


            if (media != null)
            {
                foreach (var med in media.Value)
                {
                    await _instagramLogService.SaveInstagramMedia(med);
                }
            }

            if (stories != null)
            {
                foreach (var story in stories.Value.Items)
                {
                    await _instagramLogService.SaveInstagramStory(story);
                }
            }

            InstagramIndexVM vm = new InstagramIndexVM();
            vm.Logs = await _instagramLogService.Find();


            return View("Index", vm);
        }

        public async Task<IActionResult> Download(long id)
        {

            try
            {
                // Read the content as a byte array
                var media = await _instagramLogService.FindOne(id);
                byte[] content = media.ImageData;

                // Save the content to a file
                string filePath = $"{media.Id}.jpg";
                Console.WriteLine("File downloaded successfully!");

                return File(content, "application/octet-stream", filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

    }
}