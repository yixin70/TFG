using AutoMapper;
using InstagramApiSharp.Classes.Models;
using Microsoft.EntityFrameworkCore;
using TFG.Models;
using TFG.Services.Interfaces;

namespace TFG.Services
{
    public class InstagramMediaService : IInstagramMediaService
    {
        private readonly TFGContext _ctx;
        private readonly IMapper _mapper;
        private readonly IInstagramLogService _logService;

        public InstagramMediaService(TFGContext ctx, IMapper mapper, IInstagramLogService logService)
        {
            _ctx = ctx;
            _mapper = mapper;
            _logService=logService;
        }

        public async Task<int> Save(InstaMedia media)
        {
            if(_ctx.InstagramMedias.Any(e => e.Id.Equals(media.InstaIdentifier)))
            {
                var mediaCTX = await _ctx.InstagramMedias.Where(it => it.Id.Equals(media.InstaIdentifier)).FirstAsync();

                mediaCTX.Uri = media.Images[0].Uri;

                return await _ctx.SaveChangesAsync();
            }
            
            if (media.Carousel != null)
            {
                int count = 0;
                foreach (var carouselImg in media.Carousel)
                {
                    if (_ctx.InstagramMedias.Any(e => e.Id.Equals(carouselImg.InstaIdentifier)))
                        return 0;

                    count++;
                    var insMedia = _mapper.Map<InstagramMedia>(carouselImg);
                    insMedia.Date = media.TakenAt;
                    insMedia.ImageData = await this.GetImageDataFromUri(insMedia.Uri);

                    _ctx.Add(insMedia);

                    var log = new InstagramLog()
                    {
                        Date = insMedia.Date,
                        Description = $"Uploaded a Carousel {count}/{media.Carousel.Count} with Id: {insMedia.Id}"
                    };

                    _ctx.Add(log);
                }
            }
            else
            {
                var insMedia = _mapper.Map<InstagramMedia>(media);
                insMedia.ImageData = await this.GetImageDataFromUri(insMedia.Uri);

                _ctx.Add(insMedia);

                var log = new InstagramLog()
                {
                    Date = insMedia.Date,
                    Description = $"Uploaded a Image with Id: {insMedia.Id}"
                };

                _ctx.Add(log);
            }

            var result = await _ctx.SaveChangesAsync();

            return result;
        }
        public async Task<List<InstagramMedia>> Find()
        {
            var medias = await _ctx.InstagramMedias
                                .AsNoTracking()
                                .ToListAsync();

            return medias;
        }
        public async Task<InstagramMedia> FindOne(string id)
        {
            var medias = await _ctx.InstagramMedias
                                .Where(m => m.Id.Equals(id))
                                .AsNoTracking()
                                .FirstAsync();

            return medias;
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
