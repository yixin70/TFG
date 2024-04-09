using AutoMapper;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        private readonly IInstagramStoryService _instagramStoryService;

        public InstagramController(IInstagramApiService instagramApiService,
                                    IInstagramMediaService instagramMediaService,
                                    IInstagramLogService instagramLogService,
                                    IInstagramStoryService instagramStoryService)
        {
            _instagramApiService = instagramApiService;
            _instagramMediaService = instagramMediaService;
            _instagramLogService=instagramLogService;
            _instagramStoryService = instagramStoryService;
        }

        public async Task<IActionResult> Index()
        {
            IInstaApi _instaApi = await _instagramApiService.GetInstance();

            var media = await _instaApi.UserProcessor.GetUserMediaAsync("leonardomontes1962", InstagramApiSharp.PaginationParameters.MaxPagesToLoad(6));
            var user = await _instaApi.UserProcessor.GetCurrentUserAsync();
            var stories = await _instaApi.StoryProcessor.GetUserStoryAsync(user.Value.Pk);
            foreach (var med in media.Value)
            {
                await _instagramMediaService.Save(med);
            }

            foreach (var story in stories.Value.Items)
            {
                await _instagramStoryService.Save(story);
            }

            InstagramIndexVM vm = new InstagramIndexVM();
            vm.Medias = await _instagramMediaService.Find();
            vm.Logs = await _instagramLogService.Find();


            return View(vm);
        }

        public async Task<IActionResult> Download(string id)
        {

            try
            {
                // Read the content as a byte array
                var media = await _instagramMediaService.FindOne(id);
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

        public async Task<IActionResult> DownloadStory(string id)
        {
            try
            {
                // Read the content as a byte array
                var media = await _instagramStoryService.FindOne(id);
                byte[] content = media.Content;

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