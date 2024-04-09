using AutoMapper;
using InstagramApiSharp.Classes.Models;
using Microsoft.EntityFrameworkCore;
using TFG.Models;
using TFG.Services.Interfaces;

namespace TFG.Services
{
    public class InstagramStoryService : IInstagramStoryService
    {
        private readonly TFGContext _ctx;
        private readonly IMapper _mapper;
        private readonly IInstagramLogService _logService;

        public InstagramStoryService(TFGContext ctx, IMapper mapper, IInstagramLogService logService)
        {
            _ctx = ctx;
            _mapper = mapper;
            _logService=logService;
        }

        public async Task<int> Save(InstaStoryItem story)
        {
            if (_ctx.InstagramStories.Any(e => e.Id.Equals(story.Id)))
                return 0;

            var test = story.VideoList.Count;
            var stor = _mapper.Map<InstagramStory>(story);
            stor.Content = await GetImageDataFromUri(stor.Uri);

            _ctx.Add(stor);

            var log = new InstagramLog()
            {
                Date = stor.Date,
                Description = $"Uploaded a Story with Id: {stor.Id}"
            };

            _ctx.Add(log);
            return await _ctx.SaveChangesAsync();
        }

        private async Task<byte[]> GetImageDataFromUri(string uri)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    // Send a GET request to the URI
                    HttpResponseMessage response = await httpClient.GetAsync(uri);

                    // Ensure a successful response
                    response.EnsureSuccessStatusCode();

                    // Read the response content as a byte array
                    return await response.Content.ReadAsByteArrayAsync();
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Error downloading image: {ex.Message}");
                    return null;
                }
            }
        }
        public async Task<List<InstagramStory>> Find()
        {
            var medias = await _ctx.InstagramStories
                                .AsNoTracking()
                                .ToListAsync();

            return medias;
        }
        public async Task<InstagramStory> FindOne(string id)
        {
            var medias = await _ctx.InstagramStories
                                .Where(m => m.Id.Equals(id))
                                .AsNoTracking()
                                .FirstAsync();

            return medias;
        }
    }
}
