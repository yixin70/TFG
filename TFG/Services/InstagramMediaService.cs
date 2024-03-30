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

        public InstagramMediaService(TFGContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<int> Save(InstaMedia media)
        {
            if(_ctx.InstagramMedias.Any(e => e.Id.Equals(media.InstaIdentifier))) 
                return 0;
            
            if (media.Carousel != null)
            {
                foreach (var carouselImg in media.Carousel)
                {
                    if (_ctx.InstagramMedias.Any(e => e.Id.Equals(carouselImg.InstaIdentifier)))
                        continue;

                    var insMedia = _mapper.Map<InstagramMedia>(carouselImg);
                    insMedia.Date = media.TakenAt;
                    insMedia.ImageData = await this.GetImageDataFromUri(insMedia.Uri);

                    _ctx.Add(insMedia);
                }
            }
            else
            {
                var insMedia = _mapper.Map<InstagramMedia>(media);
                insMedia.ImageData = await this.GetImageDataFromUri(insMedia.Uri);

                _ctx.Add(insMedia);
            }

            return await _ctx.SaveChangesAsync();
        }
        public async Task<List<InstagramMedia>> Find()
        {
            var medias = await _ctx.InstagramMedias
                                .AsNoTracking()
                                .ToListAsync();

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
