using AutoMapper;
using InstagramApiSharp.Classes.Models;
using Microsoft.EntityFrameworkCore;
using TFG.Models;
using TFG.Services.Interfaces;

namespace TFG.Services
{
    public class InstagramLogService : IInstagramLogService
    {
        private readonly TFGContext _ctx;
        private readonly IMapper _mapper;

        public InstagramLogService(TFGContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<int> SaveInstagramMedia(InstaMedia media)
        {
            if (media == null) return 0;

            if (_ctx.InstagramLogs.Any(e => e.MediaId.Equals(media.InstaIdentifier)))
            {
                var mediaCTX = await _ctx.InstagramLogs.Where(it => it.MediaId.Equals(media.InstaIdentifier)).FirstAsync();

                mediaCTX.Uri = media.Images[0].Uri;

                return await _ctx.SaveChangesAsync();
            }

            if (media.Carousel != null)
            {
                int count = 0;
                foreach (var carouselImg in media.Carousel)
                {
                    if (_ctx.InstagramLogs.Any(e => e.MediaId.Equals(carouselImg.InstaIdentifier)))
                        return 0;

                    count++;

                    var insLog = _mapper.Map<InstagramLog>(carouselImg);

                    insLog.DateTakenAt = media.TakenAt;
                    insLog.ImageData = await this.GetImageDataFromUri(insLog.Uri);
                    insLog.Description =  $"Uploaded a Carousel {count}/{media.Carousel.Count} with Id: {insLog.MediaId}";
                    insLog.Type = "Carousel";

                    if (media.Caption != null)
                        insLog.IsSuspicious = !(media.Caption.Text.Contains("#farmingnmx"));
                    else
                        insLog.IsSuspicious = true;

                    _ctx.Add(insLog);
                }
            }
            else
            {
                var insLog = _mapper.Map<InstagramLog>(media);
                insLog.ImageData = await this.GetImageDataFromUri(insLog.Uri);
                insLog.Description =  $"Uploaded a Image with Id: {insLog.MediaId}";
                insLog.Type = "Media";

                if (media.Caption != null)
                    insLog.IsSuspicious = !(media.Caption.Text.Contains("#farmingnmx"));
                else
                    insLog.IsSuspicious = true;

                _ctx.Add(insLog);
            }

            var result = await _ctx.SaveChangesAsync();

            return result;
        }

        public async Task<int> SaveInstagramStory(InstaStoryItem story)
        {
            if (story == null) return 0;

            //Check if it already stored
            if (_ctx.InstagramLogs.Any(e => e.MediaId.Equals(story.Id)))
                return 0;

            var insLog = _mapper.Map<InstagramLog>(story);

            insLog.ImageData = await GetImageDataFromUri(insLog.Uri);
            insLog.Description = $"Uploaded a Story with Id: {insLog.MediaId}";
            insLog.Type = "Story";

            _ctx.Add(insLog);

            return await _ctx.SaveChangesAsync();
        }

        public async Task<List<InstagramLog>> Find()
        {
            return await _ctx.InstagramLogs.AsNoTracking().ToListAsync();
        }
        public async Task<InstagramLog> FindOne(long id)
        {
            var log = await _ctx.InstagramLogs.Where(item => item.Id == id).FirstOrDefaultAsync();
            if (log != null)
                return log;

            else
                return null;
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
    }
}
